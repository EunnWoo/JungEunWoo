using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : BaseController
{

    public Define.AttackType attackType { get; private set; }

    Rigidbody rigid;
    Animator animator;
    [HideInInspector]
    public PlayerAttack playerAttack;

    Vector3 moveVec = Vector3.zero;
    Vector3 dir = Vector3.zero;
    Vector3 rollVec = Vector3.zero;

    float moveAmount = 0f;
    float moveSpeed = 8f;
    public bool isJump { get; private set; }
    public bool isRoll { get; private set; }

    int _mask = (1 << (int)Layer.Item)
        | (1 << (int)Layer.Npc)
        | (1 << (int)Layer.Monster)
        | (1 << (int)Layer.Ground);

    public override void Init()
    {
        if (playerAttack == null) playerAttack = GetComponent<PlayerAttack>();
        rigid = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();

        Managers.Mouse.MouseAction -= OnMouseEvent;
        Managers.Mouse.MouseAction += OnMouseEvent;

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
        if (isRoll) moveVec = rollVec; //������ ������ȯ ����

        if (playerAttack != null)  // ������ �̵� ����
        {
            if (!playerAttack.canMove) MoveStop();
        }

        float m = Mathf.Abs(Managers.Input.hAxis) + Mathf.Abs(Managers.Input.vAxis);
        moveAmount = Mathf.Clamp01(m);


        transform.LookAt(transform.position + moveVec);


        transform.position += moveVec * moveSpeed * Time.deltaTime;
        animator.SetFloat("Move", moveAmount, 0.2f, Time.deltaTime);

        //���콺�� �̵�
        if (_lockTarget != null)
        {
            dir = DestPos(_lockTarget.transform.position); // Ÿ�ٰ��� �Ÿ� ��

            if (_lockTarget.layer == (int)Layer.Npc || _lockTarget.layer == (int)Layer.Item)
            {
                if (dir.magnitude < 0.5f) //�ɸ��Ͱ� Ÿ���̶� 0.5���� �̳��� ������
                {
                    if (_lockTarget.layer == (int)Layer.Item) //Ÿ���� ���̾ �������̸�
                    {
                        Debug.Log("@@@ Eat item");
                        TakeItem(_lockTarget);
                    }
                    _lockTarget = null;
                    return;
                }
                else
                {
                    MouseMove(); // ���콺�� �̵��Ҷ��� �Լ�
                }
            }
        }
    }

    private void TakeItem(GameObject _itemGO)
    {
        if (_itemGO != null) //      �������� ������
        {
            //�������� : Itemcode, Itemname���
            ItemPickUp _pick = _itemGO.GetComponent<ItemPickUp>(); //������ ������Ʈ�ȿ� ItemPickUp ��ũ��Ʈ��ã�Ƽ�
            if (_pick)
            {
                Debug.Log(_pick.itemData.itemName + " ȹ���߽��ϴ�");

                //�κ��丮�� �־��ֱ�
                bool _bGet =  UI_Inventory.ins.AddItemData(_pick.itemData);
                if (_bGet)
                {
                    _pick.ClearDestroy();//������Ʈ �ֿ�� �ʵ忡 �ֿ�������� ��������ϱ�
                }
                else
                {
                    Debug.Log("������â�� ����á���ϴ�");
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
    //hitpoint�� �Ÿ���ȯ �Լ�


    #endregion
    #region jump
    private void Jump()
    {
        Jumptf();
        if (Managers.Input.jump && !isJump && !Managers.Input.roll && rigid.velocity.y==0)
        {
            isJump = true;
            animator.SetBool("IsJump", true);
            animator.SetTrigger("DoJump");
            rigid.AddForce(Vector3.up * 17, ForceMode.Impulse);

        }
    }
    private void Jumptf()
    {
        if (rigid.velocity.y < -1f)
        {
            animator.SetBool("IsFall", true);
            animator.SetBool("IsJump", true);
            isJump = true;
        }

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isJump = false;
            animator.SetBool("IsJump", false);
            animator.SetBool("IsFall", false);
        }

    }
    #endregion
    #region roll
    private void Roll()
    {
        if (!isJump && moveAmount != 0 && Managers.Input.roll)
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
        if (Managers.Input.hAxis != 0 || Managers.Input.vAxis != 0 || Managers.Input.roll || Managers.Input.jump) //Ű���� ���۽� Ÿ�� �����
        {
            _lockTarget = null;
        }
        if (_lockTarget != null)
        {


            dir = DestPos(_lockTarget.transform.position);

            if (_lockTarget.layer == (int)Layer.Monster)
            {
                if (playerAttack == null) return;

                if (DistanceAttackPos(dir)) //�Ÿ� �� bool
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

    //�����Ÿ���� �޼���
    bool DistanceAttackPos(Vector3 destpos)
    {
        
        return destpos.magnitude <= playerAttack.range;
    }
    //Ŭ���Ѱ� ���� �Լ�
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

    //���콺 Ŭ�� �̺�Ʈ �޴� �޼���
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

                    attackType = Define.AttackType.NormalAttack;  // �Ϲݰ���
                    LookHitPoint(hit);
                    if (_lockTarget != null) // �̺�Ʈ �߻��� ��������ʴٸ� ����ְ� �ٽ� �ο�
                    {
                        _lockTarget = null;
                    }

                    _lockTarget = hit.collider.gameObject;

                    break;

                case Define.MouseEvent.PointerRightDown:

                    attackType = Define.AttackType.SkillAttack;
                    LookHitPoint(hit);
                    if (_lockTarget != null) // �̺�Ʈ �߻��� ��������ʴٸ� ����ְ� �ٽ� �ο�
                    {
                        _lockTarget = null;
                    }
                    _lockTarget = hit.collider.gameObject;

                    break;
            }

        }
    }
}
