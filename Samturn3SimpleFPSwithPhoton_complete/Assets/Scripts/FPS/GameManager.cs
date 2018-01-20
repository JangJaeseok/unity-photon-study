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


		PhotonGameSetting ();

	}

	void Update () {
		if (bControll) {
			UpdateJoystick ();
		}
	}


}

