using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float range { get; protected set; }
    public bool isAttackReady { get; protected set; } // 공격 가능
    public float attackDelay { get; protected set; } //  딜레이 계산
    public float attackRate { get; protected set; }  // 쿨타임 & 공속
    public bool canMove { get; protected set; }
    public bool isAttack { get; protected set; }
    protected Animator animator;

    public GameObject attackTarget { get; private set; }
    PlayerController playerController;
   


    int _mask = (1 << (int)Layer.Monster) | (1 << (int)Layer.Ground);


    private void OnEnable()
    {

        animator = GetComponentInChildren<Animator>();
        playerController = GetComponent<PlayerController>();
        playerController.playerAttack = GetComponent<PlayerAttack>(); // 어택을 상속받아 수정되는 값 다시 받아오기
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

        if (isAttackReady && playerController.isGround &&!playerController.isRoll && !isAttack)
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
        if (raycastHit) //레이어가 _mask 값에 있다면
        {
            LookHitPoint(hit);  // 마우스 클릭한곳 바라보기
            if (hit.collider.gameObject.layer == (int)Layer.Monster)
            {
                switch (evt)
                {
                    case MouseEvent.PointerDown:
                    case MouseEvent.Press:
                        if (DistanceAttackPos(hit)) // 클릭한 지점과의 거리가 range보다 짧을경우 공격.
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
        if (!playerController.isRoll && canMove )
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
