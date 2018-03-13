using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class mutiplayerCommunication : Photon.MonoBehaviour {

//	public int tempNumber;
//
//	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
//	{
//		if (stream.isWriting)
//		{
//			// We own this player: send the others our data
//			stream.SendNext(WatsonIntegration.instance.phraseNumber);
//		}
//		else
//		{
//			// Network player, receive data
//			WatsonIntegration.instance.phraseNumber = (int)stream.ReceiveNext();
//		}
//		tempNumber = WatsonIntegration.instance.phraseNumber;
//	}
}
