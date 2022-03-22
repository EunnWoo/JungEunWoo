using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum JobInfo { COMMON, SWORD, MAGIC , BOW, STORE }

public class JobController : MonoBehaviour
{

    Animator animator;

   // public GameObject[] Weapons;  // 0 ,1  �ü� 2,3 ���� ���� 4

   // public JobInfo jobstate { get; private set; }//= JobState.COMMON; // ���� ���� ������ ����
    public string jobstring { get; private set; }

    UI_CoolTime ui_CoolTime;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        DontDestroyOnLoad(this);
        jobstring = null;
    }


    public void JobChoice(int jobstate)  // ���� Ȱ��ȭ
    { 
        if (jobstate == (int)JobInfo.SWORD) // ����
        {
            jobstring = "Sword";
          //  Weapons[2].SetActive(true);
          //  Weapons[3].SetActive(true);
        }
        else if (jobstate == (int)JobInfo.MAGIC) // ���� ����
        {
            jobstring = "Magic";
         //   Weapons[4].SetActive(true);
        }

        else if (jobstate == (int)JobInfo.BOW) // �ü� ����
        {
            jobstring = "Bow";
       
         //   Weapons[0].SetActive(true);
         //   Weapons[1].SetActive(true);
        }
       
      

        animator.runtimeAnimatorController = Managers.Resource.Load<RuntimeAnimatorController>($"Animator/{jobstring}");
        gameObject.AddComponent(System.Type.GetType(jobstring)); // ������ �´� ��ũ��Ʈ �ο�

    }
  


    //public void changejobstate(GameObject npc)
    //{
    //    jobstate = (JobInfo)npc.GetComponent<ObjData>().id;
    //}

}
