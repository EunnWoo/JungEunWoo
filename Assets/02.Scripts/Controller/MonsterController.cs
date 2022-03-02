using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    
    public float attackRange { get; protected set; }
    public float scanRange { get; protected set; }
    public override void Init()
    {
        //초기화 하는 함수

        

    }


    protected virtual void PlayerScan()// --> 상속받은 monster들 마다 스캔방식 다르게  ex) rayhit(보는 방향)골드메탈 참고 Or 적과 플레이어 distance 값 받아서 범위 스캔
    {
        //ex RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,반지름 , tarnsform.forward, scanRange, LayMask.GetMask("Player"));
        //ex 

    }

    protected override void UpdateMoving()
    {

    }
    protected override void UpdateAttack()
    {

    }

}
