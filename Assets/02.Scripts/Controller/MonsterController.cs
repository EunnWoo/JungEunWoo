using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : BaseController
{
    
    public float attackRange { get; protected set; }
    public float scanRange { get; protected set; }
    public override void Init()
    {
        //�ʱ�ȭ �ϴ� �Լ�

        

    }


    protected virtual void PlayerScan()// --> ��ӹ��� monster�� ���� ��ĵ��� �ٸ���  ex) rayhit(���� ����)����Ż ���� Or ���� �÷��̾� distance �� �޾Ƽ� ���� ��ĵ
    {
        //ex RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,������ , tarnsform.forward, scanRange, LayMask.GetMask("Player"));
        //ex 

    }

    protected override void UpdateMoving()
    {

    }
    protected override void UpdateAttack()
    {

    }




}
