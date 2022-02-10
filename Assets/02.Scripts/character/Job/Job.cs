using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum JobInfo { COMMON, BOW, SWORD, MAGIC }
//�θ�
public class Job : MonoBehaviour
{
    [SerializeField]
    
  
    private Animator animator;  //������ �÷��̾� �ִϸ����� ����
    public GameObject[] Weapons;  // 0 ,1  �ü� 2,3 ���� ���� 4
    
    public JobInfo jobstate { get; set; }//= JobState.COMMON; // ���� ���� ������ ����

    public JobInfo jobFix { get; private set; }
       
    
    //public bool Bow { get; private set; }
    //public bool Sword { get; private set; }
    //public bool Magic { get; private set; }

   // public event Action SelectJob;
    ObjData objdata;
    

    static public Job instance;

    void Awake()
    {
        animator =GetComponentInChildren<Animator>();              //GetComponent<Animator>();
        instance = this;
    }

    private void Update()
    {
       
    }

    public void JobChoice()  // ���� Ȱ��ȭ
    {
        if (jobFix != JobInfo.COMMON) return;
        if(JobInfo.BOW == jobstate) // �ü� ����
        {
            
            Weapons[0].SetActive(true);
          //  Weapons[1].SetActive(true);
            Weapons[5].SetActive(true);
            
        }
        else if (JobInfo.SWORD == jobstate) // �ü� ����
        {     
            Weapons[2].SetActive(true);
            Weapons[3].SetActive(true);
        }
        else if (JobInfo.MAGIC == jobstate) // ���� ����
        { 
            Weapons[4].SetActive(true);
        }
        animator.SetInteger("JobState", (int)jobstate);
        jobFix = jobstate;
        
    }

   


}
