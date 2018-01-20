using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public partial class LobbyManager : Photon.MonoBehaviour {

	public GameObject panelLobby;    // 로비 UI 패널

	private List<GameObject> roomPrefabs = new List<GameObject> ();                                                   // RoomPrefab 리스트
	public GameObject roomPrefab;    // UI RoomSlot  Prefab

	public InputField roomName;         // 방 제목
	public InputField maxCount;           // 방 최대 인원수
	public Toggle privateToggle;          // 비밀방 체크
	public Text txtRoomCount;

	float fSlotHeight = 100f;                // UI RoomSlot  Prefab 높이

	public void BtnJoinRandomRoom(){
		if (PhotonNetwork.JoinLobby ()) {
			PhotonNetwork.JoinRandomRoom ();
		} else {
			BtnJoinRandomRoom ();  
		}
	}

	public void OnPhotonRandomJoinFailed(object[] codeAndMsg) 
	{
		Debug.Log ("OnPhotonRandomJoinFailed");
		RoomOptions RO = new RoomOptions ();
		RO.MaxPlayers = 2;
		RO.CustomRoomProperties = new Hashtable (){{"RN", "들어와~!"}};
		RO.CustomRoomPropertiesForLobby = new string[] {"RN"};          // "RN" 을 로비에서 사용할 수 있게합니다.
		PhotonNetwork.CreateRoom ((string)RO.CustomRoomProperties["RN"], RO, null);	
		Debug.Log ("CreateRoom");
	}

	void OnJoinedLobby(){
		Debug.Log ("JoinedLobby:");
	}

	void OnJoinedRoom(){
		Debug.Log ("OnJoinedRoom");

		BtnShowOffLobby ();

		SceneManager.LoadScene ("play");

	}

	public void BtnShowOnLobby(){ 

		if (PhotonNetwork.JoinLobby ()) { 
			DataManager.Instance.SoundPlay(DataManager.Instance.sound_click, 1f);
			panelLobby.SetActive (true);   // 로비 UI 보여주기
			RefreshRoomList();
		} else { 
			BtnShowOnLobby();   
		}
	} 
	public void BtnShowOffLobby(){
		panelLobby.SetActive (false);
	}

	public void BtnCreateRoom(){
		if (PhotonNetwork.JoinLobby ()) {
			RoomOptions RO = new RoomOptions ();
			RO.MaxPlayers = byte.Parse(maxCount.text);
			//RO.IsOpen = false;  // false 못들어감
			RO.IsVisible = !privateToggle.isOn;

			RO.CustomRoomProperties = new Hashtable (){{"RN", roomName.text}};
			RO.CustomRoomPropertiesForLobby = new string[] {"RN"};          // "RN" 을 로비에서 사용할 수 있게합니다.
			//PhotonNetwork.CreateRoom ((string)RO.CustomRoomProperties["RN"], RO, null);	

			if (RO.IsVisible) {
				Debug.Log("IsVisible true");
				PhotonNetwork.CreateRoom ((string)RO.CustomRoomProperties["RN"], RO, TypedLobby.Default);	
			} else {
				Debug.Log("IsVisible false");
				PhotonNetwork.JoinOrCreateRoom ((string)RO.CustomRoomProperties["RN"], RO, TypedLobby.Default);
			}
		} else {
			BtnCreateRoom ();  
		}
	}

	void OnReceivedRoomListUpdate(){
		Debug.Log ("roomname");
		for (int i = 0; i < PhotonNetwork.GetRoomList ().Length; i++) {

			GameObject groom = Instantiate(roomPrefab);
			groom.transform.SetParent (roomPrefab.transform.parent);
			groom.GetComponent<RectTransform> ().localScale = roomPrefab.GetComponent<RectTransform> ().localScale;
			groom.GetComponent<RectTransform> ().localPosition = new Vector3 (roomPrefab.GetComponent<RectTransform> ().localPosition.x,roomPrefab.GetComponent<RectTransform> ().localPosition.y-(i*fSlotHeight),roomPrefab.GetComponent<RectTransform> ().localPosition.z);

			groom.transform.Find ("TextRoomName").GetComponent<Text> ().text = PhotonNetwork.GetRoomList () [i].Name;
			Debug.Log ("roomname:"+PhotonNetwork.GetRoomList () [i].Name);
			groom.transform.Find ("TextPlayerCount").GetComponent<Text> ().text = PhotonNetwork.GetRoomList () [i].playerCount + "/" + PhotonNetwork.GetRoomList () [i].maxPlayers;

			groom.transform.Find("joinButton").GetComponent<Button>().onClick.AddListener(()=>{PhotonNetwork.JoinRoom (groom.transform.Find ("TextRoomName").GetComponent<Text> ().text);});
			groom.SetActive (true);
			roomPrefabs.Add (groom);
		}
	}

	public void RefreshRoomList(){

		if (roomPrefabs.Count > 0) {
			for (int i = 0; i < roomPrefabs.Count; i++) {
				Destroy (roomPrefabs [i]);
			}
			roomPrefabs.Clear ();
		}
		for (int i = 0; i < PhotonNetwork.GetRoomList ().Length; i++) {

			GameObject groom = Instantiate(roomPrefab);
			groom.transform.SetParent (roomPrefab.transform.parent);
			groom.GetComponent<RectTransform> ().localScale = roomPrefab.GetComponent<RectTransform> ().localScale;
			groom.GetComponent<RectTransform> ().localPosition = new Vector3 (roomPrefab.GetComponent<RectTransform> ().localPosition.x,roomPrefab.GetComponent<RectTransform> ().localPosition.y-(i*fSlotHeight),roomPrefab.GetComponent<RectTransform> ().localPosition.z);

			Hashtable cp = PhotonNetwork.GetRoomList () [i].CustomProperties;

			groom.transform.Find ("TextRoomName").GetComponent<Text> ().text = (string)cp ["RN"] ;
			groom.transform.Find ("TextPlayerCount").GetComponent<Text> ().text = PhotonNetwork.GetRoomList () [i].playerCount + "/" + PhotonNetwork.GetRoomList () [i].maxPlayers;

			string roomname = PhotonNetwork.GetRoomList () [i].name;
			groom.transform.Find("joinButton").GetComponent<Button>().onClick.AddListener(()=>{PhotonNetwork.JoinRoom (roomname);});

			groom.SetActive (true);
			roomPrefabs.Add (groom);
		}

		txtRoomCount.text = PhotonNetwork.GetRoomList ().Length.ToString ();

		//Debug.Log ("roomcount11:"+PhotonNetwork.countOfRooms);
	}

	public void BtnJoinNameRoom(){
		PhotonNetwork.JoinRoom (roomName.text);
	}

}


//for (int i = 0; i < PhotonNetwork.GetRoomList ().Length; i++) {
//
//	GameObject groom = Instantiate(roomPrefab);
//	groom.transform.SetParent (roomPrefab.transform.parent);
//	groom.GetComponent<RectTransform> ().localScale = roomPrefab.GetComponent<RectTransform> ().localScale;
//	groom.GetComponent<RectTransform> ().localPosition = new Vector3 (roomPrefab.GetComponent<RectTransform> ().localPosition.x,roomPrefab.GetComponent<RectTransform> ().localPosition.y-(i*fSlotHeight),roomPrefab.GetComponent<RectTransform> ().localPosition.z);
//
//	Hashtable cp = PhotonNetwork.GetRoomList () [i].CustomProperties;
//
//	groom.transform.FindChild ("TextRoomName").GetComponent<Text> ().text = (string)cp ["RN"] ;
//	//groom.transform.FindChild ("TextRoomName").GetComponent<Text> ().text = PhotonNetwork.GetRoomList () [i].name;
//	groom.transform.FindChild ("TextPlayerCount").GetComponent<Text> ().text = PhotonNetwork.GetRoomList () [i].playerCount + "/" + PhotonNetwork.GetRoomList () [i].maxPlayers;
//
//	string roomname = PhotonNetwork.GetRoomList () [i].name;
//	groom.transform.FindChild("joinButton").GetComponent<Button>().onClick.AddListener(()=>{PhotonNetwork.JoinRoom (roomname);});
//	//groom.transform.FindChild("joinButton").GetComponent<Button>().onClick.AddListener(()=>{PhotonNetwork.JoinRoom (groom.transform.FindChild ("TextRoomName").GetComponent<Text> ().text);});
//	groom.SetActive (true);
//	roomPrefabs.Add (groom);
//}