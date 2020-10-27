using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviourPun
{
    public GameObject BulletPrefab;
    public Transform BulletSpawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            photonView.RPC("Fire", RpcTarget.All);
        }
    }

    [PunRPC]
    public void Fire(PhotonMessageInfo info)
    {
        GameObject  bullet =  Instantiate(BulletPrefab, BulletSpawnPoint.position, BulletSpawnPoint.rotation);
        bullet.GetComponent<Bullet>().Owner = info.Sender;
    }
}
