using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement; 
using UnityEngine.EventSystems;
using Hashtable = ExitGames.Client.Photon.Hashtable;

enum TARGET{
	MASTER = 0,
	CLIENT = 1,
}
public partial class GameManager : Photon.MonoBehaviour {

	public GameObject photonPrefab;
	public GameObject[] players;

	public DataCharacter player;   // 플레이어 객체 

	public int iPlayerId = 0;
	public int iMaxCount = 0;
	public int iReadyCount = 0;

	public string roomName;

	public void PhotonGameSetting () {

	    //코딩
		Room room = PhotonNetwork.room;
		iMaxCount = room.MaxPlayers; // 최대인원 몇명방에 들어갔는지 확인

		Hashtable cp = room.CustomProperties;
		txtRoomName.text = (string)cp ["RN"]; // 방이름
				
		player.playerChar = PhotonNetwork.Instantiate (photonPrefab.name, new Vector3 (2, 0, 2), Quaternion.identity, 0);
		player.playerChar.GetComponent<PlayerPhoton2> ().iHp = 100;
		//
		CameraSetup ();

		StartCoroutine (WaitPlayer()); // 대전 상대 기다리기 
		//Debug.Log ("roomName:"+room.Name);
	}

	IEnumerator WaitPlayer(){    // 대전 상대 기다리기 
		
		while(bWait){

			players = GameObject.FindGameObjectsWithTag("BASE");  //접속한 유저들 캐릭터 받아오기  

			iReadyCount = 0;
			for (int i = 0; i < iMaxCount; i++) { 
				for (int j = 0; j < PhotonNetwork.playerList.Length; j++) {
					if (i < players.Length) {
						if (players [i].GetComponent<PhotonView> ().viewID == (PhotonNetwork.playerList [j].ID * 1000) + 1) { // playerList가 유저의 순서가 랜덤이라 ID비교를 통해 확인 
						
						     // 코딩
							txtNickName [i].text = PhotonNetwork.playerList [j].NickName;
							Hashtable pl = PhotonNetwork.playerList [j].CustomProperties; // 유저가 변경하는 ready 상태를 실시간으로 반영
							if (pl ["RDY"].Equals ("true")) {
								imgReady [i].SetActive (true);
							} else {
								imgReady [i].SetActive (false);
							}
							//
						}
					} else {
						txtNickName [i].text = "";
						imgReady [i].SetActive (false);
					}
				}

				if (imgReady [i].activeSelf) {
					iReadyCount += 1;
				}
			}

			if(iReadyCount == iMaxCount){   // 모든 사람이 ready 상태이면
				bWait = false;
				btnReady.SetActive (false);
				GameStart ();
			}

			yield return null;
		}

	}

	void GameStart(){
		//코딩
		Room RO = PhotonNetwork.room;
		RO.IsOpen = false; // false 방에 못들어옴
        //
		for (int i = 0; i < iMaxCount; i++) {    // 팀을 나눔
			
			if (i == 0 || i== 2) {
				players[i].tag = "RED";

				int generalnumber = (int)TOY.YUBI;
				DataCharacter tmpPlayer = CCM.Instance.CharacterSetting (id,players[i], generalnumber, 2, 0, 2, (int)COUNTRYTYPE.CHOK ,-1); 

			} else {
				players[i].tag = "BLUE";

				int generalnumber = (int)TOY.JOJO;
				DataCharacter tmpPlayer = CCM.Instance.CharacterSetting (id,players[i], generalnumber, 18, 0, 18, (int)COUNTRYTYPE.WI ,-1); 

			}

			muzzleFlame[i] = players[i].transform.Find ("itm_gun").transform.Find("muzzleflame").gameObject;  // 총구화염

			if (player.playerChar.transform.GetComponent<PhotonView> ().viewID == players [i].GetComponent<PhotonView> ().viewID) {
				iPlayerId = i;
			}

			player.playerChar.transform.Find ("Camera").gameObject.SetActive(true);  // 유저 카메라 켜기


		}

		ResetDisplayHp ();                // HP 초기값 넣어주기

		bControll = true;        // 조이스틱 컨트롤 시작

	}
	void ResetDisplayHp(){          // HP 표시 리셋  UpdateCharHp()를 사용할 경우 주석 처리해도 됨 

		for(int i=0; i<iMaxCount;i++){
			txtHP [i].text = "100";
		}

	}

	public void OnPhotonPlayerConnected( PhotonPlayer other  )         // 다른 유저가 접속했을 때 
	{
		Debug.Log ("other player connect:"+other.NickName);
	}

	public void OnPhotonPlayerDisconnected( PhotonPlayer other  )      // 상대의 접속이 끊겼을 때 
	{
		Debug.Log ("players Length:"+players.Length);

		StartCoroutine (CheckPlayer());
	}
	IEnumerator CheckPlayer(){
		yield return new WaitForSeconds (1f);
		for (int i = 0; i < iMaxCount; i++) { 
			if (players [i] == null) {
				imgReady [i].SetActive (false);
			}
		}

		CheckWhoWin ();
	}

	public void BtnChangeReady(){     // ready 상태 변경 
	
		Hashtable pl = PhotonNetwork.player.CustomProperties;

		if (pl ["RDY"].Equals ("false")) {
			pl ["RDY"] = "true";
			PhotonNetwork.player.SetCustomProperties (pl);
		} else {
			pl ["RDY"] = "false";
			PhotonNetwork.player.SetCustomProperties (pl);
		}
	}

	void OnPhotonPlayerPropertiesChanged(object[] playerAndUpdatedProps) {
		PhotonPlayer pl = playerAndUpdatedProps[0] as PhotonPlayer;
		Hashtable pl1 = pl.CustomProperties;
		Hashtable pl2 = playerAndUpdatedProps[1] as Hashtable;   // player.CustomProperties  같은 값
		Debug.Log("pl:"+pl);
		Debug.Log("pl1:"+pl1);
		Debug.Log("pl2:"+pl2);
	}

	public void BtnGameEnd(){
		StartCoroutine (GameEnd(2f));
	}
	public IEnumerator GameEnd(float ftime){
		yield return new WaitForSeconds (ftime);
		LeaveRoom();
	}

	public void LeaveRoom()            // 룸을 떠남 게임 종료 
	{

		bGameEnd     = false;
		bWait        = true;

		msgPanel.SetActive (false);

		ResetDisplayHp ();                // HP 초기값 넣어주기

		PhotonNetwork.LeaveRoom();

		SceneManager.LoadScene ("lobby");
	}

}

//		Room room = PhotonNetwork.room;
//		iMaxCount = room.MaxPlayers;        // 최대인원 몇명방에 들어갔는지 확인

//		Hashtable cp = room.CustomProperties;
//		txtRoomName.text = (string)cp ["RN"];   // 방이름
		
//		player.playerChar = PhotonNetwork.Instantiate (photonPrefab.name, new Vector3 (2, 0, 2), Quaternion.identity, 0);
//		player.playerChar.GetComponent<PlayerPhoton> ().iHp = 100;


///////////////////////////////////////////////////////////////////////////////

//		txtNickName [i].text = PhotonNetwork.playerList [j].NickName;
//		Hashtable pl = PhotonNetwork.playerList [j].CustomProperties;    // 유저가 변경하는 ready 상태를 실시간으로 반영
//		if (pl ["RDY"].Equals ("true")) {
//			imgReady [i].SetActive (true);
//		} else {
//			imgReady [i].SetActive (false);
//		}

//////////////////////////////////////////////////////////////////////////////////

//		Room RO = PhotonNetwork.room;
//		RO.IsOpen = false;             // false 방에 못들어옴
