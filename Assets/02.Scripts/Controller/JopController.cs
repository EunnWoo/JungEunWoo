using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System;

public enum JobInfo { COMMON, BOW, SWORD, MAGIC }

public class JopController : MonoBehaviourPun
{
    ObjData objdata;

    Animator animator;

    public GameObject[] Weapons;  // 0 ,1  �ü� 2,3 ���� ���� 4

    public JobInfo jobstate { get; set; }//= JobState.COMMON; // ���� ���� ������ ����
    public string jobstring { get; private set; }

    public Action<string> jobevent = null;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        DontDestroyOnLoad(this);
        
    }


    [PunRPC]
    public void JobChoice()  // ���� Ȱ��ȭ
    { 
        if (jobstate == JobInfo.BOW) // �ü� ����
        {
            jobstring = "Bow";
       
            Weapons[0].SetActive(true);
            Weapons[1].SetActive(true);
        }
        else if (JobInfo.SWORD == jobstate) // ����
        {
            jobstring = "Sword";
            Weapons[2].SetActive(true);
            Weapons[3].SetActive(true);
        }
        else if (JobInfo.MAGIC == jobstate) // ���� ����
        {
            jobstring = "Magic";
            Weapons[4].SetActive(true);
        }
        animator.runtimeAnimatorController = Managers.Resource.Instantiate_Ani(jobstring); // ������ �´� �ִϸ����ͷ� ����
        gameObject.AddComponent(System.Type.GetType(jobstring)); // ������ �´� ��ũ��Ʈ �ο�
    }
  

    public void JobClick()
    {
        photonView.RPC("JobChoice", Photon.Pun.RpcTarget.MasterClient);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Npc")
        {

            objdata =other.gameObject.GetComponent<ObjData>();
            
            jobstate = (JobInfo)objdata.id;
        }
        else if(other.gameObject.tag == "Item")
        {
            //���� �̿ϼ� �Դϴ� ���� ��������
            Debug.Log("@@@@ Eat Item");
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Npc")
        {
           
            jobstate = JobInfo.COMMON;
        }
    }
}
