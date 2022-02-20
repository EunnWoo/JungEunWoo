using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bow : PlayerAttack
{
   
    private Transform firepos;

    private string arrowobj;
    
    private ObjPoolManager objpool;
    Arrow arrow;
 
    


    

    private void Awake()
    {
     
       
        firepos = GameObject.Find("Firepos").transform;
        objpool = GameObject.Find("GameManager").GetComponent<ObjPoolManager>();
        
        arrowobj = "Arrow";  
        range = 10.0f;
        
    }
    public override void OnAttack()
    {

        base.OnAttack();
    }

    protected override IEnumerator Use()
    {
        Debug.Log("Use¿‘¿Â");


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
            if (Input.GetMouseButtonUp(0))
            {
                arrow.FireArrow(firepos);
                Debug.Log("fire");
                animator.SetTrigger("Fire");

                yield return new WaitForSeconds(1f);

                

                break;
            }

            yield return null;
        }
        yield return null;
    }


}
