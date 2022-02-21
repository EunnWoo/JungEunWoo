using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   
    private Transform target;
    public Vector3 offset;

    void Start()
    {
        target = GameObject.FindWithTag("Player").GetComponent<Transform>();
        DontDestroyOnLoad(this);



    }
    void Update()
    {
        transform.position = target.position + offset;
        
        
        
    }

}
