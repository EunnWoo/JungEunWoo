using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
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

    public GameObject _locktarget { get; private set; }

    float moveAmount = 0f;
    float moveSpeed = 8f;


    public bool isGround { get; private set; }
    public bool isRoll { get; private set; }

    int _mask = (1 << (int)Layer.Npc) | (1 << (int)Layer.Monster) | (1 << (int)Layer.Ground);

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        dialogManager = GameObject.Find("@Scene").GetComponent<DialogManager>();

        Managers.Mouse.MouseAction -= OnMouseEvent;
        Managers.Mouse.MouseAction += OnMouseEvent;

        //Managers.UI.ShowPopupUI<UI_Button>("UITest");

    }

    void FixedUpdate()
    {

        Move();
        Run();
        Jump();
        Roll();
        OnMouseUpdate();
    }

    #region move
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
    }
    void Run()
    {
        bool runnig = false;
        runnig = Managers.Input.run && moveVec.magnitude != 0f;
        moveSpeed = runnig ? 8f * 1.3f : 8f * 0.8f;
        animator.SetBool("IsRun", runnig);

    }
    void OnMouseUpdate()
    {
        
        if (Managers.Input.hAxis !=0 || Managers.Input.vAxis !=0 || Managers.Input.roll || Managers.Input.jump) //키보드 조작시 타겟 지우기
        {
            _locktarget = null;
        }

        if (_locktarget != null)
        {
            dir = DestPos(_locktarget.transform.position); // 타겟과의 거리 값

            if (_locktarget.layer == (int)Layer.Npc)
            {
                if (dir.magnitude < 0.5f)
                {
                    _locktarget = null;
                    return;
                }
                else
                {
                    MouseMove(); // 마우스로 이동할때의 함수
                }
            }
            else if (_locktarget.layer == (int)Layer.Monster /*|| _locktarget.layer ==(int)Layer.Ground*/)
            {
                if (playerAttack == null) return;

                if (DistanceAttackPos(dir)) //거리 비교 bool
                {
                    playerAttack.AttackTacrgetSet(_locktarget);
                    _locktarget = null;
                    playerAttack.OnAttack();

                    return;
                }
                else
                {

                    MouseMove();
                }
            }
            else if (_locktarget.layer == (int)Layer.Ground)
            {
                playerAttack.OnAttack();
                _locktarget = null;
            }
        }
    }
    public void MoveStop()
    {
        moveVec = Vector3.zero;

    }
    //hitpoint와 거리반환 함수
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



    bool DistanceAttackPos(Vector3 destpos)
    {
        return Vector3.Distance(transform.position, destpos) <= playerAttack.range;
    }


    #endregion

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
                    if (_locktarget != null) // 이벤트 발생시 비어있지않다면 비어주고 다시 부여
                    {
                        _locktarget = null;
                    }

                    _locktarget = hit.collider.gameObject;
      
                    break;

                case Define.MouseEvent.PointerRightDown:

                    attackType = Define.AttackType.SkillAttack;
                    LookHitPoint(hit);
                    if (_locktarget != null) // 이벤트 발생시 비어있지않다면 비어주고 다시 부여
                    {
                        _locktarget = null;
                    }
                    _locktarget = hit.collider.gameObject;
   



                    break;
            }
            
        }
    }
    void LookHitPoint(RaycastHit hit)
    {
        if (!isRoll && playerAttack != null ?playerAttack.canMove :true)
        {
            Vector3 turnVec = hit.point - transform.position;
            turnVec.y = 0;
            transform.LookAt(transform.position + turnVec);
        }
    }

}
