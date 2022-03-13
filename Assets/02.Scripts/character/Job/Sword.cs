using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : PlayerAttack
{


    private float charge;

    private void Awake()
    {
        attackRate = 0.3f;
        skillRate = 10f;
        range = 2.5f;

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

}
