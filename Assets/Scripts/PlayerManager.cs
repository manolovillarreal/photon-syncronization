using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void GameObjectDelegate( GameObject GO);
public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable,IPunInstantiateMagicCallback
{
    #region Public Fields

    public float Health = 1f;
    public static GameObject LocalPlayerInstance;
    public byte Team { get; set; }
    #endregion

    #region Private Fields

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    private GameObject playerUiPrefab;


    //True, when the user is firing
    bool IsFiring;

    #endregion

    #region MonoBehaviour CallBacks
    public void Awake()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        LocalPlayerInstance = gameObject;
        Camera.main.SendMessage("SetTarget",transform, SendMessageOptions.RequireReceiver);

    }
    public void Start()
    {

        // Create the UI
        if (this.playerUiPrefab != null)
        {
            GameObject _uiGo = Instantiate(this.playerUiPrefab);
            _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><b>Missing</b></Color> PlayerUiPrefab reference on player Prefab.", this);
        }
        
    }
    public void Update()
    {
        // we only process Inputs and check health if we are the local player
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }


        if (this.Health <= 0f)
        {

        }     
    }
    public void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }
    }
    public void OnTriggerStay(Collider other)
    {
        // we dont' do anything if we are not the local player.
        if (!photonView.IsMine)
        {
            return;
        }
        
    }
    #endregion

    #region IPun implementation
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            // We own this player: send the others our data
           // stream.SendNext(this.IsFiring);
            stream.SendNext(this.Health);
        }
        else
        {
            // Network player, receive data
           // this.IsFiring = (bool)stream.ReceiveNext();
            this.Health = (float)stream.ReceiveNext();
        }
    }
    public void OnPhotonInstantiate(PhotonMessageInfo info)
    {
        object[] instantiationData = info.photonView.InstantiationData;
        this.Team = (byte)info.photonView.Owner.CustomProperties["Team"];

        if (info.photonView.gameObject.CompareTag("Player"))
        {

            info.photonView.Group = 1;          

        }

    }
    #endregion

    #region RPCs
    [PunRPC]
    public void Die()
    {
        GameController.Instance.PlayerDeath(this);
        
    }
    private void TakeDamage(float dmg)
    {
        Debug.Log("Taking "+dmg+" Damage");
        Health -= dmg;
        if(Health<=0)
        {
            photonView.RPC("Die", RpcTarget.All);
        }

    }

    #endregion

    public void BulletHit(float dmg)
    {
        if (photonView.IsMine)
        {
            TakeDamage(dmg);
        }
    }
    public void spwanCountdown(float time)
    {
        Invoke("Respawn",time);
    }
    public void Respawn()
    {
        Health = 1f;
        gameObject.SetActive(true);
    }
}
