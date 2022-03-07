using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : PlayerAttack
{



    private void Awake()
    {
        attackRate = 0.4f;
        range = 2.5f;

    }

    protected override IEnumerator Use()
    {

        animator.SetTrigger("Attack");
        Debug.Log("swordattack");
        isAttack = false;
        attackDelay = 0;
        yield return null;
    }

    protected override IEnumerator Skill()
    {
        animator.SetTrigger("IsSkill");

        if(Input.GetMouseButtonUp(1))
        {
            animator.SetTrigger("Fire");
        }


        yield return null;
    }
}
