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
        Debug.Log("Onenable");
        animator = GetComponentInChildren<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        playerMovement.playerAttack = GetComponent<PlayerAttack>(); // ������ ��ӹ޾� �����Ǵ� �� �ٽ� �޾ƿ���
        isAttackReady = true;
    }



    public virtual void OnAttack()
    {
 
            StartCoroutine(Use());
            StopCoroutine(Use());


    }
    protected virtual IEnumerator Use()
    {
        
        yield return null;
    }

}
