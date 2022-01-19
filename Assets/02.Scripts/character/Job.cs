using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum JobState { COMMON, BOW, SWORD, MAGIC }
public class Job : MonoBehaviour
{
    
    private  JobState jobstate = JobState.COMMON; // ���� ���� ������ ����
    private Animator animator;  //������ �÷��̾� �ִϸ����� ����

   // private GameObject equipWeapon;  // 
    public GameObject[] Weapons;  // 0 ,1  �ü� 2,3 ���� ���� 4

    private bool Bow = false;
    private bool Sword = false;
    private bool Magic = false;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(jobstate != JobState.COMMON && Input.GetKeyDown(KeyCode.Space) && (!Bow || !Sword || !Magic))
        {
            JobChoice(jobstate); // �������� �Լ�
 
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
            if (other.gameObject.GetComponent<ObjData>().id == 1)  //objdata id ���� 1�ϋ� �ü�
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
        if(JobState.BOW == state) // �ü� ����
        {
            Bow = true;
            Weapons[0].SetActive(true);
            Weapons[1].SetActive(true);
        }
        else if (JobState.SWORD == state) // �ü� ����
        {
            Sword = true;
            Weapons[2].SetActive(true);
            Weapons[3].SetActive(true);
        }
        else if (JobState.MAGIC == state) // ���� ����
        {
            Magic = true;
            Weapons[4].SetActive(true);
        }
    }

}
