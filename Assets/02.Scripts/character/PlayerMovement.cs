using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private DialogManager dialogManager;
    private PlayerInput playerInput;
    private Rigidbody rigid;
    private Animator animator;

    [HideInInspector]
    public PlayerAttack playerAttack;

 
    Vector3 moveVec = Vector3.zero;
    Vector3 dir = Vector3.zero;
    Vector3 rollVec = Vector3.zero;

    GameObject _locktarget;

    float moveAmount = 0f;
    float moveSpeed = 8f;


    public bool isGround { get; private set; }
    [SerializeField]
    public bool canMove { get; private set; }
    bool isRoll;

    int _mask = (1 << (int) Layer.Npc) | (1 << (int) Layer.Monster) | (1 << (int) Layer.Ground);

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>(); 
        dialogManager = GameObject.Find("GameManager").GetComponent<DialogManager>();
        canMove = true;
        Managers.Mouse.MouseAction -= MouseEventMove;
        Managers.Mouse.MouseAction += MouseEventMove;
         
    }

    // Update is called once per frame
    void FixedUpdate()
    {  
        Move();
        Run();       
        OnUpdateMove();
        Jump();
        Roll();
        
       
    }

    #region move
    private void Move()
    {
        //if (!playerInput.fire && !playerInput.roll) // �����Ҷ� ���߱�
        //{

            moveVec = new Vector3(playerInput.hAxis, 0, playerInput.vAxis).normalized;
            if (isRoll) moveVec = rollVec;
            if (playerAttack != null)  // ������ �̵� ����
            {
                if (!playerAttack.isAttackReady) moveVec = Vector3.zero;
            }

            float m = Mathf.Abs(playerInput.hAxis) + Mathf.Abs(playerInput.vAxis);
            moveAmount = Mathf.Clamp01(m);

            
            transform.LookAt(transform.position + moveVec);
            
            
            transform.position += moveVec * moveSpeed * Time.deltaTime;
            animator.SetFloat("Move", moveAmount, 0.2f, Time.deltaTime);
          
      //  }
  
    }
    void Run()
    {
        bool runnig = false;
        runnig = playerInput.run && moveVec.magnitude != 0f;
        moveSpeed = runnig ? 8f * 1.3f : 8f * 0.8f;
        animator.SetBool("IsRun", runnig);
    
    }
    void MouseEventMove(MouseEvent evt)
    {
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        if (hit.collider.gameObject.layer == (int)Layer.Npc)
        {
            if (raycastHit)
            {
                switch (evt)
                {
                    case MouseEvent.PointerDown:

                        _locktarget = hit.collider.gameObject;


                        break;
                }
            }
        }
        else  // ���̾ npc�� �ƴҶ�
        {

            if (raycastHit)
            {
                switch (evt)
                {
                    case MouseEvent.PointerDown:  // 
                        if (_locktarget != null) _locktarget = null; // Ÿ���� ���϶� �� ����ְ� ����
                        Vector3 turnVec = hit.point - transform.position;
                        turnVec.y = 0;
                        transform.LookAt(transform.position + turnVec);


                        if (playerAttack == null) break;
                        playerAttack.OnAttack();


                        break;
                }
            }
        }
    }
    void OnUpdateMove()
    {
        if (playerInput.hAxis != 0f || playerInput.vAxis != 0f) _locktarget = null;
        if(_locktarget != null)
        {
            dir = DestPos(_locktarget.transform.position);
          
            if (dir.magnitude < 0.5f)
            {
                //Debug.Log("dir.magnitude ����");
                _locktarget = null;
                return;
            }
            else
            {
                
                moveAmount = Mathf.Clamp01(/*moveSpeed * Time.deltaTime, 0,*/ dir.magnitude);
                animator.SetFloat("Move", moveAmount, 0.2f,Time.deltaTime);
               
                transform.position += dir.normalized * moveSpeed * moveAmount * Time.deltaTime;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
            }
        }
    }
    Vector3 DestPos(Vector3 hitpoint)
    {
        Vector3 dest = hitpoint - transform.position;
        
        dest.y = 0;
        
        return dest;
    }
    #endregion
    #region jump
    private void Jump()
    {
        
        if (playerInput.jump && isGround == true &&
            !playerInput.roll && !dialogManager.isAction)
        {
            rigid.AddForce(Vector3.up * 17, ForceMode.Impulse);
            isGround = false;
            animator.SetBool("IsJump", true);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
           
            isGround = true;
            animator.SetBool("IsJump", false);
            
        }
    }
  
    #endregion
    #region roll
    private void Roll()
    {
        if (isGround && moveAmount !=0 && playerInput.roll && canMove)
        {
            canMove = false;
            rollVec = moveVec;
            isRoll = true;
            moveSpeed *= 2;
            
            animator.SetTrigger("IsRoll");
            Invoke("RollOut", 0.6f);
        }
    }
    private void RollOut()
    {
        canMove = true;
        isRoll = false;
        moveSpeed /= 2;
        

    }
    #endregion
}
