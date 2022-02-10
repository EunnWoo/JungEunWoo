using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//�������̽�
public class Attack : MonoBehaviour 
{
    static public Attack instance;
    string arrowobj;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private ObjPoolArrow objpool;
    public Transform firepos;
    Arrow arrow; // arrow ��ũ��Ʈ
    public bool Fire { get; private set; }
    void Start()
    {
        
        instance = this;
        animator = GetComponentInChildren<Animator>();
        
        
    }
    
    
    private void Awake()
    {
        arrowobj = "Arrow";
    }


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
        var arrowObj = objpool.MakeObj(arrowobj);  // ȭ�� ����
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

     
        yield return null;
    }

}

