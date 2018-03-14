using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Expressions : MonoBehaviour {

	public string character = "Dormouse";
	public float changeTime;
	public Sprite[] expressions;
	public Sprite resting;
	public UnityEngine.UI.Image image;
	int currentExpression = 0;
    Animator charcon;

    public void Start()
    {
        charcon = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update () {
		if (WatsonIntegration.instance.phraseNumber >= 0) {
			if (LoadScript.instance.script.lines [WatsonIntegration.instance.phraseNumber].character == character) {
				if (charcon != null) {
					charcon.SetBool ("talking", true);
				}
				if (changeTime + 0.3f < Time.time) {
					changeTime = Time.time;
					currentExpression++;
					if (currentExpression == expressions.Length) {
						currentExpression = 0;
					}
					image.sprite = expressions [currentExpression];
				}
			} else {
				charcon.SetBool ("talking", false);
				image.sprite = resting;
			}
		}
    }
}
