using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum JobInfo { COMMON, BOW, SWORD, MAGIC }

public class JopController : MonoBehaviour
{
    ObjData objdata;
    PlayerMovement playerMovement;

    public DialogManager dialogManager;


    public GameObject[] Weapons;  // 0 ,1  궁수 2,3 전사 법사 4
    [SerializeField]
    public JobInfo jobstate { get; set; }//= JobState.COMMON; // 현재 전직 가능한 직업
    public JobInfo jobFix { get; private set; }

    //static public Job instance;

    void Awake()
    {

     //   instance = this;
        jobFix = JobInfo.COMMON;
        playerMovement = GetComponent<PlayerMovement>();
    }

    void JobChoice()  // 직업 활성화
    {

        if (jobFix != JobInfo.COMMON) return;
        if (jobstate == JobInfo.BOW) // 궁수 전직
        {

            gameObject.AddComponent<Bow>();
            Weapons[0].SetActive(true);
            Weapons[1].SetActive(true);


        }
        else if (JobInfo.SWORD == jobstate) // 궁수 전직
        {
         //   gameObject.GetComponent<Sword>().enabled = true;
            Weapons[2].SetActive(true);
            Weapons[3].SetActive(true);
        }
        else if (JobInfo.MAGIC == jobstate) // 법사 전직
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
            Debug.Log("직업관");
            jobstate = (JobInfo)objdata.id;
            // Debug.Log("NPC잡이 리턴해주는 잡스테이트 값 :"+other.gameObject.GetComponent<Job>().jobstate);
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
            //전직이 이미 있을때 실행
        }
        JobChoice();
        dialogManager.dialogPanel.SetActive(false);
        dialogManager.isAction = false;
    }
}
