using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    private Transform Destination;
    [SerializeField]
    private bool isActive;

    private GameObject onBoard;
    [SerializeField]
    private byte InterestGroup;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Debug.Log("GO");
                onBoard.SetActive(false);
                onBoard.transform.position = Destination.position;
                onBoard.SetActive(true);

                PhotonNetwork.SetInterestGroups(onBoard.GetComponent<PhotonView>().Group, false);

                onBoard.GetComponent<PhotonView>().Group = InterestGroup;
                PhotonNetwork.SetInterestGroups(InterestGroup, true);


                isActive = false;
                onBoard = null;

            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                isActive = true;
                onBoard = other.gameObject;
            }
           
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (other.GetComponent<PhotonView>().IsMine)
            {
                isActive = false;
                onBoard = null;
            }
        }
    }
}
