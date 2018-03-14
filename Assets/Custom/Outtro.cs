using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outtro : MonoBehaviour {

    public static Outtro instance;

    public GameObject OuttroCanvas;
    public UnityEngine.UI.Text percent;

	// Use this for initialization
	void Start ()
    {
        instance = this;
        OuttroCanvas.gameObject.SetActive(false);
    }

    public void End()
    {
        percent.text = Mathf.Round(WatsonIntegration.instance.percentageScore * 100).ToString() + "%";
        OuttroCanvas.gameObject.SetActive(true);
    }
}
