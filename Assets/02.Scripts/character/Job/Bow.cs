using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;

public class Bow : PlayerAttack//, IPunObservable
{ 
   
    private Transform firepos;

    private string arrowobj;
    private Arrow arrow;
    GameObject arrowObj;
    
    private void Awake()
    {
        firepos = Util.FindChild(gameObject, "Firepos", true).transform;
        arrowobj = "Arrow";  
        range = 10.0f;
        attackRate = 0.65f;
        
    }


    protected override IEnumerator Use()
    {
        animator.SetBool("Fire",false);
        arrowObj = Managers.Pool.MakeObj(arrowobj);
        if (arrowObj != null)
        {
            arrow = arrowObj.GetComponent<Arrow>();
            arrowObj.transform.position = firepos.transform.position;
            arrowObj.transform.rotation = firepos.transform.rotation;
            arrowObj.SetActive(true);
        }
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            arrowObj.transform.position = firepos.transform.position;
            arrowObj.transform.rotation = firepos.transform.rotation;
            if (playerController.isFire)
            {

                arrow.FireArrow(firepos);
                animator.SetBool("Fire", true);
                attackDelay = 0;
                isAttack = false;
                
                break;
            }

            yield return null;
        }
        yield return null;
    }

    protected override IEnumerator Skill()
    {

        animator.SetTrigger("IsSkill");

        for (int i = 0; i <6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                var arrowObj = Managers.Pool.MakeObj(arrowobj);
         
                if (arrowObj != null)
                {        
                    arrowObj.transform.position = new Vector3
                     (attackTarget.transform.position.x - 3 + i, attackTarget.transform.position.y + 10, attackTarget.transform.position.z - 3 + j);
                    arrowObj.transform.Rotate(0, 0, Random.Range(-65f,-115f));
                    arrowObj.SetActive(true);
                    arrowObj.GetComponent<Rigidbody>().useGravity = true;
   
                }

            }
        }
       


        attackDelay = 0;
        isAttack = false;

        yield return null;
    }

}
