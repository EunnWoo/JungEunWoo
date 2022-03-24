using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Quest : UI_Scene
{
   enum GameObjects
   {
       QuestView
   }

    bool bInit;


    public GameObject questView;
    public override void Init()
    {
        if (bInit) return;
        base.Init();
        bInit = true;
        Bind<GameObject>(typeof(GameObjects));
        questView = Get<GameObject>((int)GameObjects.QuestView);


        questView.SetActive(false);
    }
     public void OpenQuest()
    {
        if (!questView.activeSelf)
        {
            questView.SetActive(true);
        }
        else
        {
            questView.SetActive(false);
        }


    }
}
