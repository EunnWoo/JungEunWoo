//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using System;


//public enum JobInfo { COMMON, BOW, SWORD, MAGIC }
////부모
//public class Job : MonoBehaviour
//{
   
    
  
    
//   // public GameObject[] Weapons;  // 0 ,1  궁수 2,3 전사 법사 4
//   // [SerializeField]
//  //  public JobInfo jobstate { get; set; }//= JobState.COMMON; // 현재 전직 가능한 직업
//   // public JobInfo jobFix { get; private set; }
////
//   // static public Job instance;

//    void Awake()
//    {
 
//        instance = this;
//        jobFix = JobInfo.COMMON;
        
//    }

//    public void JobChoice()  // 직업 활성화
//    {
      
//        if (jobFix != JobInfo.COMMON) return;
//        if(jobstate == JobInfo.BOW) // 궁수 전직
//        {

//            gameObject.AddComponent<Bow>();
//           // gameObject.GetComponent<Bow>().enabled = true;
//            Weapons[0].SetActive(true);
//            Weapons[5].SetActive(true);

            
//        }
//        else if (JobInfo.SWORD == jobstate) // 궁수 전직
//        {
//            gameObject.GetComponent<Sword>().enabled = true;
//            Weapons[2].SetActive(true);
//            Weapons[3].SetActive(true);
//        }
//        else if (JobInfo.MAGIC == jobstate) // 법사 전직
//        {
//            gameObject.GetComponent<Magic>().enabled = true;
//            Weapons[4].SetActive(true);
//        }
       
//        jobFix = jobstate;
        
//    }

   


//}
