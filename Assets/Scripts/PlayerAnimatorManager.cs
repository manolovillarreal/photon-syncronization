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

    #endregion

    #region MonoBehaviour Callbacks


    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
        ControllerActions = GetComponent<Actions>();
        if (!animator)
        {
            Debug.LogError("PlayerAnimatorManager is Missing Animator Component", this);
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        // deal with Jumping
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        // only allow jumping if we are running.
        if (stateInfo.IsName("Base Layer.Run"))
        {
            // When using trigger parameter
            if (Input.GetButtonDown("Fire2"))
            {
                ControllerActions.Jump();
            }
        }

        if (!animator)
        {
            return;
        }
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        animator.SetFloat("Speed", h);
        animator.SetFloat("Direction", h, directionDampTime, Time.deltaTime);
    }

    #endregion
}