using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Quest : UI_Scene
{
   enum GameObjects
   {
       QuestView
   }


    public GameObject questView;
    public override void Init()
    {
        base.Init();

        Bind<GameObject>(typeof(GameObjects));
        questView = Get<GameObject>((int)GameObjects.QuestView);
       


    }
     public void OpenQuest()
    {
        //GameManager.isOpenInventory = true;
        if (!questView.activeSelf)
        {
            Managers.Game.isOpenInventory = true;
            questView.SetActive(true);
        }
        else
        {
            Managers.Game.isOpenInventory = false;
            questView.SetActive(false);
        }


    }
}
