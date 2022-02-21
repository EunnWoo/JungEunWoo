using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : PlayerAttack
{

    private Transform[] firePosBall;
   
    private float charge;
    private void Awake()
    {
        GameObject[] go = GameObject.FindGameObjectsWithTag("FirePos");
        for(int i=0; i<go.Length; i++)
        {
            firePosBall[i] = go[i].transform;
        }
        range = 10.0f;
        attackRate = 0.55f;

    }

    protected override IEnumerator Use()
    {

     
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            charge += Time.deltaTime;
            Debug.Log(charge);
            if (!Managers.Input.fire)
            {
              
                animator.SetTrigger("Fire");

                break;
            }

            yield return null;
        }
        yield return null;
    }
}
