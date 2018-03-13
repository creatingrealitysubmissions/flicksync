using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class mutiplayerCommunication : Photon.MonoBehaviour {

	public WatsonIntegration currentScript;
	public int tempNumber;

	void Start(){
		
		currentScript = WatsonIntegration.instance;

	}


	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// We own this player: send the others our data
			stream.SendNext(currentScript.phraseNumber);
		}
		else
		{
			// Network player, receive data
			this.currentScript.phraseNumber = (int)stream.ReceiveNext();
		}
		tempNumber = this.currentScript.phraseNumber;
	}
}
