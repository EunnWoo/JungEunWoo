using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum JobInfo { COMMON, BOW, SWORD, MAGIC }

public class JopController : MonoBehaviour
{
    ObjData objdata;
    PlayerMovement playerMovement;

    public DialogManager dialogManager;


    public GameObject[] Weapons;  // 0 ,1  �ü� 2,3 ���� ���� 4
    [SerializeField]
    public JobInfo jobstate { get; set; }//= JobState.COMMON; // ���� ���� ������ ����
    public JobInfo jobFix { get; private set; }

    //static public Job instance;

    void Awake()
    {

     //   instance = this;
        jobFix = JobInfo.COMMON;
        playerMovement = GetComponent<PlayerMovement>();
    }

    void JobChoice()  // ���� Ȱ��ȭ
    {

        if (jobFix != JobInfo.COMMON) return;
        if (jobstate == JobInfo.BOW) // �ü� ����
        {

            gameObject.AddComponent<Bow>();
            Weapons[0].SetActive(true);
            Weapons[1].SetActive(true);


        }
        else if (JobInfo.SWORD == jobstate) // �ü� ����
        {
         //   gameObject.GetComponent<Sword>().enabled = true;
            Weapons[2].SetActive(true);
            Weapons[3].SetActive(true);
        }
        else if (JobInfo.MAGIC == jobstate) // ���� ����
        {
         //   gameObject.GetComponent<Magic>().enabled = true;
            Weapons[4].SetActive(true);
        }

        jobFix = jobstate;
        playerMovement.playerAttack = GetComponent<PlayerAttack>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Npc")
        {

            objdata =other.gameObject.GetComponent<ObjData>();
            Debug.Log("������");
            jobstate = (JobInfo)objdata.id;
            // Debug.Log("NPC���� �������ִ� �⽺����Ʈ �� :"+other.gameObject.GetComponent<Job>().jobstate);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Npc")
        {
           
            jobstate = JobInfo.COMMON;
        }
    }


    public void JobExitClickButton()
    {
        dialogManager.dialogPanel.SetActive(false);
        dialogManager.isAction = false;
    }
    public void JobChoiceButton()
    {
        if (jobFix != JobInfo.COMMON)
        {
            //������ �̹� ������ ����
        }
        JobChoice();
        dialogManager.dialogPanel.SetActive(false);
        dialogManager.isAction = false;
    }
}
