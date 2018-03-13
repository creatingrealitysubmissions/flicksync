using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour {
	
	private float secondsCount;
	private int minuteCount;
	private int hourCount;
	public string currentTime;

	private string phraseToSay;

	private int phraseToSayLength;
	private int amountAllowedWrong;
	private bool correct;
	// Use this for initialization
	void Start () {
		phraseToSay = "it was the best butter";
		string[] ssize = phraseToSay.Split(null);
		phraseToSayLength = ssize.Length;
		amountAllowedWrong = ssize.Length/2;
	}
	
	// Update is called once per frame
	void Update () {
		correct = true;
		int allowedError = amountAllowedWrong;
		UpdateTimerUI();
		string currentPhrase = GetComponent<UnityEngine.UI.Text> ().text;
		if (currentPhrase.Contains ("final")) {
			int index = currentPhrase.IndexOf("(");
			if (index > 0)
				currentPhrase = currentPhrase.Substring(0, index);
			currentPhrase = currentPhrase.Trim ();
			string[] ssize = currentPhrase.Split(null);

			for (int i = 0; i < ssize.Length; i++) {
				if (!phraseToSay.Contains (ssize [i])) {
					correct = false;
					allowedError--;
				}
				//Debug.Log (ssize [i]);
			}

			if (phraseToSayLength > ssize.Length) {
				allowedError = allowedError - (phraseToSayLength - ssize.Length);
			}

			if (correct==true || allowedError >= 0) {
				Debug.Log ("Correct!");
			}
			else{
				Debug.Log (currentTime+GetComponent<UnityEngine.UI.Text> ().text);
			}
		}
		else
		Debug.Log ("No Phrase");
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
}
