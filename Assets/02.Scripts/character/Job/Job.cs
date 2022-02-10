using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum JobState { COMMON, BOW, SWORD, MAGIC }
//부모
public class Job : MonoBehaviour
{
    [SerializeField]
    
  
    private Animator animator;  //직업에 플레이어 애니메이터 변경
    public GameObject[] Weapons;  // 0 ,1  궁수 2,3 전사 법사 4
    
    public JobState jobstate { get; set; }//= JobState.COMMON; // 현재 전직 가능한 직업
    public bool Bow { get; private set; }
    public bool Sword { get; private set; }
    public bool Magic { get; private set; }

    public event Action SelectJob;
    ObjData objdata;

    static public Job instance;

    void Awake()
    {
        animator =GetComponentInChildren<Animator>();              //GetComponent<Animator>();
        instance = this;
    }

    private void Update()
    {
        Debug.Log(jobstate);
    }

    public void JobChoice()  // 직업 활성화
    {
        if(JobState.BOW == jobstate) // 궁수 전직
        {
            Bow = true;
            Weapons[0].SetActive(true);
          //  Weapons[1].SetActive(true);
            Weapons[5].SetActive(true);
            
        }
        else if (JobState.SWORD == jobstate) // 궁수 전직
        {
            Sword = true;
            Weapons[2].SetActive(true);
            Weapons[3].SetActive(true);
        }
        else if (JobState.MAGIC == jobstate) // 법사 전직
        {
            Magic = true;
            Weapons[4].SetActive(true);
        }
        animator.SetInteger("JobState", (int)jobstate);
    }


}
