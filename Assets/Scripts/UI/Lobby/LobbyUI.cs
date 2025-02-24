using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LobbyUI : MonoBehaviour
{
    [SerializeField] private TMP_InputField roomIDInputField;
    [SerializeField] private TMP_Text roomIDText;
    [SerializeField] private TMP_Text roomStatusText;


    private void Start(){
        Network.Instance.OnRoomJoined+=UpdateUI;
        Network.Instance.OnLobbyJoined+=UpdateUI;
        roomIDText.text="";
        roomStatusText.text="Not yet connected to lobby ❌";
    }
    private void UpdateUI(string roomID){
        roomStatusText.text="Joined Room : " + roomID;
    }

    private void UpdateUI(){
        roomStatusText.text="Joined Lobby. you can now create or join a room ✅";
    }

    public void OnCreateRoom(){
       const string glyphs= "abcdefghijklmnopqrstuvwxyz!@#$&0123456789"; 
       int charAmount = UnityEngine.Random.Range(5,8); 
       String roomID=new String("");
        for(int i=0; i<charAmount; i++)
        {
            roomID += glyphs[UnityEngine.Random.Range(0, glyphs.Length)];
        }
        Network.Instance.CreateRoom(roomID);
        roomIDText.text=roomID;
    }
    public void OnJoinRoom(){
        if(!string.IsNullOrEmpty(roomIDInputField.text)){
            Network.Instance.JoinRoom(roomIDInputField.text);
            roomStatusText.text="Joining Room...";
        }
    }
     private void OnDestroy() {
        Network.Instance.OnRoomJoined-=UpdateUI;
    }
}
