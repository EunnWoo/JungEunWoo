using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Npc_Job : MonoBehaviour
{
    [SerializeField]
    private ObjData objdata;

    private void Awake()
    {
        objdata = GetComponent<ObjData>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Á÷¾÷°ü");
            other.gameObject.GetComponent<Job>().jobstate = (JobInfo)objdata.id;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Job>().jobstate = JobInfo.COMMON;
        }
    }
   
}
