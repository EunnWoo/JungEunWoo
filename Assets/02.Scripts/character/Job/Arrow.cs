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
        if (Attack.instance.isFire == true) // ���ľ��� ���ٰ� hasArrow �������� �߻� �� ��
        {
            GetComponent<Rigidbody>().AddForce(transform.right * speed);
        }
    }

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
    }

    
       

    private void OnDisable()//������Ʈ ��Ȱ��ȭ
    {
        //�� �ʱ�ȭ
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rigid.Sleep();
    }
}
