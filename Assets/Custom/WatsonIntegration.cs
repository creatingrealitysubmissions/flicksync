using System.Collections; using System.Collections.Generic; using System; using UnityEngine; using UnityEngine.UI; using Photon;  public class WatsonIntegration : Photon.MonoBehaviour {  	public static WatsonIntegration instance;  	public int score; 	public int totalWords; 	public int totalWordsCorrect; 	public float percentageScore;  	private float secondsCount; 	private int minuteCount; 	private int hourCount; 	public string currentTime; 	public float currentTimer; 	public float totalTime; 	public GameObject visualTimer;  	public Image fillImage;  	private ArrayList phrasesToSay; 	private string phraseToSay; 	private int phraseToSayLength; 	public int phraseNumber; 	public string myCharacter = ""; 	private int amountAllowedWrong; 	private bool correct; 	private bool timeOver; 	private bool firstTime; 	private bool playingAudio;  	public List <AudioSource> audioSourceSource; 	Dictionary<string, AudioSource> audioSources;  	public Text line, recordedText, countDown, percentShown; 	public Image recordingDot;  	private int allowedError; 	// Use this for initialization 	void Start () { 		fillImage = visualTimer.GetComponent<Image>(); 		instance = this; 		audioSources = new Dictionary<string, AudioSource> (); 		int i = 0; 		foreach (string str in LoadScript.instance.script.playableCharacters) { 			audioSources.Add (str, audioSourceSource[i]); 			i++; 		} 		audioSources.Add ("Dormouse", audioSourceSource [i]); 		totalWords = 1; 		totalWordsCorrect = 1; 		phraseNumber = -1; 		percentageScore = 0f; 		currentTimer = 31f; 		totalTime = 31f; 		fillImage.fillAmount = currentTimer / totalTime; 		visualTimer.SetActive (false); 		timeOver = true; 		firstTime = true; 		playingAudio = false; 		PhotonNetwork.SetSendingEnabled (0, true); 		//PhotonNetwork.SetSendingEnabled (1, true); 	}  	// Update is called once per frame 	void Update () { 		if (phraseNumber >= 0) { 			percentageCorrect (); 			UpdateTimerUI (); 			line.text = LoadScript.instance.script.lines [phraseNumber].description [0]; 			playerStreaming (); 			userTurn (); 		} 		if (Input.GetKeyDown(KeyCode.A)) { 			phraseNumber++; 			updateOwner (); 		} 	}  	//this increments the current phraseNumber and determines when streaming  	//for the user should occur depending on whos line it is and who the current users character is 	void NextPhrase(){ 		phraseNumber++; 		updateOwner (); //		if (LoadScript.instance.script.lines [phraseNumber].character != myCharacter) { //			ExampleStreaming.instance.Active = false; //		}    else { //			ExampleStreaming.instance.Active = true; //		} 	}  	//this plays the audio clip associated with a specific line 	IEnumerator PlayAIAudio(){ 		//second player is getting double audio sometimes 		playingAudio = true; 		AudioClip clip = (AudioClip)Resources.Load (LoadScript.instance.script.lines [phraseNumber].audioFile); 		audioSources [LoadScript.instance.script.lines [phraseNumber].character].clip = clip; 		audioSources [LoadScript.instance.script.lines [phraseNumber].character].Play (); 		yield return new WaitForSeconds (clip.length); 		playingAudio = false; 		if (myCharacter == "Alice") { 			NextPhrase (); 		} 	}  	//this just keeps tracking of time from inception, not really used anywhere 	public void UpdateTimerUI(){ 		//set timer UI 		secondsCount += Time.deltaTime; 		currentTime = hourCount +"h:"+ minuteCount +"m:"+(int)secondsCount + "s "; 		if(secondsCount >= 60){ 			minuteCount++; 			secondsCount = 0; 		}else if(minuteCount >= 60){ 			hourCount++; 			minuteCount = 0; 		}        	}  	//this grabs the current phrase to be said, splits it up so we know how many words total there are 	//and sets the length of the phrase, and determines what int amount 50% of the words represent. 	//ei "lets go there here" would have 50% be 2, so you would be allowed to get 2 words incorrect 	public void setup(){ 		phraseToSay = LoadScript.instance.script.lines [phraseNumber].description[0]; 		string[] ssize = phraseToSay.Split(null); 		phraseToSayLength = ssize.Length; 		amountAllowedWrong = ssize.Length/2; 		allowedError = amountAllowedWrong; 	}  	//sends the phraseNumber between the two players 	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) 	{ 		if (stream.isWriting) 		{ 			// We own this player: send the others our data 			stream.SendNext(phraseNumber); 		} 		else 		{ 			// Network player, receive data 			int received = (int)stream.ReceiveNext(); 			phraseNumber = Math.Max(received, phraseNumber); 		} 	}  	//this means that it is your turn and you have 30 seconds to respond 	void countDownTimer(){ 		currentTimer-=Time.deltaTime; 		fillImage.fillAmount = currentTimer / totalTime; 		if (currentTimer <= 0f) { 			totalWords = totalWords + phraseToSayLength; 			timerOver (); 		} 	}  	//this means you either said the right line or you didn't get it within 30 seconds 	void timerOver(){ 		currentTimer = 0f; 		//countDown.text = ""; 		timeOver = true; 		firstTime = true; 		visualTimer.SetActive (false); 		NextPhrase (); 	}  	//this plays the audio only if we aren't currently playing audio 	void playAudio(){ 		if (playingAudio == false) { 			playingAudio = true; 			StartCoroutine("PlayAIAudio"); 		} 	}   	void playerStreaming(){ 		if (ExampleStreaming.instance.Active && myCharacter == LoadScript.instance.script.lines [phraseNumber].character) { 			recordingDot.color = Color.red; 		}    else { 			recordingDot.color = Color.white; 		} 	}  	void percentageCorrect(){ 		percentageScore = ((float)totalWordsCorrect / (float)totalWords); 		percentShown.text = (percentageScore * 100) + "%"; 	}  	public void updateOwner() 	{ 		//tell the masterclient to change the color. 		PhotonNetwork.RPC(photonView, "ChangePhrase", PhotonTargets.MasterClient, false, phraseNumber); 	}  	[PunRPC] 	private void ChangePhrase(int newNum) 	{ 		phraseNumber = Math.Max(newNum, phraseNumber); 	}  	void userTurn(){ 		//if its my characters line then wait for me to say my line! 		if (LoadScript.instance.script.lines [phraseNumber].character != myCharacter) { 			ExampleStreaming.instance.Active = false; 		}    else { 			ExampleStreaming.instance.Active = true; 		}  		if (LoadScript.instance.script.lines [phraseNumber].character == myCharacter) { 			//do a little setup using the like to be said by our character 			setup (); 			//if its this is the first time I have entered this section 			//we have to set timeOver to be false because we just started the timer 			//firstTime to false because it won't be our first time after this 			//Give ourselves 30 seconds, and make the visual timer active 			if (firstTime == true) { 				timeOver = false; 				firstTime = false; 				currentTimer = 31f; 				visualTimer.SetActive (true); 			} 			else if (timeOver == false) { //if timeOver != true then we wan't to keep the timer going until it reaches 0 				countDownTimer (); 			} 			//grab the text that has been spoken by our user 			string currentSpokenPhrase = recordedText.text.ToLower(); 			if (currentSpokenPhrase.Contains ("final")) { //if the words spoken by user have final it means they have finished talking 				int index = currentSpokenPhrase.IndexOf ("("); 				if (index > 0) 					currentSpokenPhrase = currentSpokenPhrase.Substring (0, index); 				currentSpokenPhrase = currentSpokenPhrase.Trim (); 				string[] ssize = currentSpokenPhrase.Split (null); 				//so we just stripped the phrase we spoke to single words 				//below we go through the words and check to see if our line had that word, if it doesn't then we got a word wrong 				for (int i = 0; i < ssize.Length; i++) { 					if (!phraseToSay.Contains (ssize [i])) { 						allowedError--; 					} 				}  				if (phraseToSayLength > ssize.Length) { //if our actual line was more words than we said, that means we also messed up 					allowedError = allowedError - (phraseToSayLength - ssize.Length); 				}  				if (allowedError >= 0) { //if we got 50% or more of the words right this will be true 					totalWordsCorrect = totalWordsCorrect + allowedError + (phraseToSayLength/2); 					totalWords = totalWords + phraseToSayLength; 					timerOver (); 				} 			} 		} 		else if(PhotonNetwork.playerList.Length==1){ //if we only have one player and the current character talking isn't Alice then play the audio 			if(LoadScript.instance.script.lines [phraseNumber].character != "Alice"){ 				playAudio(); 			} 		} 		else if(PhotonNetwork.playerList.Length==2){ //if we only have one player and the current character talking isn't Alice or the Hare then play audio for both 			if(LoadScript.instance.script.lines [phraseNumber].character != "Alice" && LoadScript.instance.script.lines [phraseNumber].character != "Hare"){ 				playAudio(); 			} 		} 	}  }    