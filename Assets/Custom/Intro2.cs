using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intro2 : MonoBehaviour {

    public GameObject introCanvas, regularCanvas;
    public Transform objectContainer;
    bool is_intro = false, intro_Ready = false;
    public AudioSource introAudio;

    public void Start()
    {
        for (int i = 0; i < objectContainer.childCount; i++)
        {
            objectContainer.GetChild(i).gameObject.SetActive(false);
        }
        introCanvas.SetActive(false);
        regularCanvas.SetActive(false);
        StartCoroutine("SpawnObjects");
    }

    public void Update()
    {
        if (ExampleStreaming.instance.ResultsField.text.ToLower().Contains("action") && WatsonIntegration.instance.phraseNumber < 0 && is_intro == false && intro_Ready)
        {
            is_intro = true;
            StartIntro();
        }
    }

    public void StartIntro()
    {
        introCanvas.SetActive(false);
        regularCanvas.SetActive(true);
        introAudio.gameObject.SetActive(false);
        WatsonIntegration.instance.phraseNumber++;
    }

    public IEnumerator SpawnObjects()
    {
        for (int i = 0; i < objectContainer.childCount; i++)
        {
            objectContainer.GetChild(i).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
        }
        while (introAudio.isPlaying)
        {
            yield return null;
        }
        introCanvas.SetActive(true);
        intro_Ready = true;
    }
}
