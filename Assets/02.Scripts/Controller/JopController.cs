using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum JobInfo { COMMON, BOW, SWORD, MAGIC , STORE}

public class JopController : MonoBehaviour
{
    ObjData objdata;

    Animator animator;

    public GameObject[] Weapons;  // 0 ,1  �ü� 2,3 ���� ���� 4

    public JobInfo jobstate { get; set; }//= JobState.COMMON; // ���� ���� ������ ����
    public string jobstring { get; private set; }


    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        DontDestroyOnLoad(this);
        jobstring = null;
    }

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
        else if (JobInfo.STORE == jobstate) // ��������
        {
            Debug.Log("��������");
            UI_Store.ins.Show_Store();
        }

        if (jobstate != JobInfo.STORE)
        {
            animator.runtimeAnimatorController = Managers.Resource.Instantiate_Ani(jobstring); // ������ �´� �ִϸ����ͷ� ����
            gameObject.AddComponent(System.Type.GetType(jobstring)); // ������ �´� ��ũ��Ʈ �ο�
        }
    }
  


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Npc")
        {

            objdata =other.gameObject.GetComponent<ObjData>();
            
            jobstate = (JobInfo)objdata.id;
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
