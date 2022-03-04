using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{
    
    public Define.AttackType attackType { get; private set; }
    private DialogManager dialogManager;

    private Rigidbody rigid;
    private Animator animator;

    [HideInInspector]
    public PlayerAttack playerAttack;


    Vector3 moveVec = Vector3.zero;
    Vector3 dir = Vector3.zero;
    Vector3 rollVec = Vector3.zero;

    float moveAmount = 0f;
    float moveSpeed = 8f;

    public bool isGround { get; private set; }
    public bool isRoll { get; private set; }

    int _mask = (1 << (int)Layer.Item) 
        | (1 << (int)Layer.Npc)
        | (1 << (int)Layer.Monster) 
        | (1 << (int)Layer.Ground);

    public override void Init()
    {
        if(playerAttack == null) playerAttack = GetComponent<PlayerAttack>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        dialogManager = GameObject.Find("@Scene").GetComponent<DialogManager>();

        Managers.Mouse.MouseAction -= OnMouseEvent;
        Managers.Mouse.MouseAction += OnMouseEvent;

        //Managers.UI.ShowPopupUI<UI_Button>("UITest");
       
    }
    protected override void UpdateMoving()
    {
        if (!Managers.UI.isAction)
        {
            Move();
            Run();
            Jump();
            Roll();
        }
    }
    protected override void UpdateAttack() 
    {
        if (!Managers.UI.isAction)
        {
            OnAttack();
        }
        
    }

    #region moving
    private void Move()
    {
        moveVec = new Vector3(Managers.Input.hAxis, 0, Managers.Input.vAxis).normalized;
        if (isRoll) moveVec = rollVec; //구를때 방향전환 막기

        if (playerAttack != null)  // 공격중 이동 막기
        {
            if (!playerAttack.canMove ) MoveStop();
        }

        float m = Mathf.Abs(Managers.Input.hAxis) + Mathf.Abs(Managers.Input.vAxis);
        moveAmount = Mathf.Clamp01(m);


        transform.LookAt(transform.position + moveVec);


        transform.position += moveVec * moveSpeed * Time.deltaTime;
        animator.SetFloat("Move", moveAmount, 0.2f, Time.deltaTime);

        //마우스로 이동
        if (_lockTarget != null)
        {
            dir = DestPos(_lockTarget.transform.position); // 타겟과의 거리 값

            if (_lockTarget.layer == (int)Layer.Npc || _lockTarget.layer == (int)Layer.Item)
            {
                if (dir.magnitude < 0.5f)
                {
                    _lockTarget = null;
                    return;
                }
                else
                {
                    MouseMove(); // 마우스로 이동할때의 함수
                }
            }
        }
    }
    void MouseMove()
    {
        moveAmount = Mathf.Clamp01(dir.magnitude);
        animator.SetFloat("Move", moveAmount, 0.2f, Time.deltaTime);

        transform.position += dir.normalized * moveSpeed * moveAmount * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 20 * Time.deltaTime);
    }
    void Run()
    {
        bool runnig = false;
        runnig = Managers.Input.run && moveVec.magnitude != 0f;
        moveSpeed = runnig ? 8f * 1.3f : 8f * 0.8f;
        animator.SetBool("IsRun", runnig);
    }

    
    public void MoveStop()
    {
        moveVec = Vector3.zero;
    }
    //hitpoint와 거리반환 함수


    #endregion
    #region jump
    private void Jump()
    {

        if (Managers.Input.jump && isGround == true &&
            !Managers.Input.roll)
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
        if (isGround && moveAmount != 0 && Managers.Input.roll)
        {

            rollVec = moveVec;
            isRoll = true;
            animator.SetTrigger("IsRoll");
            moveSpeed *= 1.5f;
            Invoke("RollOut", 0.5f);
        }
    }
    private void RollOut()
    {
        isRoll = false;
        animator.ResetTrigger("IsRoll");
        moveSpeed /= 1.5f;


    }
    #endregion
    #region Attack
    void OnAttack()
    {
        if (Managers.Input.hAxis != 0 || Managers.Input.vAxis != 0 || Managers.Input.roll || Managers.Input.jump) //키보드 조작시 타겟 지우기
        {
            _lockTarget = null;
        }
        if (_lockTarget != null)
        {


            dir = DestPos(_lockTarget.transform.position);

            if (_lockTarget.layer == (int)Layer.Monster)
            {
                if (playerAttack == null) return;

                if (DistanceAttackPos(dir)) //거리 비교 bool
                {
                    playerAttack.AttackTacrgetSet(_lockTarget);
                    _lockTarget = null;
                    playerAttack.OnAttack();

                    return;
                }
                else
                {
                    MouseMove();
                }
            }
            else if (_lockTarget.layer == (int)Layer.Ground)
            {
                if (playerAttack == null) return;
                playerAttack.AttackTacrgetSet(_lockTarget);
                _lockTarget = null;
                playerAttack.OnAttack();
              
            }
        }
    }

    //사정거리계산 메서드
    bool DistanceAttackPos(Vector3 destpos)
    {
        return Vector3.Distance(transform.position, destpos) <= playerAttack.range;
    }
    //클릭한곳 보는 함수
    void LookHitPoint(RaycastHit hit)
    {
        if (!isRoll && playerAttack != null ? playerAttack.canMove : true)
        {
            Vector3 turnVec = hit.point - transform.position;
            turnVec.y = 0;
            transform.LookAt(transform.position + turnVec);
        }
    }

    #endregion

    //마우스 클릭 이벤트 받는 메서드
    void OnMouseEvent(Define.MouseEvent evt)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        if (raycastHit)
        {
            switch (evt)
            {
                case Define.MouseEvent.PointerDown: 

                    attackType = Define.AttackType.NormalAttack;  // 일반공격
                    LookHitPoint(hit);
                    if (_lockTarget != null) // 이벤트 발생시 비어있지않다면 비어주고 다시 부여
                    {
                        _lockTarget = null;
                    }

                    _lockTarget = hit.collider.gameObject;
      
                    break;

                case Define.MouseEvent.PointerRightDown:

                    attackType = Define.AttackType.SkillAttack;
                    LookHitPoint(hit);
                    if (_lockTarget != null) // 이벤트 발생시 비어있지않다면 비어주고 다시 부여
                    {
                        _lockTarget = null;
                    }
                    _lockTarget = hit.collider.gameObject;

                    break;
            }
            
        }
    }
}
