using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    protected GameObject lockTarget { get; private set; }
    public float attackRange { get; protected set; }
    public float scanRange { get; protected set; }


    void Update()
    {
        PlayerScan();
        UpdateMoving();
    }

    protected virtual void PlayerScan()// --> ��ӹ��� monster�� ���� ��ĵ��� �ٸ���  ex) rayhit(���� ����)����Ż ���� Or ���� �÷��̾� distance �� �޾Ƽ� ���� ��ĵ
    {
        //ex RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,������ , tarnsform.forward, scanRange, LayMask.GetMask("Player"));
        //ex 

    }
    protected virtual void UpdateMoving()
    {

    }


}
