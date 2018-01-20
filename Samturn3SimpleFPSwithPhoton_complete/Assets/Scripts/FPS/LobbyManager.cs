using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public partial class LobbyManager : Photon.MonoBehaviour {

	void Awake () {

		if (!PhotonNetwork.connected) {
			Debug.Log ("ConnectUsingSettings");
			PhotonNetwork.ConnectUsingSettings ("1.0");                  // PhotonNetwork 접속
			//PhotonNetwork.Disconnect ();
			PhotonNetwork.player.CustomProperties = new Hashtable (){ { "RDY", "false" } };
		} else {
			Hashtable pl = PhotonNetwork.player.CustomProperties;
			pl ["RDY"] = "false";
			PhotonNetwork.player.SetCustomProperties (pl);
		}
	}

//	void Update(){
//		if (PhotonNetwork.connected) {
//			
//		} else {
//			
//		}
//	}


}
