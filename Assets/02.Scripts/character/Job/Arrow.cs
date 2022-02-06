using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 500f;
    private Rigidbody rigid;
    private Transform tr;



    private void Awake()
    {
        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
    }

    private void OnEnable() //오브젝트 활성화
    {
        GetComponent<Rigidbody>().AddForce(transform.right * speed);

    }
    private void OnDisable()//오브젝트 비활성화
    {
        //값 초기화
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rigid.Sleep();
    }
}
