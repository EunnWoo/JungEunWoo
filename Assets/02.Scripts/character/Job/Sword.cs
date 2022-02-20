using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : PlayerAttack
{
    


    private void Awake()
    {
        attackRate = 0.55f;

    }
    public override void OnAttack()
    {

        base.OnAttack();
    }

    protected override IEnumerator Use()
    {

        animator.SetTrigger("Attack");
        
        yield return null;
    }
}
