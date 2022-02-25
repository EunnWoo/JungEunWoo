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
        if ((Vector3.Distance(transform.position, offset) >= 20f))//사정거리 벗어나면
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

        if (other.tag == "Monster" || other.tag == "Ground") 
        {
            rigid.Sleep();
            rigid.useGravity = false;
            transform.position = other.ClosestPointOnBounds(transform.position)+new Vector3(0,0.5f,0);
            
            Debug.Log(other.ClosestPointOnBounds(transform.position));
            //Invoke("DisableArrow", 3f);
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {

        Vector3 hitpos = collision.contacts[0].point;

        tr.position = hitpos;

        Invoke("DisableArrow", 3f);
    }

    private void OnDisable()//오브젝트 비활성화
    {
        //값 초기화
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        
        rigid.Sleep();
    }
}
