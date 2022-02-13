using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bow : PlayerAttack
{

    public Transform firepos;

    private string arrowobj;
    private ObjPoolManager objpool;
    
    private void Awake()
    {
        GameObject.Find("GameManager").GetComponent<ObjPoolManager>();
        arrowobj = "Arrow";
      // tank =  Managers.Resource.Instantiate("Tank");
    }


    
    


}
