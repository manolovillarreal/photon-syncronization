using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RoomCtrl : MonoBehaviourPunCallbacks
{
    public const string MAP_PROP_KEY = "map";
    public const string GAME_MODE_PROP_KEY = "gm";


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region Photon Callbacks

    public override void OnJoinedRoom()
    {
        
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {

    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {

    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("El Master Cliente Se fue \nEl nuevo Master Client es :" + newMasterClient.NickName);     
;    }

    #endregion

    #region private methods
    
    #endregion

    #region public Methods
    public void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.LoadLevel(1);
        }
    }

    public void LeaveRoom() // Retorna al lobby
    {
        UIManager.Instance.GoToLobby();
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        StartCoroutine(rejoinLobby());
    }
    #endregion

    IEnumerator rejoinLobby()
    {
        yield return new WaitForSeconds(2);
        // para forzar la actualización de la lista de salas 
        PhotonNetwork.JoinLobby();
    }

}
