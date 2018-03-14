using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillTimer : MonoBehaviour {

	public Image fillImg;
	public float timeAmt;
	float time;
	public bool isImgOn = false;

	// Use this for initialization
	void OnEnable () {
		isImgOn = true;
		fillImg = this.GetComponent<Image>();
		fillImg.enabled = true;
		time = timeAmt;
		fillImg.fillAmount = 1;
	}
	
	// Update is called once per frame
	void Update () {
		if(time > 0) {
			time -= Time.deltaTime;
			fillImg.fillAmount = time /timeAmt; 
		}

		
		if (Input.GetKeyDown("i")) {
			if (isImgOn == true){
				fillImg.color = Color.clear;
				isImgOn = false;
			}
		}
		else {
			fillImg.color = Color.red;
			isImgOn = true;
		}
	}
}

