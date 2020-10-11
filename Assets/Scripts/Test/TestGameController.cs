using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class TestGameController : MonoBehaviourPunCallbacks
{
    public byte MaxPlayers = 20;
    public static TestGameController Instance;

    public LayerMask Ground;
    public GameObject playerPrefab;



    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }
    void Start()
    {
        Connect();
    }

    #region PUN Callbacks
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al Servidor de Photon");
        CreateRoom();
    }
    public override void OnJoinedRoom()
    {
        Debug.Log("Se unio a un cuarto");
        StartGame();
    }
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        Debug.Log("Room props update:"+propertiesThatChanged);
    }

    #endregion

    #region Privaye Methods
    private void Connect()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = RegionsCodes.USW.ToString();
        PhotonNetwork.ConnectUsingSettings();
    }
    private void StartGame()
    {
        Instance = this;

        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            if (PlayerManager.LocalPlayerInstance == null)
            {
                object[] myCustomInitData = new object[]
                {
                    20,"sniper",
                };
                
                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 1f, 0f), playerPrefab.transform.rotation, 0, myCustomInitData);
            }
        }
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MaxPlayers;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        PhotonNetwork.JoinOrCreateRoom("Test Room",roomOptions, new TypedLobby(null,LobbyType.Default));
    }
    #endregion
}
