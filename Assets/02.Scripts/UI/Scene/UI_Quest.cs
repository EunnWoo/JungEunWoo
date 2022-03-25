using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Quest : UI_Scene
{
    bool bInit;

    enum GameObjects
    {
       QuestView
    }



    public GameObject questView;
    public override void Init()
    {
        if (bInit) return;
        base.Init();
        bInit = true;

        
        Bind<GameObject>(typeof(GameObjects));
        questView = Get<GameObject>((int)GameObjects.QuestView);

        Debug.Log("UI_Quest �ʱ�ȭ");
        questView.SetActive(false);
    }
     public void OpenQuest()
     {
        questView.SetActive(!questView.activeSelf);
        //if (!questView.activeSelf)
        //{
        //    Debug.Log("OpenQuest Ȱ��ȭ");
        //    questView.SetActive(true);
        //}
        //else
        //{
        //    Debug.Log("OpenQuest ��Ȱ��ȭ");
        //    questView.SetActive(false);
        //}


    }
}
