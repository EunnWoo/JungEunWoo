using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public enum JobState { COMMON, BOW, SWORD, MAGIC }
//�θ�
public class Job : MonoBehaviour
{
    [SerializeField]
    
  
    private Animator animator;  //������ �÷��̾� �ִϸ����� ����
    public GameObject[] Weapons;  // 0 ,1  �ü� 2,3 ���� ���� 4
    
    public JobState jobstate { get; set; }//= JobState.COMMON; // ���� ���� ������ ����
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

    public void JobChoice()  // ���� Ȱ��ȭ
    {
        if(JobState.BOW == jobstate) // �ü� ����
        {
            Bow = true;
            Weapons[0].SetActive(true);
          //  Weapons[1].SetActive(true);
            Weapons[5].SetActive(true);
            
        }
        else if (JobState.SWORD == jobstate) // �ü� ����
        {
            Sword = true;
            Weapons[2].SetActive(true);
            Weapons[3].SetActive(true);
        }
        else if (JobState.MAGIC == jobstate) // ���� ����
        {
            Magic = true;
            Weapons[4].SetActive(true);
        }
        animator.SetInteger("JobState", (int)jobstate);
    }


}
