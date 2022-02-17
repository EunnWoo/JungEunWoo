using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bow : PlayerAttack
{

    public Transform firepos;

    private string arrowobj;
    private Animator animator;
    private ObjPoolManager objpool;
    public static Bow instance;
    Arrow arrow;
    public bool canFire;
    


    //private void Awake()
    //{
    //    GameObject.Find("GameManager").GetComponent<ObjPoolManager>();
    //    animator = GetComponentInChildren<Animator>();
    //    arrowobj = "Arrow";
    //    instance = this;
    //    Fire = false;
    //  // tank =  Managers.Resource.Instantiate("Tank");
    //}

    private void OnEnable()
    {
        objpool = GameObject.Find("GameManager").GetComponent<ObjPoolManager>();
        animator = GetComponentInChildren<Animator>();
        arrowobj = "Arrow";
        instance = this;
        canFire = true;
        range = 10.0f;
        
    }
    private void Update()
    {
        OnAttack();

    }
    protected override void OnAttack()
    {
        
        base.OnAttack();
    }
    
    protected override IEnumerator Use()
    {
       

        
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
