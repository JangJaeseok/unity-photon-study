using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public partial class GameManager: Photon.MonoBehaviour {

	void UpdateCharHp(){  // AttackProcess2
//		for(int i=0;i<iMaxCount;i++){     
//			if (players [i]) {
//				txtHP [i].text = players [i].GetComponent<PlayerPhoton> ().iHp.ToString ();
//
//				if (players [i].GetComponent<PlayerPhoton> ().iHp <= 0) {  
//					players [i].SetActive (false);
//					imgReady [i].SetActive (false);
//
//					CheckWhoWin ();
//				}
//			}
//		}
	}

	public void BtnShoot(){

		DataManager.Instance.SoundPlay (DataManager.Instance.sound_gun01, 1f);

		var bullet = (GameObject)Instantiate (bulletPrefab, bulletSpawn.transform.position,bulletSpawn.transform.rotation);

		bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * 40;   // Add velocity to the bullet

		StartCoroutine (MuzzleFlame(iPlayerId));  

		RPCMuzzleFlame(iPlayerId);  // 상대 유저에게 내가 총을 쐈다는 것을 보여줌 
	}


	public void ShowMuzzleFlame (int charid){
		Debug.Log ("ShowMuzzleFlame:"+charid);
		StartCoroutine (MuzzleFlame(charid));

	}
	IEnumerator MuzzleFlame(int index){         // 총구 화염
		muzzleFlame[index].SetActive (true);
		yield return new WaitForSeconds (1f);
		muzzleFlame[index].SetActive (false);
	}

	public void RPCAttackDamage(int viewID,int damage){     // 네트워크를 통해 데미지 전송 

		player.playerChar.GetComponent<PhotonView> ().RPC ("AttackInfo", PhotonTargets.All, viewID, damage);

	}
	public void RPCMuzzleFlame(int charid){      

		player.playerChar.GetComponent<PhotonView> ().RPC ("MuzzleFlameShow", PhotonTargets.Others, charid);

	}

	public void AttackProcess (int viewID,int damage){        // 데미지 계산 

		for(int i=0;i<iMaxCount;i++){
			if (players [i].GetComponent<PhotonView> ().viewID == viewID) {
				iHP [i] -= damage;
				txtHP [i].text = iHP [i].ToString ();

				if (iHP [i] <= 0) {  
					players [i].SetActive (false);
					imgReady [i].SetActive (false);

					if (!bGameEnd){
						CheckWhoWin ();
					}
				}
			}
		}

	}

	public void AttackProcess2 (int viewID,int damage){         // 데미지 계산 
		if (player.playerChar.GetComponent<PhotonView> ().viewID == viewID) {
			player.playerChar.GetComponent<PlayerPhoton> ().iHp -= 10;
		}
	}


	void CheckWhoWin(){
		
		GameObject[] tmpplayers1 = GameObject.FindGameObjectsWithTag("RED");
		GameObject[] tmpplayers2 = GameObject.FindGameObjectsWithTag("BLUE");

		Debug.Log ("RED:"+tmpplayers1.Length);
		Debug.Log ("BLUE:"+tmpplayers2.Length);

		if(!bGameEnd && tmpplayers1.Length == 0){
			bGameEnd = true;
			GameEndProcess("Blue Win!!");

		}else if(!bGameEnd && tmpplayers2.Length == 0){
			bGameEnd = true;
			GameEndProcess("Red Win!!");

		}else if (!bGameEnd && tmpplayers1.Length == 0 && tmpplayers2.Length == 0) {
			bGameEnd = true;
			GameEndProcess("Draw");
		}
	}

	void GameEndProcess(string msg){
		msgPanel.SetActive (true);
		msgPanel.transform.Find("Text").GetComponent<Text>().text = msg;
		StartCoroutine(GameEnd(4f));
	}
}
