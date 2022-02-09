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
    public bool hasArrow;
    public bool isFire;
    public Transform firepos;


    void Start()
    {
        
        instance = this;
        animator = GetComponentInChildren<Animator>();
        // target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        //   objpool = GetComponentInChildren<ObjPoolArrow>();
        hasArrow = false;
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
        Reload();
        
    }
    public void Use()
    {
       
            if (Job.instance.Bow)
            {
                
                StopCoroutine(Arrow());
                StartCoroutine(Arrow());

            }
     
            if (Job.instance.Sword)
            {
                StopCoroutine(Swing());
                StartCoroutine(Swing());
            }   
    }

    IEnumerator Arrow()
    {
        if (!hasArrow) yield break;

        isFire = true;
        animator.SetTrigger("Attack");

        yield return null;
        // yield return new WaitForSeconds(1.2f);
        //animator.SetTrigger("IsReload");
        while (true)
        {
             if (Input.GetMouseButtonUp(0))
                {
                    Debug.Log("fire");
                    animator.SetTrigger("Fire");
                    

                    yield return new WaitForSeconds(0.4f);
                    
                    
                    hasArrow = false;
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
    public void Reload()
    {
        if (Job.instance.Bow)
        {
            var arrow = objpool.MakeObj(arrowobj);
            if (hasArrow && !isFire)
            {
                arrow.transform.position = firepos.transform.position;
                arrow.transform.rotation = firepos.transform.rotation;
            }
            else if (!hasArrow)
            {
                animator.SetTrigger("IsReload");

                if (arrow != null)
                {
                    Debug.Log("È­»ì SetActive");
                    hasArrow = true;
                    arrow.SetActive(true);
                }
                

            }
        }
    }
}

