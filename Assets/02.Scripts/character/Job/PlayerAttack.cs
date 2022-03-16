using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    public float range { get; protected set; }
    public bool isAttackReady { get; protected set; } // 공격 가능
    public float attackDelay { get; protected set; } //  딜레이 계산
    public float attackRate { get; protected set; }  // 쿨타임 & 공속
    public bool isSkillReady { get; protected set; } // 공격 가능
    public float skillDelay { get; protected set; } //  딜레이 계산
    public float skillRate { get; protected set; }  // 쿨타임 & 공속
    public bool canMove { get; protected set; }
    public bool isAttack { get; protected set; }
    protected Animator animator;

    public GameObject attackTarget { get;  private set; }  //유도탄을 위한 타겟
    PlayerController playerController;
    UI_CoolTime ui_CoolTime;

    private void Update()
    {
        attackDelay += Time.deltaTime;
        isAttackReady = attackRate < attackDelay;

        skillDelay += Time.deltaTime;
        isSkillReady = skillRate < skillDelay;

        canMove = animator.GetBool("canMove");

        ui_CoolTime.SetCoolTimeImage(attackRate, skillRate, attackDelay, skillDelay);
    }

    private void OnEnable()
    {
        animator = GetComponentInChildren<Animator>();
        playerController = Managers.Game.GetPlayer().GetComponent<PlayerController>();
        playerController.playerAttack = GetComponent<PlayerAttack>(); // 어택을 상속받아 수정되는 값 다시 받아오기
        attackDelay = 40;
        skillDelay = 40;
        canMove = true;
        animator.SetBool("canMove",canMove);

        ui_CoolTime = Managers.UI.ShowSceneUI<UI_CoolTime>();

        //job이 결정된후에 job에 따른 skill 이미지 교체 작업
        JobController _jobController = GetComponent<JobController>();

        switch (_jobController.jobstate)// 직업을선택
        {
            case JobInfo.BOW: //JobInfo가 BOW이면
                Debug.Log("궁수");
                UI_CoolTime.ins.SetJobSkillImage(
                    ItemInfo.ins.GetSprite("BOW_01"),
                    ItemInfo.ins.GetSprite("BOW_02"));
                break;

            case JobInfo.SWORD: //JobInfo가 SWORD이면
                Debug.Log("전사");
                UI_CoolTime.ins.SetJobSkillImage(
                    ItemInfo.ins.GetSprite("SWORD_01"),
                    ItemInfo.ins.GetSprite("SWORD_02"));
                break;

            case JobInfo.MAGIC: //JobInfo가 MAGIC이면
                Debug.Log("마법사");
                UI_CoolTime.ins.SetJobSkillImage(
                    ItemInfo.ins.GetSprite("MAGIC_01"),
                    ItemInfo.ins.GetSprite("MAGIC_02"));
                break;
        }
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
            else if(isSkillReady &&playerController.attackType == Define.AttackType.SkillAttack)
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
