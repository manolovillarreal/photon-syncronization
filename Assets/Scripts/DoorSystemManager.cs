
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;

using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public enum EventCodesEnum
{
    Zero,
    DoorSystem
}

public class DoorSystemManager : MonoBehaviour ,IOnEventCallback
{

    public List<AutoDoor> Doors;


    void Awake()
    {
        InitDoorSystem();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void InitDoorSystem()
    {
        Doors = new List<AutoDoor>(GetComponentsInChildren<AutoDoor>());
        for (int i = 0; i < Doors.Count; i++)
        {
            Doors[i].Id =(byte)(i + 1);
        }

    }

    private void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    private void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }


    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        if (eventCode == (byte)EventCodesEnum.DoorSystem)
        {
            object[] data = (object[])photonEvent.CustomData;
            byte doorId = (byte)data[0];
            bool doorState = (bool)data[1];

            string doorAction = (doorState) ? "cerrada" : "abierta";
            string msg = "La puerta " + doorId + " ha sido " + doorAction;

            Doors.Find(x => x.Id == doorId).SetDoorState(doorState);
            //this.BroadcastMessage("SetDoorState", doorState, SendMessageOptions.RequireReceiver);
            Debug.Log(msg);
        }
    }

  
}
