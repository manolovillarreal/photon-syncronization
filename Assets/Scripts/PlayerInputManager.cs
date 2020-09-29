using System.Collections;
using UnityEngine;
using Photon.Pun;

public class PlayerInputManager : MonoBehaviourPun
{
    #region Private Fields


    [SerializeField]
    private float directionDampTime = 0.25f;

    private Animator animator;
    private Actions ControllerActions;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 0.5f;
    private float gravityValue = -9.81f;
    private float delay = 1.0f;
    private float timeToMove = 0.5f;
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


        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -1.0f * gravityValue);
            ControllerActions.Jump();
        }
        if (Input.GetButtonDown("Fire1") )
        {
            ControllerActions.Stay();
            ControllerActions.Attack();
            move = Vector3.zero;
        }

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
            ControllerActions.Move(move.magnitude);
            //controller.Move(move * Time.deltaTime * playerSpeed);
        }

        if (!groundedPlayer)
        {
            controller.Move(move * Time.deltaTime * playerSpeed);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        ///controller.Move(playerVelocity * Time.deltaTime);




    }
}