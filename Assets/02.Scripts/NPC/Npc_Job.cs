//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Npc_Job : MonoBehaviour
//{
//    [SerializeField]
//    private ObjData objdata;

//    private void Awake()
//    {
//        objdata = GetComponent<ObjData>();
//    }
//    private void OnTriggerEnter(Collider other)
//    {
//        if (other.gameObject.tag == "Player")
//        {
//            Debug.Log("직업관");
//            other.gameObject.GetComponent<Job>().jobstate = (JobInfo)objdata.id;
//           // Debug.Log("NPC잡이 리턴해주는 잡스테이트 값 :"+other.gameObject.GetComponent<Job>().jobstate);
//        }
//    }
//    private void OnTriggerExit(Collider other)
//    {
//        if (other.gameObject.tag == "Player")
//        {
//            Debug.Log("나가짐");
//            other.gameObject.GetComponent<Job>().jobstate = JobInfo.COMMON;
//        }
//    }
   
//}
