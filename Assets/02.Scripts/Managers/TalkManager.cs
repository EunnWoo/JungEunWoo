using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TalkManager
{ 
    Dictionary<int, string[]> talkData;

    
    public GameObject NPC { get; private set; }

    //public int talkIndex;

    public string[] errorString { get; private set; }

    public void Init()
    {
        talkData = new Dictionary<int, string[]>();
        talkData.Add(1000, new string[]{"...."});
        talkData.Add(2000, new string[]{"없는거 빼고 다 있음"});

        talkData.Add(1, new string[] {"궁수의 길을 걷겠는가?" , "쉽지 않은 길일거야.."} );
        talkData.Add(2, new string[] { "전사의 길을 걷겠는가?", "쉽지 않은 길일거야.." });
        talkData.Add(3, new string[] { "마법사의 길을 걷겠는가?", "쉽지 않은 길일거야.." });
        errorString = new string[] { "자네는 이미 직업이 있군"/*, "다시 한번 생각하고 오게나.."*/ };

    }

    public void Action(GameObject npc)
    {
        NPC = npc;
        if(npc.GetComponent<ObjData>().id <=3)
        {
            Managers.UI.ShowPopupUI<UI_JobPanel>();
            //Managers.Game.GetPlayer().GetComponent<JobController>().changejobstate(npc);
        }
        else if (npc.GetComponent<ObjData>().id == 4)
        {
            Managers.UI.ShowPopupUI<UI_Store>();
        }
        else if(npc.GetComponent<ObjData>().id ==6000)
        {
            UI_Message ui_Message = Managers.UI.ShowPopupUI<UI_Message>();
            ui_Message.Init();
            ui_Message.ShowMessage("맵 이동", "마을로 이동합니다." );
            ui_Message.okButton.gameObject.AddUIEvent(ui_Message.SceneMoveOk);


        }
        
        
        Managers.UI.isTalk(true);
        
    }

    public string GetTalk(int id, int talkIndex)
    {
        if (talkIndex == talkData[id].Length)
        {
            return null;
        }
        else
        {
            return talkData[id][talkIndex];
        }
    }
    public string GetTalk(int talkIndex)
    {
        if (talkIndex == errorString.Length)
        {
            return null;
        }
        else
        {
            return errorString[talkIndex];
        }
    }




}
