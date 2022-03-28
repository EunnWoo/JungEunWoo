using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum JobInfo { COMMON, SWORD, MAGIC , BOW, STORE }

public class JobController : MonoBehaviour
{

    Animator animator;

   // public GameObject[] Weapons;  // 0 ,1  �ü� 2,3 ���� ���� 4

   // public JobInfo jobstate { get; private set; }//= JobState.COMMON; // ���� ���� ������ ����
    public string jobstring { get; private set; }
    ItemData itemData;
    UI_CoolTime ui_CoolTime;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        DontDestroyOnLoad(this);
        jobstring = null;
    }


    public void JobChoice(int jobstate)  // ���� Ȱ��ȭ
    { 
        if (jobstate == (int)JobInfo.SWORD) // ����
        {
            jobstring = "Sword";
            itemData = new ItemData((int)Define.Itemcode.Sword1);
        }
        else if (jobstate == (int)JobInfo.MAGIC) // ���� ����
        {
            jobstring = "Magic";
            itemData = new ItemData((int)Define.Itemcode.Wand1);
        }

        else if (jobstate == (int)JobInfo.BOW) // �ü� ����
        {
            jobstring = "Bow";
            itemData = new ItemData((int)Define.Itemcode.Bow1);
        }

        
        

        Managers.UI.ui_Inventory.AddItemData(itemData);
        gameObject.AddComponent(System.Type.GetType(jobstring)); // ������ �´� ��ũ��Ʈ �ο�

    }

}
