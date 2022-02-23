using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : PlayerAttack
{
    [SerializeField]
    private GameObject[] fireBallPos;
    private ObjPoolManager objpool;

    private int hasFireBall;
    private string fireballobj;
    private float charge;

    private void Awake()
    {
        objpool = GameObject.Find("GameManager").GetComponent<ObjPoolManager>();

        fireBallPos = GameObject.FindGameObjectsWithTag("FirePos");
        fireballobj = "FireBall";
        hasFireBall = 0;

        range = 10.0f;
        attackRate = 0.8f;

    }

    protected override IEnumerator Use()
    {

     
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.2f);


        while (true)
        {

            charge += Time.deltaTime;
            
            if (hasFireBall < 5? charge >= hasFireBall : false) // 차징만큼 담기
            { 
                var fireBallObj = objpool.MakeObj(fireballobj);
                if (fireBallObj != null)
                {
                    fireBallObj.transform.position = fireBallPos[hasFireBall].transform.position;
               
                    fireBallObj.SetActive(true);
                    hasFireBall++;
                }
               
            }

            if (!Managers.Input.fire) // 발사
            {
                animator.SetTrigger("Fire");
                isAttack = false;
                attackDelay = 0;
                hasFireBall = 0;
                charge = 0;

                break;
            }

            yield return null;
        }
        yield return null;
    }
}
