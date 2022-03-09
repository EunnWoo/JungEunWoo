using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PlayerAttack : MonoBehaviourPun
{
    
    public float range { get; protected set; }
    public bool isAttackReady { get; protected set; } // ���� ����
    public float attackDelay { get; protected set; } //  ������ ���
    public float attackRate { get; protected set; }  // ��Ÿ�� & ����
    public bool canMove { get; protected set; }
    public bool isAttack { get; protected set; }
    protected Animator animator;

    public GameObject attackTarget { get;  private set; }  //����ź�� ���� Ÿ��
    protected PlayerController playerController;


    private void Update()
    {
        attackDelay += Time.deltaTime;
        isAttackReady = attackRate < attackDelay;
        canMove = animator.GetBool("canMove");

       
    }

    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = Managers.Game.GetPlayer().GetComponent<PlayerController>();
        playerController.playerAttack = GetComponent<PlayerAttack>(); // ������ ��ӹ޾� �����Ǵ� �� �ٽ� �޾ƿ���
        isAttackReady = true;
        canMove = true;
        animator.SetBool("canMove",canMove);
    }
   
    [PunRPC]
    public virtual void OnAttack()
    {
        if (isAttackReady && !playerController.isJump&&!playerController.isRoll && !isAttack)
        {
            isAttack = true;

            if (playerController.attackType == Define.AttackType.NormalAttack)
            {
                StopCoroutine(Use());
                StartCoroutine(Use());
            }
            else if(playerController.attackType == Define.AttackType.SkillAttack)
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
        attackTarget = go;
    }

}
