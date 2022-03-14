using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : PlayerAttack
{


    private float charge;
    [SerializeField]
    GameObject[] weapons; //
    
    private void Awake()
    {
        attackRate = 0.3f;
        skillRate = 10f;
        range = 2f;
        weapons = GameObject.FindGameObjectsWithTag("Sword");
        
    }

    protected override IEnumerator Use()
    {

        animator.SetTrigger("Attack");
        isAttack = false;
        attackDelay = 0;
        yield return null;
    }

    protected override IEnumerator Skill()
    {
        animator.SetTrigger("IsSkill");
        UI_SkillTime ui_SkillTime = Managers.UI.ShowPopupUI<UI_SkillTime>();
        ui_SkillTime.Init();
        while (true)
        {
            charge += Time.deltaTime;
            ui_SkillTime.SetImage(charge);
            if (Input.GetMouseButtonUp(1) || charge >=5f)
            {
                Managers.UI.ClosePopupUI(ui_SkillTime);
                animator.SetTrigger("Fire");

                skillDelay = 0;
                charge = 0;
                isAttack = false;

                break;

            }
            yield return null;
        }
        
    }
    protected override void OnHitEvent()
    {
        //if (attackTarget.layer == (int)Layer.Monster)
        //{
        //    Debug.Log("들어옴");
        //}
        //else
        //{

        Vector3 vec = transform.localPosition + transform.forward;
        Collider[] hit = Physics.OverlapSphere(vec, 1.2f, 1 << (int)Layer.Monster);
        for (int i = 0; i < hit.Length; i++)
        {
            Debug.Log("오버랩맞음");
            
        }

        //weapons[0].GetComponent<WeaponSword>().HitOverlep();
        //weapons[1].GetComponent<WeaponSword>().HitOverlep();

        //        }
    }
    public void SkillEvent() // 스킬
    {
        Collider[] hit = Physics.OverlapSphere(transform.position, 2.2f, 1 << (int)Layer.Monster);


        for (int i = 0; i < hit.Length; i++)
        {
            Debug.Log("맞음");
        }
    }

}
