using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 500f;
    private Rigidbody rigid;
    private Transform tr;
    
    public Vector3 offset { get; private set; }

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        
    }

    private void Update()
    {
        if ((Vector3.Distance(transform.position, offset) >= 20f))//�����Ÿ� �����
        {
            DisableArrow();
        }


    }

    public void FireArrow(Transform firepos)
    {
        offset = firepos.position;
        rigid.AddForce(transform.right * speed);

    }
  
    public void DisableArrow() 
    { 
            gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        tr.position = other.bounds.center;
        Debug.Log("TriggerEnter �߻�");
    }
  
    //private void OnCollisionEnter(Collision collision)
    //{
    //    Vector3 hitpos = collision.contacts[0].point;
    //    tr.position = hitpos;

    //    Invoke("DisableArrow", 3f);
    //}

    private void OnDisable()//������Ʈ ��Ȱ��ȭ
    {
        //�� �ʱ�ȭ
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rigid.Sleep();
    }
}
