using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class GameController : MonoBehaviourPunCallbacks
{
    public static GameController Instance;
    public static LayerMask Ground;

    public GameObject playerPrefab;
    public Transform TeamASpawnPoint;
    public Transform TeamBSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

       if (PhotonNetwork.IsConnectedAndReady )
       {
            StartGame();
       }
    }


    #region Photon Callbacks
    //
    public override void OnPlayerEnteredRoom(Player other)
    {
        Debug.LogFormat("{0} Se ha unido a la partida", other.NickName); // not seen if you're the player connecting

        if (PhotonNetwork.IsMasterClient)
        {
          
        }
    }
    public override void OnPlayerLeftRoom(Player other)
    {
        Debug.LogFormat("{0} Ha abandonado la partida", other.NickName);

        if (PhotonNetwork.IsMasterClient)
        {

        }
    }
    public override void OnRoomPropertiesUpdate(Hashtable propertiesThatChanged)
    {
        Debug.Log(propertiesThatChanged);
    }
    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(0);
    }
    #endregion

    public void StartGame()
    {

        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
             PhotonNetwork.SetInterestGroups(1, true);

            if (PlayerManager.LocalPlayerInstance == null)
            {

                object[] myCustomInitData = null;


                byte playerTeam = (byte)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
                Vector3 spawnPosition = (playerTeam == 1) ? TeamASpawnPoint.position : TeamBSpawnPoint.position;
                byte[] i = new byte[] { 0, 1, 2 };
                PhotonNetwork.Instantiate(this.playerPrefab.name, spawnPosition, playerPrefab.transform.rotation,0, myCustomInitData);
               
            }
        }
    }
    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
    public void PlayerDeath(PlayerManager playerManager)
    {
        playerManager.gameObject.SetActive(false);

        byte playerTeam = (byte)playerManager.photonView.Owner.CustomProperties["Team"];
        Vector3 spawnPosition = (playerTeam == 1) ? TeamASpawnPoint.position : TeamBSpawnPoint.position;
        playerManager.transform.position = spawnPosition;
        playerManager.spwanCountdown(5f);

    }
  



}
