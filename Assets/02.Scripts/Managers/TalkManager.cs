using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TalkManager
{ 
    Dictionary<int, string[]> talkData;

    
    public GameObject NPC { get; private set; }
    public string[] errorString { get; private set; }

    public void Init()
    {
        talkData = new Dictionary<int, string[]>();

        talkData.Add(1, new string[] { "전사의 길을 걷겠는가?", "쉽지 않은 길일거야..","너에게 맞는 무기를 주겠네.. I와 U를 눌러 확인해보시게"} );
        talkData.Add(2, new string[] { "마법사의 길을 걷겠는가?", "쉽지 않은 길일거야..", "너에게 맞는 무기를 주겠네.. I와 U를 눌러 확인해보시게" });
        talkData.Add(3, new string[] { "궁수의 길을 걷겠는가?", "쉽지 않은 길일거야..", "너에게 맞는 무기를 주겠네.. I와 U를 눌러 확인해보시게" });

        talkData.Add(5, new string[] { 
            "안녕 시작의 섬에 온 걸 환영해 간단한 조작법을 알려줄게",
            "이동 W,A,S,D 달리기 Shift  점프 Space   구르기 MouseWheel\n 상호작용 F or Click",
            "조작법은 알았지?\n 그렇다면 가르키는 곳을 전부 가고 포탈을 이용해봐"
             });
        talkData.Add(6, new string[] { "여기까지 잘 왔구나!\n이제 직업을 가져봐야지!! 마음에 드는 직업을 선택해봐",
            "아 맞다!\n 왼쪽 클릭은 일반 공격이고 우측 클릭은 스킬이야\n 공격과 스킬을 활용해 더미를 잡고 마을에서 봐!" });


        errorString = new string[] { "자네는 이미 직업이 있군"};
    }

    public void Action(GameObject npc)
    {
        NPC = npc;
        int id = npc.GetComponent<ObjData>().id;
        if (id <= 3 )
        {
            foreach(var quest in Managers.Quest.ActiveQuests)
            {
                if(quest.CodeName == "TUTORIAL2")
                {
                    UI_Panel ui_Panel =  Managers.UI.ShowPopupUI<UI_Panel>();
                    ui_Panel.Init();
                    ui_Panel.okButton.gameObject.AddUIEvent(ui_Panel.OnJobChoiceButton);
                    return;
                }
            }
            UI_Message ui_Message = Managers.UI.ShowPopupUI<UI_Message>();
            ui_Message.Init();
            ui_Message.ShowMessage("에러", "퀘스트를 먼저 진행해주세요.");
            ui_Message.okButton.gameObject.AddUIEvent(ui_Message.Cancel);
        }

        else if (id == 4)
        {
            Managers.UI.ShowPopupUI<UI_Store>();
        }
        else if (id ==5 || id ==6)
        {

            UI_Panel ui_Panel = Managers.UI.ShowPopupUI<UI_Panel>();
            ui_Panel.Init();
            ui_Panel.okButton.gameObject.AddUIEvent(ui_Panel.QuestGive);
        }


        #region SceneMove
        else if (id >= (int)SceneState.Select && id < (int)SceneState.End   ) 
        {
            
            UI_Message ui_Message = Managers.UI.ShowPopupUI<UI_Message>();
            ui_Message.Init();

            if (id == (int)SceneState.Select) // 셀렉 신으로 이동
            {            
                ui_Message.ShowMessage("맵 이동", "선택의 길로 이동합니다.");
            }
            else if (id == (int)SceneState.Town) // 타운 이동
            {        
                ui_Message.ShowMessage("맵 이동", "마을로 이동합니다.");

                if (NPC.GetComponent<QuestReporter>() == null)
                {
#if UNITY_EDITOR
                    Debug.Log("QuestReporter가 아닙니다 ");
#endif
                }
                else
                {
                    NPC.GetComponent<QuestReporter>().Report();
                }

            }
            else if (id == (int)SceneState.Map1) // 사냥터 1로 이동
            {
                ui_Message.ShowMessage("맵 이동", "늪의 길로 이동합니다 ");
                
            }
            else if (id == (int)SceneState.Map2) // 사냥터 2로 이동
            {
                ui_Message.ShowMessage("맵 이동", "망자들의 길로 이동합니다.");
                
            }
            else if (id == (int)SceneState.Map3)
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
