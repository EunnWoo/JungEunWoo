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

        UI_MonsterHpBar ui_MonsterHpBar = FindObjectOfType<UI_MonsterHpBar>();
        if (ui_MonsterHpBar != null)
        {
            ui_MonsterHpBar.ChangeMonsterHit(this);
        }
     
               
            
    }
    public override void Die()
    {
        base.Die();

        UI_MonsterHpBar ui_MonsterHpBar = FindObjectOfType<UI_MonsterHpBar>();
        if (ui_MonsterHpBar != null)
        {
            ui_MonsterHpBar.OffMonsterHpbar();
        }
    }

}
