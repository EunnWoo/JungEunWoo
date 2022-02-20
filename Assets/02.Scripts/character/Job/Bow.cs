using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bow : PlayerAttack
{
   
    private Transform firepos;
    private ObjPoolManager objpool;
    private PlayerInput playerInput;
    private string arrowobj;
    private Arrow arrow;
 
    
    private void Awake()
    {
        firepos = GameObject.Find("Firepos").transform;
        objpool = GameObject.Find("GameManager").GetComponent<ObjPoolManager>();
        playerInput = GetComponent<PlayerInput>();
        arrowobj = "Arrow";  
        range = 10.0f;
        attackRate = 1.0f;
        
    }
    public override void OnAttack()
    {

        base.OnAttack();
    }

    protected override IEnumerator Use()
    {
        attackDelay += Time.deltaTime;
        isAttackReady = attackRate < attackDelay;
        var arrowObj = objpool.MakeObj(arrowobj);
        if (arrowObj != null)
        {
            arrow = arrowObj.GetComponent<Arrow>();
            arrowObj.SetActive(true);
        }
        animator.SetTrigger("Attack");

        while (true)
        {
            arrowObj.transform.position = firepos.transform.position;
            arrowObj.transform.rotation = firepos.transform.rotation;
            if (!playerInput.fire)
            {
                arrow.FireArrow(firepos);
                animator.SetTrigger("Fire");
                yield return new WaitForSeconds(1f);


                attackDelay = 0;
                break;
            }

            yield return null;
        }
        yield return null;
    }


}
