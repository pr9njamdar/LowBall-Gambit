
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using System;
public class Network : MonoBehaviourPunCallbacks
{
  internal static Network Instance {get; private set;}
  public event Action<string> OnRoomJoined;
  public event Action OnLobbyJoined;
  private bool isConnectedToMaster = false;
  private void Awake(){
    if(Instance==null){
        Instance=this;
        DontDestroyOnLoad(gameObject);
    }
    else{
        Destroy(gameObject);
    }

  }

  private void Start(){
    PhotonNetwork.ConnectUsingSettings();
  }

    public override void OnConnectedToMaster()
    {
         Debug.Log("✅ Connected to Photon Master Server.");
        isConnectedToMaster = true;
        PhotonNetwork.JoinLobby();
    }

    public override void OnJoinedLobby()
    {
        OnLobbyJoined?.Invoke();
    }

    public void CreateRoom(string roomId){
         if (!isConnectedToMaster)
        {
            Debug.LogError("❌ Cannot create room. Not connected to Master Server.");
            return;
        }
        RoomOptions roomOptions=new RoomOptions{MaxPlayers=4};
        PhotonNetwork.CreateRoom(roomId,roomOptions);
    }

    public void JoinRoom(string roomId){
        PhotonNetwork.JoinRoom(roomId);

    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined Room : "+PhotonNetwork.CurrentRoom.Name);
        OnRoomJoined?.Invoke(PhotonNetwork.CurrentRoom.Name);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Room Creation Failed: " + message);
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Join Room Failed: " + message);
    }
}
