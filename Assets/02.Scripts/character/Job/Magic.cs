using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic : PlayerAttack
{

    private Transform firepos;
    
    private PlayerInput playerInput;
   

    private void Awake()
    {
        firepos = GameObject.Find("Firepos").transform;
        
        playerInput = GetComponent<PlayerInput>();
        
        range = 10.0f;
        attackRate = 0.55f;

    }

    protected override IEnumerator Use()
    {

     
        animator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
       
            if (!playerInput.fire)
            {
              
                animator.SetTrigger("Fire");

                break;
            }

            yield return null;
        }
        yield return null;
    }
}
