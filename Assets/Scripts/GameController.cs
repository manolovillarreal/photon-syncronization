using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviourPunCallbacks
{
    public static GameController Instance;
    [Tooltip("The prefab to use for representing the player")]
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
            if (PlayerManager.LocalPlayerInstance == null)
            {
                object[] myCustomInitData = new object[]
                {
                    20,"sniper",
                };

                PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(0f, 5f, 0f), Quaternion.identity, 0, myCustomInitData);
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
