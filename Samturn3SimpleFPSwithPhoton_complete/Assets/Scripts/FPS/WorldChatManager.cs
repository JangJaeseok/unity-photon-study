using System;
using System.Collections.Generic;
using ExitGames.Client.Photon.Chat;
using UnityEngine;
using UnityEngine.UI;

public partial class WorldChatManager : MonoBehaviour, IChatClientListener {

	public ChatClient chatClient;

	public GameObject panelGetMyID;
	public GameObject panelWorldChat;

	public InputField txtMyID;
	public Text txtTitleMyID;
	public Toggle ChannelToggleToInstantiate;          // 채널 패널
	public GameObject FriendListUiItemtoInstantiate;   // 친구 패널

	public string[] ChannelsToJoinOnConnect;    // 채널 등록
	public List<GameObject> panelFriendItems;
	public List<string> FriendsList;

	public string myID;
	public Text msgArea;
	public Text msgInput;

	private string selectedChannelName;    // 현재 선택한 채널
	int iRemoveFriendIndex= -1;

	private readonly Dictionary<string, Toggle> channelToggles       = new Dictionary<string, Toggle>();
	private readonly Dictionary<string,FriendItem> friendListItemLUT =  new Dictionary<string, FriendItem>();

	void Awake(){

		if (PhotonNetwork.connected) {
			txtTitleMyID.text = myID;
			panelGetMyID.SetActive (false);
			panelWorldChat.SetActive (true);
		} 

	}

	public void BtnGetMyID (){

		if (!string.IsNullOrEmpty (txtMyID.text)) {

			DataManager.Instance.SoundPlay(DataManager.Instance.sound_click, 1f);

			PhotonNetwork.player.NickName = txtMyID.text;   // 유저 id
			txtTitleMyID.text             = txtMyID.text;
			myID                          = txtMyID.text;
			panelGetMyID.SetActive (false);
			panelWorldChat.SetActive (true);

			SetupChat ();
		} else {
			DataManager.Instance.SoundPlay(DataManager.Instance.sound_pop, 1f);
		}

	}

	void SetupChat () {
		DontDestroyOnLoad(gameObject);

		ChannelsToJoinOnConnect = new string[] {"WORLD","REGION"};   // 채널 등록
		chatClient = new ChatClient( this );
		chatClient.ChatRegion = "ASIA";  // "EU", "US", and "ASIA"
		chatClient.Connect(PhotonNetwork.PhotonServerSettings.ChatAppID, "1.0",new ExitGames.Client.Photon.Chat.AuthenticationValues(myID));

		ChannelToggleToInstantiate.gameObject.SetActive(false);
		FriendListUiItemtoInstantiate.SetActive(false);
	}
	
	void Update () {
		if (chatClient != null)
		{
			chatClient.Service(); 
		}
	}

	public void OnConnected(){
		Debug.Log ("OnConnected:");
		chatClient.Subscribe (ChannelsToJoinOnConnect);  // 채널 등록
		ShowFriendList ();
		chatClient.SetOnlineStatus (ChatUserStatus.Online); // 친구에게 나의 상태 알려줌 

	}
	public void OnSubscribed(string[] channels,bool[] results){

		foreach (string channel in channels)
		{
			this.chatClient.PublishMessage(channel, "says 'hi'."); // you don't HAVE to send a msg on join but you could.

			if (this.ChannelToggleToInstantiate != null)
			{
				Debug.Log("OnSubscribed: " + channel);
				this.InstantiateChannelButton(channel);

			}
		}

		ShowChannel(channels[0]);
	}
	public void OnUnsubscribed(string[] channels){
	}
	public void OnDisconnected(){
		Debug.Log ("OnDisconnected:");
	}

	public void OnChatStateChange(ChatState state){
		Debug.Log ("ChatState:"+state);
	}

	public void  OnPrivateMessage(string sender, object message, string channelName){  // 비밀 메시지 받기
		InstantiateChannelButton(channelName);

		byte[] msgBytes = message as byte[];
		if (msgBytes != null)
		{
			Debug.Log("Message with byte[].Length: "+ msgBytes.Length);
		}
		if (this.selectedChannelName.Equals(channelName))
		{
			Debug.Log("OnPrivateMessage: " + message);
			ShowChannel(channelName);
		}
	}

	public void OnGetMessages(string channelName, string[] senders, object[] messages)
	{
		if (channelName.Equals(selectedChannelName))
		{
			ShowChannel(selectedChannelName);  	
		}
	}

	public void BtnSendMessage(){
		if (string.IsNullOrEmpty(msgInput.text))
		{
			return;
		}
		SendChatMessage(msgInput.text);
	}
	private void SendChatMessage(string chatmessage)
	{
		bool doingPrivateChat = chatClient.PrivateChannels.ContainsKey(selectedChannelName);
		string privateChatTarget = string.Empty;
		if (doingPrivateChat)
		{
			string[] splitNames = this.selectedChannelName.Split(new char[] { ':' });
			privateChatTarget = splitNames[1];
		}

		if (chatmessage [0].Equals ('\\')) {   // 채팅창에  \ 옵션이 들어가 있으면
			string[] tokens = chatmessage.Split(new char[] {' '}, 2);
			if (tokens[0].Equals("\\p") && !string.IsNullOrEmpty(tokens[1]))   // \p 는 프라이빗 대화 \p ID message 형식
			{
				string[] subtokens = tokens[1].Split(new char[] {' ', ','}, 2);
				if (subtokens.Length < 2) return;

				string targetUser = subtokens[0];   // 프라이빗 타겟 유저
				string message = subtokens[1];      // 프라이빗 메시지
				chatClient.SendPrivateMessage(targetUser, message);
			}
		} else {
			if (doingPrivateChat) {
				chatClient.SendPrivateMessage (privateChatTarget, chatmessage);
			} else {
				chatClient.PublishMessage (selectedChannelName, chatmessage);
			}
		}

	}

	private void InstantiateChannelButton(string channelName)   // 채널 버튼
	{
		if (this.channelToggles.ContainsKey(channelName))
		{
			Debug.Log("Skipping creation for an existing channel toggle.");
			return;
		}

		Toggle cbtn = (Toggle)GameObject.Instantiate(this.ChannelToggleToInstantiate);
		cbtn.gameObject.SetActive(true);
		cbtn.GetComponentInChildren<SelectChannel>().SetChannel(channelName);
		cbtn.transform.SetParent(this.ChannelToggleToInstantiate.transform.parent, false);

		this.channelToggles.Add(channelName, cbtn);
	}

	public void ShowChannel(string channelName)
	{
		if (string.IsNullOrEmpty(channelName))
		{
			return;
		}

		ChatChannel channel = null;
		bool found = this.chatClient.TryGetChannel(channelName, out channel);
		if (!found)
		{
			Debug.Log("ShowChannel failed to find channel: " + channelName);
			return;
		}

		selectedChannelName = channelName;             // 채널 선택
		msgArea.text = channel.ToStringMessages();     // 채널별 저장된 대화 불러오기 

		foreach (KeyValuePair<string, Toggle> pair in channelToggles)
		{
			pair.Value.isOn = pair.Key == channelName ? true : false;
		}
	}

	public void BtnAddFriendList(){                   // 친구 추가
		string friendid = msgInput.text;
		FriendsList.Add(friendid);
		ShowFriendList ();
	}
	public void BtnSelectRemoveFriend(Button btn){      // 친구 선택
		iRemoveFriendIndex = btn.GetComponent<FriendItem> ().iIndex;
	}
	public void BtnRemoveFriendList(){               // 친구 삭제
		FriendsList.RemoveAt(iRemoveFriendIndex);
		ShowFriendList ();
	}
		
	public void ShowFriendList(){

		if (FriendsList!=null  && FriendsList.Count>0)
		{
			this.chatClient.AddFriends(FriendsList); // Add some users to the server-list to get their status updates

			for (int i = 0; i < panelFriendItems.Count; i++) {
				Destroy (panelFriendItems [i]);
			}
			//panelFriendItem.Clear ();

			int count =0;
			foreach(string _friend in FriendsList)
			{
				InstantiateFriendButton(_friend,count);

				count += 1;
			}

		}

	}

	private void InstantiateFriendButton(string friendId,int index)
	{
		GameObject fbtn = (GameObject)GameObject.Instantiate(FriendListUiItemtoInstantiate);
		fbtn.GetComponent<FriendItem>().iIndex = index;
		fbtn.gameObject.SetActive(true);
		panelFriendItems.Add (fbtn);
		FriendItem  _friendItem =	fbtn.GetComponent<FriendItem>();

		_friendItem.FriendId = friendId;

		fbtn.transform.SetParent(FriendListUiItemtoInstantiate.transform.parent, false);

		friendListItemLUT[friendId] = _friendItem;
	}
		
	public void OnStatusUpdate(string user,int status,bool gotMessage,object message){
		if (friendListItemLUT.ContainsKey(user))
		{
			FriendItem _friendItem = friendListItemLUT[user];
			if ( _friendItem!=null) _friendItem.OnFriendStatusUpdate(status,gotMessage,message);
		}
	}
	public void DebugReturn(ExitGames.Client.Photon.DebugLevel level, string message){
		Debug.Log ("DebugReturn:"+message);
	}
		
}
