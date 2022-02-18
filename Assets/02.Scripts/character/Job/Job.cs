using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum JobInfo { COMMON, BOW, SWORD, MAGIC }
//�θ�
public class Job : MonoBehaviour
{
   
    
  
    //private Animator animator;  //������ �÷��̾� �ִϸ����� ����
    public GameObject[] Weapons;  // 0 ,1  �ü� 2,3 ���� ���� 4
    [SerializeField]
    public JobInfo jobstate { get; set; }//= JobState.COMMON; // ���� ���� ������ ����
    public JobInfo jobFix { get; private set; }

    static public Job instance;

    void Awake()
    {
    //    animator =GetComponentInChildren<Animator>();              //GetComponent<Animator>();
        instance = this;
        jobFix = JobInfo.COMMON;
        
    }

    public void JobChoice()  // ���� Ȱ��ȭ
    {
      
        if (jobFix != JobInfo.COMMON) return;
        if(jobstate == JobInfo.BOW) // �ü� ����
        {
          
            gameObject.GetComponent<Bow>().enabled = true;
            Weapons[0].SetActive(true);
            Weapons[5].SetActive(true);

            
        }
        else if (JobInfo.SWORD == jobstate) // �ü� ����
        {
            gameObject.GetComponent<Sword>().enabled = true;
            Weapons[2].SetActive(true);
            Weapons[3].SetActive(true);
        }
        else if (JobInfo.MAGIC == jobstate) // ���� ����
        {
            gameObject.GetComponent<Magic>().enabled = true;
            Weapons[4].SetActive(true);
        }
       
        jobFix = jobstate;
        
    }

   


}
