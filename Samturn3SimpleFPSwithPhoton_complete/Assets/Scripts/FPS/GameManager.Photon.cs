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

		Room room = PhotonNetwork.room;
		iMaxCount = room.MaxPlayers;        // 최대인원 몇명방에 들어갔는지 확인

		Hashtable cp = room.CustomProperties;
		txtRoomName.text = (string)cp ["RN"];   // 방이름 받아오기

		player.playerChar = PhotonNetwork.Instantiate (photonPrefab.name, new Vector3 (2, 0, 2), Quaternion.identity, 0);
		player.playerChar.GetComponent<PlayerPhoton> ().iHp = 100;   // 이동 2번째 방법일때는 PlayerPhoton2 로 변환
		CameraSetup ();

		StartCoroutine (WaitPlayer());   // 대전 상대 기다리기 

	}

	IEnumerator WaitPlayer(){    // 대전 상대 기다리기 
		
		while(bWait){

			players = GameObject.FindGameObjectsWithTag("BASE");

			iReadyCount = 0;
			for (int i = 0; i < iMaxCount; i++) { 
				for (int j = 0; j < PhotonNetwork.playerList.Length; j++) {
					if (i < players.Length) {
						if (players [i].GetComponent<PhotonView> ().viewID == (PhotonNetwork.playerList [j].ID * 1000) + 1) { // playerlist 랜덤저장이라 순서 체크시 필요

							txtNickName [i].text = PhotonNetwork.playerList [j].NickName;  // UserId 

							Hashtable pl = PhotonNetwork.playerList [j].CustomProperties;   // ready 상태 체크
							if (pl ["RDY"].Equals ("true")) {     
								imgReady [i].SetActive (true);
							} else {
								imgReady [i].SetActive (false);
							}
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

		Room RO = PhotonNetwork.room;
		RO.IsOpen = false;             // false 방에 못들어옴

		for (int i = 0; i < iMaxCount; i++) {    // 들어온 순서대로 팀을 나눔
			
			if (i == 0 || i== 2) {    // 0 , 2 번째
				players[i].tag = "RED";

				int generalnumber = (int)TOY.YUBI;
				DataCharacter tmpPlayer = CCM.Instance.CharacterSetting (id,players[i], generalnumber, 2, 0, 2, (int)COUNTRYTYPE.CHOK ,-1); 

			} else {    // 1 ,3 번째 
				players[i].tag = "BLUE";

				int generalnumber = (int)TOY.JOJO;
				DataCharacter tmpPlayer = CCM.Instance.CharacterSetting (id,players[i], generalnumber, 18, 0, 18, (int)COUNTRYTYPE.WI ,-1); 

			}

			muzzleFlame[i] = players[i].transform.Find ("itm_gun").transform.Find("muzzleflame").gameObject;  // 총구화염

			if (player.playerChar.transform.GetComponent<PhotonView> ().viewID == players [i].GetComponent<PhotonView> ().viewID) {
				iPlayerId = i;  // 자신의 순서 저장
			}

			player.playerChar.transform.Find ("Camera").gameObject.SetActive(true);  // 유저 카메라 켜기

		}

		ResetDisplayHp ();                // HP 초기값 넣어주기

		bControll = true;        // 조이스틱 컨트롤 시작

	}
	void ResetDisplayHp(){          // HP 표시 리셋  // Hp 표시 2번째 방법일 시 필요없음

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

		StartCoroutine (CheckPlayer());  // 접속이 끊기고 photon view 객체가 사라지기까지 시간차가 있어서 코루틴 사용
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

	public void BtnChangeReady(){
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
		Debug.Log ("pl:"+pl);
		Debug.Log ("pl1:"+pl1);
		Debug.Log ("pl2:"+pl2);
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
