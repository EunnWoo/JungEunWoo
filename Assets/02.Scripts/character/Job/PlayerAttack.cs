using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

    public float del { get; protected set; }
    public float range { get; protected set; }
    

    
    
    public virtual void OnAttack()
    {
     //   Debug.Log("OnAttack����");
        
            StartCoroutine(Use());
            StopCoroutine(Use());
            
            
      


    }
    protected virtual IEnumerator Use()
    {
        Debug.Log("�θ� �ڷ�ƾ");
        yield return null;
    }

}
