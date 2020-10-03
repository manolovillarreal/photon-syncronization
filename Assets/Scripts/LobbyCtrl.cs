using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCtrl : MonoBehaviourPunCallbacks
{

    public const string MAP_PROP_KEY = "map";
    public const string GAME_MODE_PROP_KEY = "gm";


    public int maxPlayers = 4;
    public bool EstrictSearch { get; set; }
    private bool flag ;
    #region SerializeFields
    [SerializeField]
    private Transform RoomsContainer; 
    [SerializeField]
    private GameObject prefabRoomItem; 

    #endregion

    private Dictionary<RoomInfo,GameObject> RoomsList; //lista de salas

    //Match Options
    private int expectedPlayers = 0;



    #region MonoBehaviour Callbacks 
    void Awake()
    {

    }

    #endregion

    #region Photon Callbacks 

    public override void OnJoinedLobby()
    {
        Debug.Log("Se unio al lobby");
    }
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
      
    }
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("No se econtro partida");
        CreateRoom();       
    }
    public override void OnCreatedRoom()
    {
        Debug.Log("Se creo un nuevo cuarto para "+PhotonNetwork.CurrentRoom.MaxPlayers);
    }
    public override void OnJoinedRoom()
    {    
        Debug.Log("Se unio a un cuarto");
        UIManager.Instance.ShowProgress("Waiting for players . . .");
    }
    public override void OnCreateRoomFailed(short returnCode, string message) //si la sala existe
    {
        Debug.Log("Fallo en crear una nueva sala \n"+message);
    }
    #endregion

    #region Private Methods

    #endregion
    #region Public Methods
    public void CreateRoom()
    {
        Debug.Log("Creando nueva sala para "+expectedPlayers+" judadores");

        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = (byte)expectedPlayers;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        roomOptions.CleanupCacheOnLeave = false;

        PhotonNetwork.CreateRoom(null,roomOptions);
    }
    public void FindMatch(int players)
    {
        expectedPlayers = players;
        PhotonNetwork.JoinRandomRoom(null, (byte)expectedPlayers);
        UIManager.Instance.GotoRoom();
       
    }
    public void LeaveLobby() { 
        UIManager.Instance.PanelLobby.SetActive(false);
        UIManager.Instance.PanelConnect.SetActive(false);

        PhotonNetwork.LeaveLobby();
    }
    #endregion




}
