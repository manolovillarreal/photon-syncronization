using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable,IPunInstantiateMagicCallback
{
    #region Public Fields

    public float Health = 1f;
    public static GameObject LocalPlayerInstance;

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

    /// <summary>
    /// MonoBehaviour method called when the Collider 'other' enters the trigger.
    /// Affect Health of the Player if the collider is a beam
    /// Note: when jumping and firing at the same, you'll find that the player's own beam intersects with itself
    /// One could move the collider further away to prevent this or check if the beam belongs to the player.
    /// </summary>
    public void OnTriggerEnter(Collider other)
    {
        if (!photonView.IsMine)
        {
            return;
        }
        if (!other.name.Contains("Beam"))
        {
            return;
        }

        this.Health -= 0.1f;
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
        Debug.Log("OnPhotonInstantiate");
        if (info.photonView.gameObject.CompareTag("Player"))
        {
            Debug.Log("Se creo el  Player Prefab de " + info.Sender.NickName);

            object[] instantiationData = info.photonView.InstantiationData;

            int level = (int)instantiationData[0];
            string rol = (string)instantiationData[1];
            Debug.Log("Player Level:  " + level + " Clase: " + rol);

        }
    }
    #endregion

    #region RPCs

  

    [PunRPC]
    public void TakeDamage(float dmg)
    {
        Debug.Log("Taking "+dmg+" Damage");
        Health -= dmg;
    }

    #endregion

    public void BulletHit(float dmg)
    {
        if (photonView.IsMine)
        {
            photonView.RPC("TakeDamage", RpcTarget.All,dmg);
        }
    }
}
