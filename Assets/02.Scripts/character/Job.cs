using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum JobState { COMMON, BOW, SWORD, MAGIC }
public class Job : MonoBehaviour
{
    
    private  JobState jobstate = JobState.COMMON;
    private Animator animator;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(jobstate != JobState.COMMON && Input.GetKeyDown(KeyCode.Space))
        {

        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<ObjData>().id == 1)
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

    void JobChoice(int state)
    {
        if(jobstate ==JobState.BOW)
        {

        }
    }

}
