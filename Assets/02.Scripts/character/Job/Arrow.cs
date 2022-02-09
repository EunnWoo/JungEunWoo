using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 500f;
    private Rigidbody rigid;
    private Transform tr;

    private void Update()
    {
        if (Attack.instance.isFire == true) // 고쳐야함 가다가 hasArrow 없어지면 발사 안 됨
        {
            GetComponent<Rigidbody>().AddForce(transform.right * speed);
        }
    }

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
    }

    
       

    private void OnDisable()//오브젝트 비활성화
    {
        //값 초기화
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rigid.Sleep();
    }
}
