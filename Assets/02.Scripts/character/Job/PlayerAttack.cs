using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float range { get; protected set; }
    public bool isAttackReady { get; protected set; } // ���� ����
    public float attackDelay { get; protected set; } //  ������ ���
    public float attackRate { get; protected set; }  // ��Ÿ�� & ����
    public bool canMove { get; protected set; }
    public bool isAttack { get; protected set; }
    protected Animator animator;

    public GameObject attackTarget { get; private set; }
    PlayerMovement playerMovement;
   


    int _mask = (1 << (int)Layer.Monster) | (1 << (int)Layer.Ground);


    private void OnEnable()
    {

        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.playerAttack = GetComponent<PlayerAttack>(); // ������ ��ӹ޾� �����Ǵ� �� �ٽ� �޾ƿ���
        isAttackReady = true;
        canMove = true;
        animator.SetBool("canMove",canMove);
        Managers.Mouse.MouseAction -= MouseEventAttack;
        Managers.Mouse.MouseAction += MouseEventAttack;
    }

    private void Update()
    {
       
        attackDelay += Time.deltaTime;
        isAttackReady = attackRate < attackDelay;
        canMove = animator.GetBool("canMove");
            
    }

    public virtual void OnAttack()
    {

        if (isAttackReady && playerMovement.isGround &&!playerMovement.isRoll && !isAttack)
        {
            isAttack = true;
            StopCoroutine(Use());
            StartCoroutine(Use());
        }


    }
    protected virtual IEnumerator Use()
    {

        yield return null;
    }

    void MouseEventAttack(MouseEvent evt)
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);
        if (raycastHit) //���̾ _mask ���� �ִٸ�
        {
            LookHitPoint(hit);  // ���콺 Ŭ���Ѱ� �ٶ󺸱�
            if (hit.collider.gameObject.layer == (int)Layer.Monster)
            {
                switch (evt)
                {
                    case MouseEvent.PointerDown:
                    case MouseEvent.Press:
                        if (DistanceAttackPos(hit)) // Ŭ���� �������� �Ÿ��� range���� ª����� ����.
                        {
                            attackTarget = hit.collider.gameObject;
                            OnAttack();
                            
                        }
                        
                        break;
                }
            }
            else if (hit.collider.gameObject.layer == (int)Layer.Ground)
            {
                switch (evt)
                {
                    case MouseEvent.PointerDown:
                    case MouseEvent.Press:
                        OnAttack();

                        break;
                }
            }

        }
    }

    void LookHitPoint(RaycastHit hit)
    {
        if (!playerMovement.isRoll && canMove )
        {
            Vector3 turnVec = hit.point - transform.position;
            turnVec.y = 0;
            transform.LookAt(transform.position + turnVec);
        }
    }
    bool DistanceAttackPos(RaycastHit hit)
    {
        return Vector3.Distance(transform.position , hit.point) < range;
    }
}
