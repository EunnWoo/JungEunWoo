using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float range { get; protected set; }
    public bool isAttackReady { get; protected set; } // ���� ����
    public float attackDelay { get; protected set; } //  ������ ���
    public float attackRate { get; protected set; }  // ��Ÿ�� & ����
    [SerializeField]
    protected Animator animator;

    PlayerMovement playerMovement;


    private void OnEnable()
    {
        
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.playerAttack = GetComponent<PlayerAttack>(); // ������ ��ӹ޾� �����Ǵ� �� �ٽ� �޾ƿ���
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
