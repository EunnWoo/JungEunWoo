using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : Status
{
    void Start()
    {
        hp = MAX_HP;

    }



    public override void TakeDamage(Status attacker, float ratio)
    {
        base.TakeDamage(attacker, ratio);
        Managers.UI.ui_MonsterHpbar.ChangeMonsterHit(this);
        
     
    }
    public override void Die()
    {
        base.Die();
        Managers.UI.ui_MonsterHpbar.OffMonsterHpbar();
    }

}
