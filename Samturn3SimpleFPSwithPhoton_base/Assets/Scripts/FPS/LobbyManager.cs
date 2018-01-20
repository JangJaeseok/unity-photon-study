using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using Hashtable = ExitGames.Client.Photon.Hashtable;
public partial class LobbyManager : Photon.MonoBehaviour {

	void Awake () {

		if (!PhotonNetwork.connected) {
			//코딩
            PhotonNetwork.ConnectUsingSettings ("1.0"); // PhotonNetwork 접속
			PhotonNetwork.player.CustomProperties = new Hashtable (){ { "RDY", "false" } }; // 플레이어의 CustomProperties 속성 정하기
			// 
		} else {
			//코딩
			Hashtable pl = PhotonNetwork.player.CustomProperties;     
			pl ["RDY"] = "false"; // 게임이 끝나고 로비로 나오면 READY 상태를 false 로 바꿔줌.
			PhotonNetwork.player.SetCustomProperties (pl);
			//

		}
	}

}
//PhotonNetwork.Disconnect ();







//	PhotonNetwork.ConnectUsingSettings ("1.0");                  // PhotonNetwork 접속
//  PhotonNetwork.player.CustomProperties = new Hashtable (){ { "RDY", "false" } };  // 플레이어의 CustomProperties 속성 정하기

//	Hashtable pl = PhotonNetwork.player.CustomProperties;     
//	pl ["RDY"] = "false";                                // 게임이 끝나고 로비로 나오면 READY 상태를 false 로 바꿔줌.
//	PhotonNetwork.player.SetCustomProperties (pl);


