using System.Collections;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimatorManager : MonoBehaviourPun
{

    [SerializeField]
    public float GroundDistance = 0.2f;

    #region Private Fields
    private Animator animator;
    private Actions ControllerActions;
    private CharacterController controller;
    private bool _isGrounded = true;
    private Transform _groundChecker;
    #endregion




    // Use this for initialization
    void Start()
    {
        controller = GetComponent<CharacterController>();
        if (!controller)
        {
            Debug.LogError("PlayerControl is Missing CharacterController Component", this);
        }
        animator = GetComponent<Animator>();
        if (!animator)
        {
            Debug.LogError("PlayerControl is Missing Animator Component", this);
        }

        ControllerActions = GetComponent<Actions>();
        _groundChecker = transform.GetChild(0);

    }
    // Update is called once per fravme
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (!animator)
        {
            return;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 00);


        _isGrounded = Physics.CheckSphere(_groundChecker.position, GroundDistance, GameController.Ground, QueryTriggerInteraction.Ignore);

        if (Input.GetButtonDown("Jump") )
        {
            //ControllerActions.Jump();
        }
        if (Input.GetButtonDown("Fire1") )
        {
            Debug.Log("Shot");
            ControllerActions.Attack();
        }

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            ControllerActions.Move(move.magnitude);
        }






    }
}