using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour {
	
	private float secondsCount;
	private int minuteCount;
	private int hourCount;
	public string currentTime;
	private string phraseToSay;
	private bool correct;
	// Use this for initialization
	void Start () {
		phraseToSay = "It was the best butter";
	}
	
	// Update is called once per frame
	void Update () {
		correct = true;
		UpdateTimerUI();
		string currentPhrase = GetComponent<UnityEngine.UI.Text> ().text;
		if (currentPhrase.Contains ("final")) {
//			string[] ssize = currentPhrase.Split(null);
//			for (int i = 0; i < ssize.Length; i++) {
//				if(phraseToSay.Contains(ssize[i])){
//				}
//				else{
//					correct=false;
//				}
//					
//			}
			if(!currentPhrase.Contains("best butter")){
				correct = false;
			}
			if (correct==true) {
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
