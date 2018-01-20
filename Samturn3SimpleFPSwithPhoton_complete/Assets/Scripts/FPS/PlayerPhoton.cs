using UnityEngine;
using System.Collections;

public class PlayerPhoton  : Photon.PunBehaviour, IPunObservable {

	public int iHp = 10;
	public string chatMessage;            //채팅 메시지

	public GameManager gameManager;

	void Awake () {

		DontDestroyOnLoad(gameObject);
		StartCoroutine (GetGameManager());  // 생성시 photonview 시간차로 코루틴으로 gameManager 얻음

	}
	IEnumerator GetGameManager(){
		bool bset = false;
		while (!bset) {
			if (gameManager == null && GameObject.Find ("GameManager")) {
				bset = true;
				gameManager = GameObject.Find ("GameManager").transform.GetComponent<GameManager> ();

			}
			yield return null;
		}
	}

	[PunRPC]          
	public void AttackInfo(int viewID,int damage){
		gameManager.AttackProcess (viewID,damage);
	}
	[PunRPC]          
	public void MuzzleFlameShow(int charid){
		gameManager.ShowMuzzleFlame (charid);
	}
	[PunRPC]      // 채팅 메시지 전송
	public void ChatInfo(string sCaht){
		gameManager.ShowChat (sCaht);
	}
	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{
			// 우리 캐릭터가 다른 유저에게 정보를 전송합니다. 
			stream.SendNext(this.iHp);
			stream.SendNext(this.chatMessage);         // 추가  
		}
		else
		{
			// 다른 유저가 정보를 받습니다.
			this.iHp             = (int)stream.ReceiveNext();
			this.chatMessage   = (string)stream.ReceiveNext();    // 추가 
		}
	}
}
