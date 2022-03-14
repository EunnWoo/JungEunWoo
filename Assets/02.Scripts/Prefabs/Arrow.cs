using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 500f;
    private Rigidbody rigid;
    private Transform tr;

    Vector3 offset;
    Vector3 hitpos;

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
           
            Invoke("DisableArrow", 3f);
        }
        
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    GameObject otherObject = collision.gameObject;
    //        // scale 변경 방지용 쿠션 parent
    //    GameObject sharedParent = new GameObject("Father");
    //    sharedParent.transform.position = otherObject.transform.position;
    //    sharedParent.transform.rotation = otherObject.transform.rotation;
    //    sharedParent.transform.SetParent(otherObject.gameObject.transform);

    //    // 고정될 화살 생성
    //    GameObject newArrow = Managers.Resource.Instantiate("Arrow");
    //    newArrow.transform.SetParent(sharedParent.transform, true);
    //    //2초 후 소멸
    //    Destroy(newArrow, 2);
    //}

    private void OnDisable()//오브젝트 비활성화
    {
        //값 초기화
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rigid.Sleep();


    }

    public void HitMonster()
    {
        Collider[] hit = Physics.OverlapSphere(new Vector3(1.2f, 0f, 0f), 0.2f, 1 << (int)Layer.Monster);

        
        for (int i = 0; i < hit.Length; i++)
        {
            Debug.Log("맞음");
        }
    }
    
}
