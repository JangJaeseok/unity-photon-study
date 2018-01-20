using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public partial class GameManager : Photon.MonoBehaviour {

	public GameObject[] imgReady;
	public GameObject btnReady;
	public GameObject msgPanel;

	public Text[] txtNickName;
	public Text[] txtHP;
	public Text txtMessage;
	public Text txtRoomName;

	public int[] iHP = new int[4];
	int id    = 0;

	public bool bGameEnd = false;                // 게임이 끝났는지  
	public bool bWait    = true;
	public bool bControll = false;

	void Awake () {

		for (int i = 0; i < 4; i++) {
			iHP [i] = 100;
		}

         
		//삭제 
		player.playerChar = PhotonNetwork.Instantiate (photonPrefab.name, new Vector3 (2, 0, 2), Quaternion.identity, 0);
		player.playerChar.transform.Find ("Camera").gameObject.SetActive(true);  // 유저 카메라 켜기
		muzzleFlame[0] = player.playerChar.transform.Find ("itm_gun").transform.Find("muzzleflame").gameObject;  // 총구화염
		iPlayerId = 0;
		CameraSetup ();
        //
		
		//PhotonGameSetting ();

	}

	void Update () {
		//if (bControll) {
			UpdateJoystick ();
		//	UpdateCharHp ();
		//}

	}


}

