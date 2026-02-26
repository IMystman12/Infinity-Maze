using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class MutilplayerManager : MonoBehaviourPunCallbacks
{
    public static MutilplayerManager Instance;
    public PlayerBase localPlayer, playerPref, playerTemp;
    public List<PlayerBase> players = new List<PlayerBase>();
    private void Awake() => Instance = this;
    // Start is called before the first frame update
    void Start() => PhotonNetwork.ConnectUsingSettings();
    public override void OnConnectedToMaster() => PhotonNetwork.JoinRandomRoom();
    public override void OnJoinRandomFailed(short returnCode, string message) => PhotonNetwork.CreateRoom(null, new RoomOptions() { MaxPlayers = 20 });
    public override void OnDisconnected(DisconnectCause cause) => OnLeftRoom();
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedOrCreatedRoom");
        localPlayer.gameObject.SetActive(false);
        playerTemp = PhotonNetwork.Instantiate(playerPref.name, localPlayer.transform.position, localPlayer.transform.rotation).GetComponent<PlayerBase>();
        playerTemp.maze = localPlayer.maze;
        players.Add(playerTemp);
    }
    public override void OnLeftRoom()
    {
        Debug.Log("OnDisconnected");
        while (players.Count > 0)
        {
            Destroy(players[0].gameObject);
            players.RemoveAt(0);
        }
        PhotonNetwork.ConnectUsingSettings();
        localPlayer.gameObject.SetActive(true);
    }
}
