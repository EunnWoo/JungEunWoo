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

    public GameObject attackTarget { get;  private set; }  //유도탄을 위한 타겟
    PlayerController playerController;


    private void Update()
    {
        attackDelay += Time.deltaTime;
        isAttackReady = attackRate < attackDelay;
        canMove = animator.GetBool("canMove");

        
       
    }

    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        playerController.playerAttack = GetComponent<PlayerAttack>(); // 어택을 상속받아 수정되는 값 다시 받아오기
        isAttackReady = true;
        canMove = true;
        animator.SetBool("canMove",canMove);
    }

    public virtual void OnAttack()
    {
        if (isAttackReady && playerController.isGround &&!playerController.isRoll && canMove )
        {
            isAttack = true;

            if (playerController.attackType == AttackType.NormalAttack)
            {
                StopCoroutine(Use());
                StartCoroutine(Use());
            }
            else if(playerController.attackType ==AttackType.SkillAttack)
            {
                
                StopCoroutine(Skill());
                StartCoroutine(Skill());
            }
        }
    }
    protected virtual IEnumerator Use()
    {

        yield return null;
    }
    protected virtual IEnumerator Skill()
    {

        yield return null;
    }

    public void AttackTacrgetSet(GameObject go)
    {
        Debug.Log("go"+go);
        attackTarget = go;
        Debug.Log("attacktarget" +attackTarget);

    }

    



}
