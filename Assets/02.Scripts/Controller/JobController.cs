using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum JobInfo { COMMON, BOW, SWORD, MAGIC , STORE}

public class JobController : MonoBehaviour
{
    private static



    ObjData objdata;

    Animator animator;

    public GameObject[] Weapons;  // 0 ,1  궁수 2,3 전사 법사 4

    public JobInfo jobstate { get; private set; }//= JobState.COMMON; // 현재 전직 가능한 직업
    public string jobstring { get; private set; }


    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        DontDestroyOnLoad(this);
        jobstring = null;
    }

    public void JobChoice()  // 직업 활성화
    { 
        if (jobstate == JobInfo.BOW) // 궁수 전직
        {
            jobstring = "Bow";
       
            Weapons[0].SetActive(true);
            Weapons[1].SetActive(true);
        }
        else if (JobInfo.SWORD == jobstate) // 전사
        {
            jobstring = "Sword";
            Weapons[2].SetActive(true);
            Weapons[3].SetActive(true);
        }
        else if (JobInfo.MAGIC == jobstate) // 법사 전직
        {
            jobstring = "Magic";
            Weapons[4].SetActive(true);
        }
        
        animator.runtimeAnimatorController = Managers.Resource.Load<RuntimeAnimatorController>($"Animator/{jobstring}");
        //Managers.Resource.Instantiate_Ani(jobstring); // 직업에 맞는 애니메이터로 변경
        gameObject.AddComponent(System.Type.GetType(jobstring)); // 직업에 맞는 스크립트 부여

    }
  


    public void changejobstate(GameObject npc)
    {
        jobstate = (JobInfo)npc.GetComponent<ObjData>().id;
    }
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "Npc")
    //    {
           
    //        jobstate = JobInfo.COMMON;
    //    }
    //}
}
