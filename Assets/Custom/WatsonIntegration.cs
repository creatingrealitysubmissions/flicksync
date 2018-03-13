using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WatsonIntegration : MonoBehaviour {
	
	private float secondsCount;
	private int minuteCount;
	private int hourCount;
	public string currentTime;

	private ArrayList phrasesToSay;
	private string phraseToSay;
	private int phraseToSayLength;
	public int phraseNumber;
	public string myCharacter = "Alice";
	private int amountAllowedWrong;
	private bool correct;

	public Text line, recordedText;
	public Image recordingDot;

	private int allowedError;
	// Use this for initialization
	void Start () {
		//LoadScript.instance.script.lines [0].character
		//First we get all the phrases that Alice says, eliminating punctuation and trim it and set it to all lower case
		/*phrasesToSay = new ArrayList();
		for (int i = 0; i < LoadScript.instance.script.lines.Length; i++) {
			if (LoadScript.instance.script.lines [i].character.Contains ("Alice")) {
				string toAdd = LoadScript.instance.script.lines [i].description [0];
				toAdd = toAdd.Replace("\\p{P}+", "");
				toAdd = toAdd.Trim ();
				toAdd = toAdd.ToLower ();
				phrasesToSay.Add (toAdd);
			}
		}*/

		phraseNumber = 0;
	}
	
	// Update is called once per frame
	void Update () {
		line.text = LoadScript.instance.script.lines [phraseNumber].description [0];
		if (ExampleStreaming.instance.Active) {
			recordingDot.color = Color.red;
		} else {
			recordingDot.color = Color.white;
		}
		if (LoadScript.instance.script.lines [phraseNumber].character == myCharacter) {
			setup ();
			UpdateTimerUI ();

			string currentSpokenPhrase = recordedText.text.ToLower();
			if (currentSpokenPhrase.Contains ("final")) {
				int index = currentSpokenPhrase.IndexOf ("(");
				if (index > 0)
					currentSpokenPhrase = currentSpokenPhrase.Substring (0, index);
				currentSpokenPhrase = currentSpokenPhrase.Trim ();
				string[] ssize = currentSpokenPhrase.Split (null);

				for (int i = 0; i < ssize.Length; i++) {
					if (!phraseToSay.Contains (ssize [i])) {
						allowedError--;
					}
					//Debug.Log (ssize [i]);
				}

				if (phraseToSayLength > ssize.Length) {
					allowedError = allowedError - (phraseToSayLength - ssize.Length);
				}

				if (allowedError >= 0) {
					Debug.Log ("Correct!");
					NextPhrase ();
				} else {
					Debug.Log ("Phrase Number: " + phraseNumber + ", currentSpokenPhrase: " + currentSpokenPhrase + ", phraseToSay " + phraseToSay);
					Debug.Log (currentTime+GetComponent<UnityEngine.UI.Text> ().text);
				}
			} else {
				//Debug.Log ("No Phrase");
			}
		}
//		if (Input.GetKeyDown (KeyCode.A)) {
//			ExampleStreaming.instance.Active = false;
//			ExampleStreaming.instance.Active = true;
//		}
	}

	void NextPhrase(){
		phraseNumber++;
		if (LoadScript.instance.script.lines [phraseNumber].character != myCharacter) {
			ExampleStreaming.instance.Active = false;
			Invoke ("NextPhrase", 1);
		} else {
			ExampleStreaming.instance.Active = true;
		}
	}

	public void UpdateTimerUI(){
		//set timer UI
		secondsCount += Time.deltaTime;
		currentTime = hourCount +"h:"+ minuteCount +"m:"+(int)secondsCount + "s ";
		if(secondsCount >= 60){
			minuteCount++;
			secondsCount = 0;
		}else if(minuteCount >= 60){
			hourCount++;
			minuteCount = 0;
		}    
	}

	public void setup(){
		phraseToSay = LoadScript.instance.script.lines [phraseNumber].description[0];
		string[] ssize = phraseToSay.Split(null);
		phraseToSayLength = ssize.Length;
		amountAllowedWrong = ssize.Length/2;
		correct = true;
		allowedError = amountAllowedWrong;
	}
}
