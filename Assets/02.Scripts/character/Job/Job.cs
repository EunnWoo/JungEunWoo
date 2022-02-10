using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum JobInfo { COMMON, BOW, SWORD, MAGIC }
//부모
public class Job : MonoBehaviour, IAttack
{
    [SerializeField]
    
  
    private Animator animator;  //직업에 플레이어 애니메이터 변경
    public GameObject[] Weapons;  // 0 ,1  궁수 2,3 전사 법사 4
    
    public JobInfo jobstate { get; set; }//= JobState.COMMON; // 현재 전직 가능한 직업
    public JobInfo jobFix { get; private set; }

    static public Job instance;

    void Awake()
    {
        animator =GetComponentInChildren<Animator>();              //GetComponent<Animator>();
        instance = this;
    }

    public void JobChoice()  // 직업 활성화
    {
        if (jobFix != JobInfo.COMMON) return;
        if(JobInfo.BOW == jobstate) // 궁수 전직
        {
            gameObject.GetComponent<Bow>().enabled = true;
            Weapons[0].SetActive(true);
          //  Weapons[1].SetActive(true);
            Weapons[5].SetActive(true);
            
        }
        else if (JobInfo.SWORD == jobstate) // 궁수 전직
        {
            gameObject.GetComponent<Sword>().enabled = true;
            Weapons[2].SetActive(true);
            Weapons[3].SetActive(true);
        }
        else if (JobInfo.MAGIC == jobstate) // 법사 전직
        {
            gameObject.GetComponent<Magic>().enabled = true;
            Weapons[4].SetActive(true);
        }
        animator.SetInteger("JobState", (int)jobstate);
        jobFix = jobstate;
        
    }

    public virtual void IsAttack()
    {

    }
   


}
