using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManager : Photon.PunBehaviour
{
    private const string roomName = "RoomName";
    private RoomInfo[] roomsList;
    public List<PhotonPlayer> currentPlayersInRoom = new List<PhotonPlayer>();
    void Start()
    {
        PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;
        PhotonNetwork.ConnectUsingSettings("0.1");
    }
    private void OnGUI()
    {
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());

        //Jos ei olla missään huoneessa, eli ollaan lobbyssa, näytetään nappuloita huoneista
        if (PhotonNetwork.room == null)
        {
            //Ei olla huoneessa
            if (GUI.Button(new Rect(100, 100, 250, 100), "Start Server(Create Room)"))
            {
                PhotonNetwork.CreateRoom(roomName + System.Guid.NewGuid().ToString("N"));
            }

            if (roomsList != null)
            {
                for (int i = 0; i < roomsList.Length; i++)
                {
                    if (GUI.Button(new Rect(100, 250 + (110 * i), 250, 100), "Join " + roomsList[i].Name + "\n\nPlayer Count:" + roomsList[i].PlayerCount))
                    {
                        PhotonNetwork.JoinRoom(roomsList[i].Name);
                    }
                }
            }
        }



    }

    public override void OnReceivedRoomListUpdate()
    {
        // Jos huonelistaus päivittyy palvelimella, tämä metodi ajetaan.
        // Jos siis joku tekee huoneen, tai joku huone menee tyhjäksi ja poistuu olemasta.
        roomsList = PhotonNetwork.GetRoomList();
    }

    public override void OnConnectedToPhoton()
    {
        Debug.Log("Connection to Photon");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to Master");
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room");
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Joined room");
        
    }

}
