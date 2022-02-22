using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    protected GameObject locktarget { get; private set; }
    public float attackRange { get; protected set; }
    public float scanRange { get; protected set; }


    private void Update()
    {
        
    }

    protected virtual void UpdateMovig()
    {

    }

}
