using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bow : PlayerAttack
{
   
    private Transform firepos;
    private float charge;
    private string arrowobj;
    private Arrow arrow;
 
    
    private void Awake()
    {
        firepos = Util.FindChild(gameObject, "Firepos", true).transform;
        arrowobj = "Arrow";  
        range = 10.0f;
        skillRate = 10f;
        attackRate = 0.65f;
        
    }


    protected override IEnumerator Use()
    {
        UI_SkillTime ui_SkillTime = Managers.UI.ShowPopupUI<UI_SkillTime>();
        ui_SkillTime.Init();

        animator.SetBool("Fire",false);
        animator.SetTrigger("Attack");

        var arrowObj = Managers.Pool.MakeObj(arrowobj);
        if (arrowObj != null)
        {
            arrow = arrowObj.GetComponent<Arrow>();        
        }

        yield return new WaitForSeconds(0.2f);

        arrowObj.SetActive(true);

        while (true)
        {
            ui_SkillTime.SetImage(charge);

            arrowObj.transform.position = firepos.transform.position;
            arrowObj.transform.rotation = firepos.transform.rotation;

            charge += Time.deltaTime;
            if(charge>2)
            {
                arrow.chargeParticle.SetActive(true);
            }

            if (!Managers.Input.fire)
            {
                if(charge>=5f)
                {
                    arrow.fireParticle.SetActive(true);
                }
                arrow.FireArrow(firepos);
                arrow.chargeParticle.SetActive(false); // 날아갈땐 꺼주기

                Managers.UI.ClosePopupUI(ui_SkillTime);
                animator.SetBool("Fire", true);
                attackDelay = 0;
                isAttack = false;
                charge = 0;
                break;
            }

            yield return null;
        }
        yield return null;
    }

    protected override IEnumerator Skill()
    {
        animator.SetBool("Fire", false);
        animator.SetTrigger("IsSkill");

        while(!animator.GetBool("Fire")) // false 라면 계속 들어감 true 되어야 입장
        {
            yield return null;
        }

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
        skillDelay = 0;
        isAttack = false;
        
        yield return null;
    }
    protected override void OnHitEvent()
    {
        //if (attackTarget.layer == (int)Layer.Monster)
        //{
        //    Debug.Log("들어옴");
        //}
        //else
        //{

        //    Collider[] colls = gameObject.GetComponentsInChildren<Collider>();
        //    foreach (Collider coll in colls)
        //    {
        //        Debug.Log(coll);
        //        coll.enabled = true;
        //    }

        //}
    }
}
