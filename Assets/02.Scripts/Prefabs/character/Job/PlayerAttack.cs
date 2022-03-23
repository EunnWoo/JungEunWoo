using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : BaseController
{
    
    public float range { get; protected set; }
  
    public bool canMove { get; protected set; }
    public bool isAttack { get; protected set; }
    public float attackRatio { get; protected set; }
    public float skillRatio { get; protected set; }
    public GameObject attackTarget { get; private set; }  //유도탄을 위한 타겟


    protected Animator animator;
    PlayerController playerController;
    UI_CoolTime ui_CoolTime;


    PartInfo _partInfo;

    protected override void UpdateAttack() 
    {
      

        canMove = animator.GetBool("canMove");

        ui_CoolTime.SetCoolTimeImage(attackRate, skillRate, attackDelay, skillDelay);
    }

    public override void Init()
    {

        animator = GetComponent<Animator>();
     
        playerController = Managers.Game.GetPlayer().GetComponent<PlayerController>();
        playerController.playerAttack = GetComponent<PlayerAttack>(); // 어택을 상속받아 수정되는 값 다시 받아오기

        attackDelay = 40;
        skillDelay = 40;
        canMove = true;
        animator.SetBool("canMove", canMove);


        ui_CoolTime = Managers.UI.ShowSceneUI<UI_CoolTime>();
        ui_CoolTime.Init();
        ui_CoolTime.SetSkiilImage(GetComponent<JobController>().jobstring);

    }

    public virtual void OnAttack()
    {

        if ( !playerController.isJump&&!playerController.isRoll && !isAttack)
        {
            
            if (isAttackReady && playerController.attackType == Define.AttackType.NormalAttack)
            {
                isAttack = true;
                StopCoroutine("Use");
                StartCoroutine("Use");
            }
            else if(isSkillReady &&playerController.attackType == Define.AttackType.SkillAttack && canMove)
            {
                isAttack = true;
                StopCoroutine("Skill");
                StartCoroutine("Skill");
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
    protected virtual void OnHitEvent()
    {
    }
}
