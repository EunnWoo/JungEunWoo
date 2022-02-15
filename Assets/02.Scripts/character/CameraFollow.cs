using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   
    public Transform target;
    public Vector3 offset;
    private DialogManager manager;
   // private bool RightClick;
    void Start()
    {
        manager = GameObject.Find("@Managers").GetComponent<DialogManager>();
        DontDestroyOnLoad(this);
       
    }
    
    void Update()
    {
        transform.position = target.position + offset;
    }

}
