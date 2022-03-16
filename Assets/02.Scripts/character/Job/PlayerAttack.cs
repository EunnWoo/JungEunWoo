using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    
    public float range { get; protected set; }
    public bool isAttackReady { get; protected set; } // ���� ����
    public float attackDelay { get; protected set; } //  ������ ���
    public float attackRate { get; protected set; }  // ��Ÿ�� & ����
    public bool isSkillReady { get; protected set; } // ���� ����
    public float skillDelay { get; protected set; } //  ������ ���
    public float skillRate { get; protected set; }  // ��Ÿ�� & ����
    public bool canMove { get; protected set; }
    public bool isAttack { get; protected set; }
    protected Animator animator;

    public GameObject attackTarget { get;  private set; }  //����ź�� ���� Ÿ��
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
        playerController.playerAttack = GetComponent<PlayerAttack>(); // ������ ��ӹ޾� �����Ǵ� �� �ٽ� �޾ƿ���
        attackDelay = 40;
        skillDelay = 40;
        canMove = true;
        animator.SetBool("canMove",canMove);

        ui_CoolTime = Managers.UI.ShowSceneUI<UI_CoolTime>();

        //job�� �������Ŀ� job�� ���� skill �̹��� ��ü �۾�
        JobController _jobController = GetComponent<JobController>();

        switch (_jobController.jobstate)// ����������
        {
            case JobInfo.BOW: //JobInfo�� BOW�̸�
                Debug.Log("�ü�");
                UI_CoolTime.ins.SetJobSkillImage(
                    ItemInfo.ins.GetSprite("BOW_01"),
                    ItemInfo.ins.GetSprite("BOW_02"));
                break;

            case JobInfo.SWORD: //JobInfo�� SWORD�̸�
                Debug.Log("����");
                UI_CoolTime.ins.SetJobSkillImage(
                    ItemInfo.ins.GetSprite("SWORD_01"),
                    ItemInfo.ins.GetSprite("SWORD_02"));
                break;

            case JobInfo.MAGIC: //JobInfo�� MAGIC�̸�
                Debug.Log("������");
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
