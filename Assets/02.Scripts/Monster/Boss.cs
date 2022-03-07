using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonsterController
{
    private float moveSpeed = 8f, rotateSpeed = 3f;
    float attackDelay;
    bool isAttackReady;
    float attackRate;
    public override void Init()
    {
        base.Init();
        gameObject.SetActive(false);
        //초기화 하는 함수
    }
    protected override void UpdateMoving(){
        base.UpdateMoving();
    }
    protected override void UpdateAttack()
    {
        base.UpdateAttack();
        attackDelay += Time.deltaTime;
        isAttackReady = attackRate < attackDelay;
        switch(Random.Range(0,2)){
            case 0:
                
            break;
            case 1:
            break;
            case 2:
            break;
        }
    }
}
