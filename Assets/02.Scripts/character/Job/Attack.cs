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
                    var arrow = objpool.MakeObj(arrowobj);
                    if (arrow != null)
                    {
                        Debug.Log("발싸 오브젝트풀 입장");
                        arrow.transform.position = firepos.transform.position;
                        arrow.transform.rotation = firepos.transform.rotation;
                        arrow.SetActive(true);

                    }

                    yield return new WaitForSeconds(0.4f);
                    animator.SetTrigger("IsReload");
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
}

