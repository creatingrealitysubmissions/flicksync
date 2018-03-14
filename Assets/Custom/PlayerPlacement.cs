using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlacement : MonoBehaviour {

    public Transform canvas1, canvas2, canvas3, player;
    public Transform set1, set2, set3, setPlayer;
    public SkinnedMeshRenderer hareRenderer;

    private void Start()
    {
        Invoke("SetPlayerLocation", 0.5f);
    }

    public void SetPlayerLocation()
    {
        if (WatsonIntegration.instance.myCharacter == "Hare")
        {
            hareRenderer.enabled = false;
            canvas1.transform.rotation = set1.transform.rotation;
            canvas2.transform.rotation = set2.transform.rotation;
            canvas3.transform.rotation = set3.transform.rotation;
            player.transform.rotation = setPlayer.transform.rotation;
            canvas1.transform.position = set1.transform.position;
            canvas2.transform.position = set2.transform.position;
            canvas3.transform.position = set3.transform.position;
            player.transform.position = setPlayer.transform.position;
        }
    }
}
