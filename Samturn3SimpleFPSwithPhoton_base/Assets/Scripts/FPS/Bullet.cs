using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	GameManager gameManager;

	void Awake(){
		gameManager = GameObject.Find ("GameManager").transform.GetComponent<GameManager> ();
	}

	void OnCollisionEnter (Collision other)
	{
		if (other.transform.tag == "WALL") {
			DataManager.Instance.SoundPlay (DataManager.Instance.sound_hit01, 1f);

			transform.gameObject.SetActive (false);

			Destroy(transform.gameObject, 2.0f);     // Destroy the bullet after 2 seconds
		}

		if(gameManager.player.playerChar.tag == "RED"){
			if (other.transform.tag == "BLUE") {
				DataManager.Instance.SoundPlay (DataManager.Instance.sound_soldierhit01, 1f);

				int viewID = other.transform.GetComponent<PhotonView>().viewID;
				int damage = 10;	
				gameManager.RPCAttackDamage(viewID,damage);

				Destroy(transform.gameObject);     // Destroy the bullet after 2 seconds
			}
		}else if(gameManager.player.playerChar.tag == "BLUE"){
			if (other.transform.tag == "RED") {
				DataManager.Instance.SoundPlay (DataManager.Instance.sound_soldierhit01, 1f);

				int viewID = other.transform.GetComponent<PhotonView>().viewID;
				int damage = 10;	
				gameManager.RPCAttackDamage(viewID,damage);

				Destroy(transform.gameObject);     // Destroy the bullet after 2 seconds
			}
		}

	}

}
