using System.Collections;
using UnityEngine;
using Photon.Pun;

public class PlayerAnimatorManager : MonoBehaviourPun
{
    #region Private Fields


    [SerializeField]
    private float directionDampTime = 0.25f;

    private Animator animator;
    private Actions ControllerActions;

    private CharacterController controller;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    private float jumpHeight = 1.0f;
    private float gravityValue = -9.81f;
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
        // deal with Jumping
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // only allow jumping if we are running.

            if (Input.GetButtonDown("Jump"))
            {
                animator.SetTrigger("Jump");
            }
        
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(h, 0, v);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }
        animator.SetFloat("Speed", move.magnitude);
        animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
    }
}