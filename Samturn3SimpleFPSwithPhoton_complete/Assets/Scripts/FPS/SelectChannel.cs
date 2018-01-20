using ExitGames.Client.Photon.Chat;
using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectChannel : MonoBehaviour, IPointerClickHandler
{
	public string Channel;
	public Text txtChannelName;

	public void SetChannel(string channel)
	{
		this.Channel = channel;
		txtChannelName.text = channel;
	}

	public void OnPointerClick(PointerEventData eventData)
	{
		WorldChatManager handler = FindObjectOfType<WorldChatManager>();
		handler.ShowChannel(this.Channel);
	}
}