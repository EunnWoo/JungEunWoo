using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : PlayerAttack
{
    


    private void Awake()
    {
        attackRate = 0.2f;
        range = 2f;

    }
    public override void OnAttack()
    {

        base.OnAttack();
    }

    protected override IEnumerator Use()
    {

        animator.SetTrigger("Attack");

        isAttack = false;
        yield return null;
    }
}
