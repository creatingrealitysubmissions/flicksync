using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour {

    public UnityEngine.UI.Image logo1, logo2, logo3, fading;
    public float rate = 0.01f;
    public string toLoad;

	// Use this for initialization
	void Start () {
        logo1.gameObject.SetActive(true);
        logo2.gameObject.SetActive(false);
        logo3.gameObject.SetActive(false);
        fading = logo1;
        Invoke("StartFade", 2f);
	}
    public IEnumerator FadeOut(UnityEngine.UI.Image sprite)
    {
        sprite.color = new Color(sprite.color.r, sprite.color.g, sprite.color.b, sprite.color.a - rate);
        yield return null;
        if(sprite.color.a > 0)
        {
            StartCoroutine(FadeOut(sprite));
        }
        else
        {
            if(sprite == logo1)
            {
                logo2.gameObject.SetActive(true);
                fading = logo2;
                Invoke("StartFade", 0.5f);
            }
            else if (sprite == logo2)
            {
                logo3.gameObject.SetActive(true);
                fading = logo3;
                Invoke("StartFade", 0.5f);
            }
            else if (sprite == logo3)
            {
                SceneManager.LoadScene(toLoad);
            }
        }
    }
	void StartFade()
    {
        StartCoroutine(FadeOut(fading));
    }
	// Update is called once per frame
	void Update () {
		
	}
}
