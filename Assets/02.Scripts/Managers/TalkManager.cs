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

        talkData.Add(1, new string[] { "전사의 길을 걷겠는가?", "쉽지 않은 길일거야.."} );
        talkData.Add(2, new string[] { "마법사의 길을 걷겠는가?", "쉽지 않은 길일거야.." });
        talkData.Add(3, new string[] { "궁수의 길을 걷겠는가?", "쉽지 않은 길일거야.." });

        talkData.Add(5, new string[] { "퀘스트 튜토리얼" });
        talkData.Add(6, new string[] { "퀘스트 튜토리얼2" });


        errorString = new string[] { "자네는 이미 직업이 있군"};
    }

    public void Action(GameObject npc)
    {
        NPC = npc;
        int id = npc.GetComponent<ObjData>().id;
        if (id <= 3 )
        {
            UI_Panel ui_Panel =  Managers.UI.ShowPopupUI<UI_Panel>();
            ui_Panel.Init();
            ui_Panel.OkButton.gameObject.AddUIEvent(ui_Panel.OnJobChoiceButton);
        }

        else if (id == 4)
        {
            Managers.UI.ShowPopupUI<UI_Store>();
        }
        else if (id ==5 || id ==6)
        {

            UI_Panel ui_Panel = Managers.UI.ShowPopupUI<UI_Panel>();
            ui_Panel.Init();
            ui_Panel.OkButton.gameObject.AddUIEvent(ui_Panel.QuestGive);
        }


        #region SceneMove
        else if (id /6000 == 1 ) // 보완하기
        {
            
            UI_Message ui_Message = Managers.UI.ShowPopupUI<UI_Message>();
            ui_Message.Init();
            ui_Message.NextScene = id;

            if (id == 6000) // 셀렉 신으로 이동
            {            
                ui_Message.ShowMessage("맵 이동", "선택의 길로 이동합니다.");
            }
            else if (id == 6001) // 타운 이동
            {        
                ui_Message.ShowMessage("맵 이동", "마을로 이동합니다.");

                if(NPC.GetComponent<QuestReporter>() == null)
                {
                    Debug.Log("QuestReporter가 아닙니다 ");
                }
                else
                {
                    NPC.GetComponent<QuestReporter>().Report();
                }

            }
            else if (id == 6002) // 사냥터 1로 이동
            {
                ui_Message.ShowMessage("맵 이동", "늪의 길로 이동합니다 ");
                
            }
            else if (id == 6003) // 사냥터 2로 이동
            {
                ui_Message.ShowMessage("맵 이동", "망자들의 길로 이동합니다.");
                
            }
            else if (id == 6004)
            {
                ui_Message.ShowMessage("맵 이동", "-위험-\n왕의 길로 이동합니다");
               
            }
            ui_Message.okButton.gameObject.AddUIEvent(ui_Message.SceneMoveOk);
        }
        #endregion


        
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
