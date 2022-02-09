using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour 
{
    static public Attack instance;
    string arrowobj;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private ObjPoolArrow objpool;
    public Transform firepos;
    Arrow arrow; // arrow 스크립트

    void Start()
    {
        
        instance = this;
        animator = GetComponentInChildren<Animator>();
        // target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //   objpool = GetComponentInChildren<ObjPoolArrow>();
        
    }
    
    
    private void Awake()
    {
        arrowobj = "Arrow";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Use();
        }
       // Reload();
        
    }
    public void Use()
    {
       
            if (Job.instance.Bow)
            {
                
                StopCoroutine(Shoot());
                StartCoroutine(Shoot());

            }
     
            if (Job.instance.Sword)
            {
                StopCoroutine(Swing());
                StartCoroutine(Swing());
            }   
    }

    IEnumerator Shoot()
    {
        var arrowObj = objpool.MakeObj(arrowobj);  // 화살 생성
        if (arrowObj != null)
        {
            arrow = arrowObj.GetComponent<Arrow>();
            arrowObj.SetActive(true);
        }
        animator.SetTrigger("Attack");

        yield return null;
        // yield return new WaitForSeconds(1.2f);
        //animator.SetTrigger("IsReload");
        while (true)
        {
            arrowObj.transform.position = firepos.transform.position;
            arrowObj.transform.rotation = firepos.transform.rotation;
            if (Input.GetMouseButtonUp(0))
             {
                Debug.Log("fire");
                animator.SetTrigger("Fire");
                arrow.Fire = true;

                yield return new WaitForSeconds(0.4f);
                    
                    
              
                break;
             }

            yield return null;
        }
        yield return null;
    }
    IEnumerator Swing()
    {
        animator.SetTrigger("Attack");

       // transform.position = target.position;
        yield return null;
    }
    //public void Reload()
    //{
      
    //            animator.SetTrigger("IsReload");
    

    //}
}

