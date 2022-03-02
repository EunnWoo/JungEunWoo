using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_JobPanel : UI_Popup
{
    ObjData objData;
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


    public override void Init()
    {
        base.Init();



        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.JobButton).gameObject.AddUIEvent(OnJobChoiceButton);
        GetButton((int)Buttons.ExitButton).gameObject.AddUIEvent(JobExitButton);

        objData = Managers.talk.NPC.GetOrAddComponent<ObjData>();
        GetText((int)Texts.TalkText).text = Managers.talk.GetTalk(objData.id,talkIndex);
        GetText((int)Texts.NPCNameText).text = Managers.talk.NPC.name;

    }


    public void JobExitButton(PointerEventData data) // ��ҹ�ư ������
    {
        talkIndex = 0;
        Managers.UI.isTalk(false);
        Managers.UI.ClosePopupUI();

    }
    public void OnJobChoiceButton(PointerEventData data)
    {
        JopController jopController = Managers.Game.GetPlayer().GetOrAddComponent<JopController>();

       
        if (jopController.jobstring != null)  //������ �̹� ������
        {
            if (Managers.talk.GetTalk(talkIndex) == null)
            {
                talkIndex = 0;
                Managers.UI.isTalk(false);
                Managers.UI.ClosePopupUI();
            }
            GetText((int)Texts.TalkText).text = Managers.talk.GetTalk(talkIndex);
            talkIndex++;


        }
        else // �ƴϸ� �� ��ȭ ����
        {
            talkIndex++;
            GetText((int)Texts.TalkText).text = Managers.talk.GetTalk(objData.id, talkIndex);
        }
        if (Managers.talk.GetTalk(objData.id, talkIndex) == null) //���� �� ������
        {
            talkIndex = 0;
            Managers.UI.isTalk(false);
            jopController.JobChoice();
            Managers.UI.ClosePopupUI();
        }

    }

}
