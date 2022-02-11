using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private DialogManager dialogManager;
    private PlayerInput playerInput;
    private Rigidbody rigid;
    private Animator animator;

    Vector3 moveVec = Vector3.zero;

    float moveAmount;
    float moveSpeed = 8f;

    bool isGround;
    bool canMove;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>(); 
        dialogManager = GameObject.Find("GameManager").GetComponent<DialogManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
        Jump();
        Roll();
    }

    #region move&run
    private void Move()
    {
        if (playerInput.roll) return;


        moveVec = new Vector3(playerInput.hAxis, 0, playerInput.vAxis).normalized;

        float m = Mathf.Abs(playerInput.hAxis) + Mathf.Abs(playerInput.vAxis);
        moveAmount = Mathf.Clamp01(m);
        
        animator.SetFloat("Move", moveAmount, 0.2f, Time.deltaTime);
        transform.LookAt(transform.position + moveVec);
        //런이면 1.3배 이동속도
        transform.position += moveVec * moveSpeed * (playerInput.run ? 1.3f : 0.8f) * Time.deltaTime;
        //런 애니메이션
        animator.SetBool("IsRun", playerInput.run && moveAmount != 0f);

    }

    #endregion
    #region jump
    private void Jump()
    {
        if (playerInput.jump && isGround == true &&
            !playerInput.roll && !dialogManager.isAction)
        {
            rigid.AddForce(Vector3.up * 17, ForceMode.Impulse);


        }
    }
  
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGround = true;
            animator.SetBool("IsJump", false);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGround = false;
            animator.SetBool("IsJump", true);
        }
    }
    #endregion
    #region roll
    private void Roll()
    {
        if (isGround && moveVec != Vector3.zero && playerInput.roll && canMove)
        {
            canMove = false;
            //RollVec = moveVec;
            moveSpeed *= 2;
            
            animator.SetTrigger("IsRoll");
            Invoke("RollOut", 0.6f);
        }
    }
    private void RollOut()
    {
        canMove = true;
        moveSpeed = 5f;
        

    }
    #endregion
}
