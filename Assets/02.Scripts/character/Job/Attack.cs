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
              
    }

    public IEnumerator Arrow()
    {
        animator.SetTrigger("Attack");


        yield return new WaitForSeconds(0.3f);
        animator.SetTrigger("IsReload");

    }
}
