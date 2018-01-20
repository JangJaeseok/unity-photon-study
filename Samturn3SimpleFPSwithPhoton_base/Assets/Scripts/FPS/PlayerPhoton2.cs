using UnityEngine;
using System.Collections;

public class PlayerPhoton2 : Photon.MonoBehaviour {

	public int iHp = 10;

	private Vector3    charPos = Vector3.zero;       // 캐릭터 위치     
	private Quaternion charRot = Quaternion.identity;  //  캐릭터 회전

	PlayerFSM controllerScript;

	public GameManager gameManager;

	void Awake () {
		controllerScript = GetComponent<PlayerFSM>();

		DontDestroyOnLoad(gameObject);
		StartCoroutine (SetupGameManager());
	}
	IEnumerator SetupGameManager(){
		bool bset = false;
		while (!bset) {
			if (gameManager == null && GameObject.Find ("GameManager")) {
				bset = true;
				gameManager = GameObject.Find ("GameManager").transform.GetComponent<GameManager> ();

			}
			yield return null;
		}
	}
	void Update()
	{

		if (!photonView.isMine)
		{
			transform.position = Vector3.Lerp(transform.position, charPos, Time.deltaTime * 5);
			transform.rotation = Quaternion.Lerp(transform.rotation, charRot, Time.deltaTime * 5);
			GetComponent<PlayerFSM> ().SetState (controllerScript.state);

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

	void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{
		if (stream.isWriting)
		{			
			stream.SendNext(transform.position);         // 캐릭터 위치   
			stream.SendNext(transform.rotation);          //  캐릭터 회전 
            stream.SendNext((int)controllerScript.state);  //애니메이션 상태 
		}
		else
		{
			charPos = (Vector3)stream.ReceiveNext();
			charRot = (Quaternion)stream.ReceiveNext();
			controllerScript.state = (CHARCTERSTATE)(int)stream.ReceiveNext();
		}
	}
}
