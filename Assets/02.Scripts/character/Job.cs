using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum JobState { COMMON, BOW, SWORD, MAGIC }
public class Job : MonoBehaviour
{
    
    private  JobState jobstate = JobState.COMMON;
    private Animator animator;

    private GameObject equipWeapon;
    public GameObject[] Weapons;  // 0 ,1  �ü� 2,3 ���� ���� 4
   
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(jobstate != JobState.COMMON && Input.GetKeyDown(KeyCode.Space))
        {
            JobChoice(jobstate);
 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Npc")
        {
            jobstate = JobState.COMMON;
        }
    }
    private void OnTriggerStay(Collider other)   // npc�� id���� state�� ����
    {
        if (other.tag == "Npc")
        {
            if (other.gameObject.GetComponent<ObjData>().id == 1)
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

    void JobChoice(JobState state)  // ���� Ȱ��ȭ
    {
        if(JobState.BOW == state)
        {
            Weapons[0].SetActive(true);
            Weapons[1].SetActive(true);
        }
    }

}
