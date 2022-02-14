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
    public bool Fire;
    
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
        Fire = true;
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
        if (!Fire)
        {
            Debug.Log("ÄÚ·çÆ¾ Å»Ãâ");
            yield break;
        }

        Fire = false;
        var arrowObj = objpool.MakeObj(arrowobj);
        if (arrowObj != null)
        {
            arrow = arrowObj.GetComponent<Arrow>();
            arrowObj.SetActive(true);
        }
        animator.SetTrigger("Attack");

        yield return null;

        while (true)
        {
            arrowObj.transform.position = firepos.transform.position;
            arrowObj.transform.rotation = firepos.transform.rotation;
            if (Input.GetMouseButtonUp(0))
            {
                Debug.Log("fire");
                animator.SetTrigger("Fire");
                Fire = true;

                yield return new WaitForSeconds(2f);



                break;
            }

            yield return null;
        }
        yield return null;
    }


}
