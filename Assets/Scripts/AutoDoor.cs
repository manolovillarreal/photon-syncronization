using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;

public class AutoDoor : MonoBehaviour
{



    public byte Id;

    public GameObject Door;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetDoorState(bool state)
    {
        Door.SetActive(state);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SendDoorSystemEvent(false);       
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SendDoorSystemEvent(true);
        }
    }

    private void SendDoorSystemEvent(bool state)
    {
        object[] content = new object[] { this.Id, state };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions {
                Receivers = ReceiverGroup.All ,
                CachingOption  = EventCaching.DoNotCache
            };
        PhotonNetwork.RaiseEvent((byte)EventCodesEnum.DoorSystem, content, raiseEventOptions, SendOptions.SendReliable);


        var propsToSet = new ExitGames.Client.Photon.Hashtable() { { "Door_" + this.Id,state} };
        PhotonNetwork.CurrentRoom.SetCustomProperties(propsToSet);
    }
   
}
