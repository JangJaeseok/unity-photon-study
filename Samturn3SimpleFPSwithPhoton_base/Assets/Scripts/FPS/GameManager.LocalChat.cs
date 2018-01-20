using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public partial class GameManager : Photon.MonoBehaviour {

	public InputField txtInputChat;
	public Text txtShowChat;

	public void BtnChatSend(){   // 채팅 메시지 전송
		player.playerChar.GetComponent<PhotonView> ().RPC ("ChatInfo", PhotonTargets.All, txtInputChat.text);
	}
	public void ShowChat(string sChat){  // 받은 메시지 보여주기
		txtShowChat.text = sChat;
	}

	public void BtnChangeRoomName(){
		if (PhotonNetwork.isMasterClient) {

			Room room = PhotonNetwork.room;
			room.Name = txtInputChat.text;        // 방이름 바꾸기 
			txtRoomName.text = room.Name;    // 방이름

			Hashtable cp = room.CustomProperties;
			cp ["RN"] = txtInputChat.text;
			room.SetCustomProperties (cp);

			//Debug.Log ("room.Name:"+room.Name);
		}
	}

	void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged){

		Debug.Log ("propertiesChanged:"+propertiesThatChanged);
		txtRoomName.text = (string)propertiesThatChanged["RN"];
	}
}
