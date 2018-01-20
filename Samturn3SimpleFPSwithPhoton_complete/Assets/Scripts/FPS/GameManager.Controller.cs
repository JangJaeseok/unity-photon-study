using UnityEngine;
using System.Collections;

public partial class GameManager : Photon.MonoBehaviour {

	public VirturalJoystick joystick;

	public GameObject FPSCamera;
	public GameObject FPSGun;
	public GameObject bulletPrefab;
	public GameObject bulletSpawn;
	public GameObject[] muzzleFlame;

	public Vector3 movement;

	public float fHo;
	public float fVe;

	public float fRotX;
	public float fRotY;

	public float sensitivityR = 2f;
	public float fSpeedMove = 3f;
	public float fJumpSpeed = 8f;
	public float fGravity   = 20f;

	float minimumYR = -20f;
	float maximumYR = 60f;

	bool bJump = false;

	public void CameraSetup (){
		
		FPSCamera = player.playerChar.transform.Find ("Camera").gameObject;
		FPSGun    = player.playerChar.transform.Find ("itm_gun").gameObject;
		bulletSpawn = FPSGun.transform.Find("bulletspawn").gameObject;

	}

	void UpdateJoystick() {

		//-----------------------------Move 캐릭터------------------------------------------------------------

		fHo = joystick.Horizontal ();
		fVe = joystick.Vertical ();

		if (player.playerChar.GetComponent<PlayerFSM> ()._cc.isGrounded) {
			
			movement = new Vector3 (fHo,0,fVe);
			movement = player.playerChar.transform.rotation * movement;
			movement *= fSpeedMove;

			if (bJump) {
				bJump = false;
				movement.y = fJumpSpeed;
			}

		}
		movement.y -= fGravity * Time.deltaTime;
		player.playerChar.GetComponent<PlayerFSM> ()._cc.Move (movement*fSpeedMove*Time.deltaTime);

		if (fHo != 0 || fVe != 0) {		// 움직일 때 
			player.playerChar.GetComponent<PlayerFSM> ().SetState (CHARCTERSTATE.Run);
		} else {
			player.playerChar.GetComponent<PlayerFSM> ().SetState (CHARCTERSTATE.Idle);
		}

		//-----------------------------Rotate 캐릭터, 무기------------------------------------------------------
		if (IsPointerOverUIObject ()) {  // UI 클릭이면 
			return;
		}

		fRotX = Input.GetAxis ("Mouse X") * sensitivityR;
		fRotY += Input.GetAxis ("Mouse Y") * sensitivityR;
		fRotY = Mathf.Clamp (fRotY, minimumYR, maximumYR);		// 위아래 카메라 각도 제한

		player.playerChar.transform.Rotate(0,fRotX,0);                  // 좌우 rotate
		FPSCamera.transform.localEulerAngles = new Vector3(-fRotY, 0, 0);   // 위아래 rotate
		FPSGun.transform.localEulerAngles    = new Vector3(-fRotY, 0, 0);   // 위아래 rotate
	}

	public void BtnJump(){
		DataManager.Instance.SoundPlay (DataManager.Instance.sound_footstep, 1f);
		if(player.playerChar.GetComponent<PlayerFSM> ()._cc.isGrounded){
			bJump = true;
		}
	}
}


