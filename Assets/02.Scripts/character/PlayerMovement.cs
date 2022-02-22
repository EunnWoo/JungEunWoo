using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private DialogManager dialogManager;

    private Rigidbody rigid;
    private Animator animator;

    [HideInInspector]
    public PlayerAttack playerAttack;

 
    Vector3 moveVec = Vector3.zero;
    Vector3 dir = Vector3.zero;
    Vector3 rollVec = Vector3.zero;

    public GameObject _locktarget { get; private set; }

    float moveAmount = 0f;
    float moveSpeed = 8f;


    public bool isGround { get; private set; }
    [SerializeField]

    public bool isRoll { get; private set; }

    int _mask = (1 << (int) Layer.Npc) | (1 << (int) Layer.Monster) |(1<<(int)Layer.Ground);

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>(); 
        dialogManager = GameObject.Find("@Scene").GetComponent<DialogManager>();
        
        Managers.Mouse.MouseAction -= MouseEventMove;
        Managers.Mouse.MouseAction += MouseEventMove;
         
    }

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
        moveVec = new Vector3(Managers.Input.hAxis, 0, Managers.Input.vAxis).normalized;
        if (isRoll) moveVec = rollVec; //������ ������ȯ ����

        if (playerAttack!=null)  // ������ �̵� ����
        {
            if (!playerAttack.canMove || !playerAttack.isAttackReady) MoveStop();

        }

        float m = Mathf.Abs(Managers.Input.hAxis) + Mathf.Abs(Managers.Input.vAxis);
        moveAmount = Mathf.Clamp01(m);

            
        transform.LookAt(transform.position + moveVec);
            
            
        transform.position += moveVec * moveSpeed * Time.deltaTime;
        animator.SetFloat("Move", moveAmount, 0.2f, Time.deltaTime);  
    }
    public void MoveStop()
    {
        moveVec = Vector3.zero;
       
    }
    void Run()
    {
        bool runnig = false;
        runnig = Managers.Input.run && moveVec.magnitude != 0f;
        moveSpeed = runnig ? 8f * 1.3f : 8f * 0.8f;
        animator.SetBool("IsRun", runnig);
    
    }
    void MouseEventMove(MouseEvent evt)
    {
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        if (raycastHit)
        {
            if (hit.collider.gameObject.layer == (int)Layer.Npc || hit.collider.gameObject.layer == (int)Layer.Monster || hit.collider.gameObject.layer == (int)Layer.Ground)
            {

                switch (evt)
                {
                    case MouseEvent.PointerDown:
                        if (_locktarget != null) // �̺�Ʈ �߻��� ��������ʴٸ� ����ְ� �ٽ� �ο�
                        {
                            _locktarget = null;
                        }
                        _locktarget = hit.collider.gameObject;
                       
                        break;
                }
            }
            else
            {
                _locktarget = null;
            }

        }
    }
    void OnUpdateMove()
    {
        
        if (Managers.Input.hAxis != 0f || Managers.Input.vAxis != 0f || Managers.Input.jump || Managers.Input.roll) _locktarget = null;
        if(_locktarget != null)
        {

            dir = DestPos(_locktarget.transform.position);

            if (_locktarget.layer == (int)Layer.Npc)
            {
                if (dir.magnitude < 0.5f)
                {
                    _locktarget = null;
                    return;
                }
                else
                {
                   MouseMove(); // ���콺�� �̵��Ҷ��� �Լ�
                }
            }
            else if(_locktarget.layer ==(int)Layer.Monster)
            {
                if(dir.magnitude < playerAttack.range) // �����Ÿ� �̳� ���޽� ��ǥ���� �ѹ� ���� �� ����
                {
                    
                    _locktarget = null;
                    return;
                }
                else
                {
                    MouseMove();
                }
            }
        }
    }
    //hitpoint�� �Ÿ���ȯ �Լ�
    Vector3 DestPos(Vector3 hitpoint)
    {
        Vector3 dest = hitpoint - transform.position;
        
        dest.y = 0;
        
        return dest;
    }
    void MouseMove()
    {
        moveAmount = Mathf.Clamp01(dir.magnitude);
        animator.SetFloat("Move", moveAmount, 0.2f, Time.deltaTime);

        transform.position += dir.normalized * moveSpeed * moveAmount * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
    }
    #endregion
    #region jump
    private void Jump()
    {
        
        if (Managers.Input.jump && isGround == true &&
            !Managers.Input.roll && !dialogManager.isAction)
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
        if (isGround && moveAmount !=0 && Managers.Input.roll )
        {
            
            rollVec = moveVec;
            isRoll = true;

            animator.SetTrigger("IsRoll");
            moveSpeed *= 2;
            Invoke("RollOut", 0.5f);
        }
    }
    private void RollOut()
    {   
        isRoll = false;
        animator.ResetTrigger("IsRoll");
        moveSpeed /= 2;
        

    }
    #endregion
}
