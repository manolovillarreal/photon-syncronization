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

    // Start is called before the first frame update
    void Start()
    {
        Instance = this;

        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }
        else
        {
            if (PhotonNetwork.IsConnectedAndReady && PlayerManager.LocalPlayerInstance == null)
            {
                object[] myCustomInitData = new object[]
                {
                    20,"sniper",
                };

                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 1f, 0f), playerPrefab.transform.rotation, 0, myCustomInitData);
            }
            else
            {
                PhotonNetwork.OfflineMode = true;
                Instantiate(playerPrefab, new Vector3(0f, 1f, 0f), playerPrefab.transform.rotation);
            }
        }

        if (PhotonNetwork.IsMasterClient)
        {
            //PhotonNetwork.InstantiateRoomObject();
            //photonView.TransferOwnership();
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


    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }



}
