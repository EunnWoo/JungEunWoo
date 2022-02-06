using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum JobState { COMMON, BOW, SWORD, MAGIC }
public class Job : MonoBehaviour
{
    
    private  JobState jobstate = JobState.COMMON; // 현재 전직 가능한 직업
    private Animator animator;  //직업에 플레이어 애니메이터 변경

   // private GameObject equipWeapon;  // 
    public GameObject[] Weapons;  // 0 ,1  궁수 2,3 전사 법사 4

    public bool Bow = false;
    public bool Sword = false;
    public bool Magic = false;

    static public Job instance;

    void Awake()
    {
        animator =GetComponentInChildren<Animator>();              //GetComponent<Animator>();
        instance = this;
    }

    private void Update()
    {
        if(jobstate != JobState.COMMON && Input.GetKeyDown(KeyCode.Space) && !Bow && !Sword && !Magic)
        {
            JobChoice(jobstate); // 직업선택 함수
 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Npc")
        {
            jobstate = JobState.COMMON;
        }
    }
    private void OnTriggerStay(Collider other)   // npc의 id별로 state값 변경
    {
        if (other.tag == "Npc")
        {
            if (other.gameObject.GetComponent<ObjData>().id == 1)  //objdata id 값이 1일떄 궁수
            {
                jobstate = JobState.BOW;
            }
            else if (other.gameObject.GetComponent<ObjData>().id == 2)
            {
                jobstate = JobState.SWORD;
            }
            else if (other.gameObject.GetComponent<ObjData>().id == 3)
            {
                jobstate = JobState.MAGIC;
            }
        }
    }

    void JobChoice(JobState state)  // 직업 활성화
    {
        if(JobState.BOW == state) // 궁수 전직
        {
            Bow = true;
            Weapons[0].SetActive(true);
          //  Weapons[1].SetActive(true);
            Weapons[5].SetActive(true);
            
        }
        else if (JobState.SWORD == state) // 궁수 전직
        {
            Sword = true;
            Weapons[2].SetActive(true);
            Weapons[3].SetActive(true);
        }
        else if (JobState.MAGIC == state) // 법사 전직
        {
            Magic = true;
            Weapons[4].SetActive(true);
        }
        animator.SetInteger("JobState", (int)state);
    }

}
