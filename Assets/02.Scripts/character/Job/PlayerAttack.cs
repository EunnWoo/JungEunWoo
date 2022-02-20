using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float range { get; protected set; }
    public bool isAttackReady { get; protected set; } // 공격 가능
    public float attackDelay { get; protected set; } //  딜레이 계산
    public float attackRate { get; protected set; }  // 쿨타임 & 공속
    [SerializeField]
    protected Animator animator;

    PlayerMovement playerMovement;


    private void OnEnable()
    {
        
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.playerAttack = GetComponent<PlayerAttack>(); // 어택을 상속받아 수정되는 값 다시 받아오기
        isAttackReady = true;
    }

    private void Update()
    {
        attackDelay += Time.deltaTime;
        isAttackReady = attackRate < attackDelay;
        Debug.Log(isAttackReady);
        
    }

    public virtual void OnAttack()
    {

        if (isAttackReady && playerMovement.isGround && playerMovement.canMove)
        {
            StopCoroutine(Use());
            StartCoroutine(Use());
            attackDelay = 0;
        }


    }
    protected virtual IEnumerator Use()
    {
        
        yield return null;
    }

}
