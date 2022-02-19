using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : PlayerAttack
{

    private void Awake()
    {


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
