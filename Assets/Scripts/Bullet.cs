using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float Speed=10f;
    private Vector3 Direction = new Vector3(1,0,0);

    public Player Owner { get; set; }

    void Start()
    {
        Destroy(gameObject, 4f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Direction*Time.deltaTime*Speed);
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("Hit " + other.gameObject.name);

        if (other.CompareTag("Player"))
        {
            Player otherPlayer = other.GetComponent<PhotonView>().Owner;

            byte otherTeam = (byte)other.GetComponent<PhotonView>().Owner.CustomProperties["Team"];
            byte ownerTeam = (byte)Owner.CustomProperties["Team"];
            if (otherTeam == ownerTeam)
                return;
            other.gameObject.SendMessage("BulletHit", 0.1f, SendMessageOptions.DontRequireReceiver);
        }
        Destroy(this.gameObject);
    }

}
