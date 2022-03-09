using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;

public class Bow : PlayerAttack, IPunObservable
{ 
   
    private Transform firepos;

    private string arrowobj;
    private Arrow arrow;
    GameObject arrowObj;
    
    private void Awake()
    {
        firepos = Util.FindChild(gameObject, "Firepos", true).transform;
        arrowobj = "Arrow";  
        range = 10.0f;
        attackRate = 0.65f;
        
    }


    protected override IEnumerator Use()
    {
        Debug.Log("Use ����");
        animator.SetBool("Fire",false);
        arrowObj = Managers.Pool.MakeObj(arrowobj);
        Debug.Log("Ǯ����");
        if (arrowObj != null)
        {
            arrow = arrowObj.GetComponent<Arrow>();
            Debug.Log("��ũ��Ʈ ����");
            arrowObj.transform.position = firepos.transform.position;
            arrowObj.transform.rotation = firepos.transform.rotation;
            Debug.Log("������ �����̼� ����");
            arrowObj.SetActive(true);
            Debug.Log("��Ƽ�� Ʈ��");
        }
        animator.SetTrigger("Attack");

        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            arrowObj.transform.position = firepos.transform.position;
            arrowObj.transform.rotation = firepos.transform.rotation;
            if (!Managers.Input.fire)
            {

                arrow.GetComponent<PhotonView>().RPC("FireArrow", RpcTarget.AllBuffered,firepos);
                //arrow.FireArrow(firepos);
                animator.SetBool("Fire", true);
                attackDelay = 0;
                isAttack = false;
             
                break;
            }

            yield return null;
        }
        yield return null;
    }

    protected override IEnumerator Skill()
    {

        animator.SetTrigger("IsSkill");

        for (int i = 0; i <6; i++)
        {
            for (int j = 0; j < 6; j++)
            {
                var arrowObj = Managers.Pool.MakeObj(arrowobj);
         
                if (arrowObj != null)
                {        
                    arrowObj.transform.position = new Vector3
                     (attackTarget.transform.position.x - 3 + i, attackTarget.transform.position.y + 10, attackTarget.transform.position.z - 3 + j);
                    arrowObj.transform.Rotate(0, 0, Random.Range(-65f,-115f));
                    arrowObj.SetActive(true);
                    arrowObj.GetComponent<Rigidbody>().useGravity = true;
   
                }

            }
        }
       


        attackDelay = 0;
        isAttack = false;

        yield return null;
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(arrowObj.transform.position);
            stream.SendNext(arrowObj.transform.rotation);
            stream.SendNext(arrowObj.activeSelf);
        }
        else
        {
            arrowObj.transform.position = (Vector3)stream.ReceiveNext();
            arrowObj.transform.rotation = (Quaternion)stream.ReceiveNext();
            arrowObj.SetActive((GameObject)stream.ReceiveNext());
        }
    }
}
