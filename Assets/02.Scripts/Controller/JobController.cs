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
    ItemData itemData;
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
            itemData = new ItemData((int)Define.Itemcode.Sword1);
        }
        else if (jobstate == (int)JobInfo.MAGIC) // 법사 전직
        {
            jobstring = "Magic";
            itemData = new ItemData((int)Define.Itemcode.Wand1);
        }

        else if (jobstate == (int)JobInfo.BOW) // 궁수 전직
        {
            jobstring = "Bow";
            itemData = new ItemData((int)Define.Itemcode.Bow1);
        }

        
        

        Managers.UI.ui_Inventory.AddItemData(itemData);
        gameObject.AddComponent(System.Type.GetType(jobstring)); // 직업에 맞는 스크립트 부여

    }

}
