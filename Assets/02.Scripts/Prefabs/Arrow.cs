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
        DisableArrow();


    }

    public void FireArrow(Transform firepos)
    {
        offset = firepos.position;
        rigid.AddForce(transform.right * speed);

    }
  
    public void DisableArrow() //�����Ÿ� �����
    {
        if ((Vector3.Distance(transform.position, offset) >= 20f))
        {
            gameObject.SetActive(false);

        }
    }
       
    
    private void OnDisable()//������Ʈ ��Ȱ��ȭ
    {
        //�� �ʱ�ȭ
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rigid.Sleep();
    }
}
