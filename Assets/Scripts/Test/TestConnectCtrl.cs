using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class TestConnectCtrl : MonoBehaviourPunCallbacks
{
    public byte MaxPlayers = 20;
    private byte nextTeam = 1;

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
    public override void OnCreatedRoom()
    {
        Debug.Log("Se creo el cuarto");
        SetPlayerTeam(PhotonNetwork.LocalPlayer);
    }
    public override void OnPlayerPropertiesUpdate(Player targetPlayer, Hashtable changedProps)
    {
        if (targetPlayer.IsLocal && changedProps.ContainsKey("Team"))
        {
            GameController.Instance.StartGame();
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("Player Enter");
        if (PhotonNetwork.IsMasterClient)
        {
            SetPlayerTeam(newPlayer);
        };              
        
    }
    #endregion

    #region Privaye Methods
    private void Connect()
    {
        PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = RegionsCodes.USW.ToString();
        PhotonNetwork.ConnectUsingSettings();
    }
    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = MaxPlayers;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;

        PhotonNetwork.JoinOrCreateRoom("Test Room",roomOptions, new TypedLobby(null,LobbyType.Default));
    }
    private void SetPlayerTeam(Player player)
    {
        var propsToSet = new ExitGames.Client.Photon.Hashtable() { { "Team", nextTeam }, { "KEY", "Value" } };       
        player.SetCustomProperties(propsToSet);
        nextTeam = (nextTeam == 1) ? (byte)2 : (byte)1;
    }
    #endregion

}
