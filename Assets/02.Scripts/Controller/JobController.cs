using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum JobInfo { COMMON, SWORD, MAGIC , BOW, STORE }

public class JobController : MonoBehaviour
{

    Animator animator;

   // public GameObject[] Weapons;  // 0 ,1  궁수 2,3 전사 법사 4

   // public JobInfo jobstate { get; private set; }//= JobState.COMMON; // 현재 전직 가능한 직업
    public string jobstring { get; private set; }

    UI_CoolTime ui_CoolTime;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        DontDestroyOnLoad(this);
        jobstring = null;
    }


    public void JobChoice(int jobstate)  // 직업 활성화
    { 
        if (jobstate == (int)JobInfo.SWORD) // 전사
        {
            jobstring = "Sword";
          //  Weapons[2].SetActive(true);
          //  Weapons[3].SetActive(true);
        }
        else if (jobstate == (int)JobInfo.MAGIC) // 법사 전직
        {
            jobstring = "Magic";
         //   Weapons[4].SetActive(true);
        }

        else if (jobstate == (int)JobInfo.BOW) // 궁수 전직
        {
            jobstring = "Bow";
       
         //   Weapons[0].SetActive(true);
         //   Weapons[1].SetActive(true);
        }
       
      

        animator.runtimeAnimatorController = Managers.Resource.Load<RuntimeAnimatorController>($"Animator/{jobstring}");
        gameObject.AddComponent(System.Type.GetType(jobstring)); // 직업에 맞는 스크립트 부여

    }

}
