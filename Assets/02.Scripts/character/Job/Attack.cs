using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    static public Attack instance;
    public GameObject arrow;
    [SerializeField]
    private Animator animator;
    void Start()
    {
        instance = this;
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Use();
        }
    }
    public void Use()
    {
        if(Job.instance.Bow)
        {
            StopCoroutine(Arrow());
            StartCoroutine(Arrow());
                
        }
        if(Job.instance.Sword)
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

    }
    IEnumerator Swing()
    {
        animator.SetTrigger("Attack");
        yield return null;
    }
}

