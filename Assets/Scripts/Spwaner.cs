using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwaner : MonoBehaviour,IOnEventCallback
{

    private float timeToSpawn;
    public float nextSpawn=10f;
    private float initTime;

    public bool isActive;

    public Transform[] SpawnPoints;
    public Transform Container;
    public GameObject spawnPrefab;

    private int spawnCount;


    

    // Use this for initialization
    void Awake()
    {
        timeToSpawn = Time.time + nextSpawn;
        SpawnPoints = GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            if (Time.time >= timeToSpawn)
            {

                nextSpawn = Random.Range(3f, 8f);
                timeToSpawn += nextSpawn;
                Spawn();
            }
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


    private void Spawn()
    {
        if (spawnCount <= 8)
        {
            Debug.Log("Spawn");
            int spawnIndex = Random.Range(0, SpawnPoints.Length - 1);
            RaiseEventOptions raiseEventOptions = new RaiseEventOptions
            {
                Receivers = ReceiverGroup.All,
                CachingOption = EventCaching.AddToRoomCache
            };
            //raiseEventOptions.CachingOption = EventCaching.AddToRoomCache;
            PhotonNetwork.RaiseEvent((byte)EventCodesEnum.SpawnBot, spawnIndex, raiseEventOptions, SendOptions.SendReliable);
        }
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;
        //photonEvent.Sender
        if (eventCode == (byte)EventCodesEnum.SpawnBot)
        {
            int spawnIndex = (int)photonEvent.CustomData;
            Transform t = SpawnPoints[spawnIndex];
            GameObject instance = Instantiate(spawnPrefab, t.position, t.rotation, transform);

            if (PhotonNetwork.IsMasterClient)
            {
                spawnCount++;
            }
        }
    }
}
