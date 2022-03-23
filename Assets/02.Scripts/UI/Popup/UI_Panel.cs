using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Panel : UI_Popup
{
    
    enum Buttons
    {
        JobButton,
        ExitButton
    }

    enum Texts
    {
        TalkText,
        NPCNameText,
    }

    enum Images
    {
        JobBackGroundImage
    }

    bool bInit;
    ObjData objData;

    [HideInInspector]
    public Button OkButton;

    Text npcNameText;
    Text talkText;

    
    public override void Init()
    {
        base.Init();
        if (bInit) return;
        bInit = true;


        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        OkButton = GetButton((int)Buttons.JobButton);
        //.gameObject.AddUIEvent(OnJobChoiceButton);
        GetButton((int)Buttons.ExitButton).gameObject.AddUIEvent(JobExitButton);

        objData = Managers.talk.NPC.GetComponent<ObjData>();

        npcNameText = GetText((int)Texts.NPCNameText);
        talkText = GetText((int)Texts.TalkText);


        talkText.text = Managers.talk.GetTalk(objData.id,talkIndex);
        npcNameText.text = Managers.talk.NPC.name;

    }


    public void JobExitButton(PointerEventData data) // ��ҹ�ư ������
    {
        talkIndex = 0;
        Managers.UI.isTalk(false);
        Managers.UI.ClosePopupUI();

    }
    public void OnJobChoiceButton(PointerEventData data)
    {
        JobController jobController = Managers.Game.GetPlayer().GetOrAddComponent<JobController>();
       
       
        if (jobController.jobstring != null)  //������ �̹� ������
        {
            if (Managers.talk.GetTalk(talkIndex) == null)
            {
                talkIndex = 0;
                Managers.UI.isTalk(false);
                Managers.UI.ClosePopupUI();
            }
            talkText.text = Managers.talk.GetTalk(talkIndex);
            talkIndex++;
        }
        else // �ƴϸ� �� ��ȭ ����
        {
            talkIndex++;
            talkText.text = Managers.talk.GetTalk(objData.id, talkIndex);
        }
        if (Managers.talk.GetTalk(objData.id, talkIndex) == null) //���� �� ������
        {
            talkIndex = 0;
            Managers.UI.isTalk(false);
            jobController.JobChoice(objData.id); // ����Ȯ��
            QuestReporter questReporter = Managers.talk.NPC.GetComponent<QuestReporter>();
            questReporter.Report();
            Managers.UI.ClosePopupUI(this);
        }

    }

    public void QuestGive(PointerEventData data)
    {
        Managers.talk.NPC.GetComponent<QuestGiver>().QuestSet();
        Managers.UI.ClosePopupUI(this);
        Managers.UI.isTalk(false);
    }
}
