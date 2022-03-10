using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Arrow : MonoBehaviourPunCallbacks//, IPunObservable
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


    [PunRPC]
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
    //        // scale ���� ������ ��� parent
    //    GameObject sharedParent = new GameObject("Father");
    //    sharedParent.transform.position = otherObject.transform.position;
    //    sharedParent.transform.rotation = otherObject.transform.rotation;
    //    sharedParent.transform.SetParent(otherObject.gameObject.transform);

    //    // ������ ȭ�� ����
    //    GameObject newArrow = Managers.Resource.Instantiate("Arrow");
    //    newArrow.transform.SetParent(sharedParent.transform, true);
    //    //2�� �� �Ҹ�
    //    Destroy(newArrow, 2);
    //}

    private void OnDisable()//������Ʈ ��Ȱ��ȭ
    {
        //�� �ʱ�ȭ
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rigid.Sleep();


    }

    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(transform.position);
    //        stream.SendNext(transform.rotation);
    //        stream.SendNext(gameObject.activeSelf);
    //        stream.SendNext(rigid);
    //    }
    //    else
    //    {
    //        transform.position = (Vector3)stream.ReceiveNext();
    //        transform.rotation = (Quaternion)stream.ReceiveNext();
    //        gameObject.SetActive((GameObject)stream.ReceiveNext());
    //        rigid = (Rigidbody)stream.ReceiveNext();
    //    }
    //}
}
