using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Message : UI_Popup
{
    #region sigletone
    public static UI_Message ins;
    bool bInit;
    private void Awake()
    {
        ins = this;
    }
    #endregion
    Text title, content;


    public Button okButton;
    [HideInInspector]
    public Slider countSlider;

    System.Action on;

    private SceneState nextScene;
    public int NextScene
    {
        set
        {
            if (value == 6000)
            {
                nextScene = SceneState.Select;
            }
            else if (value ==6001)
            {
                nextScene = SceneState.Town;
            }
            else if(value == 6002)
            {
                nextScene = SceneState.Map1;
            }
            else if(value == 6003)
            {
                nextScene = SceneState.Map2;
            }
            else if(value == 6004)
            {
                nextScene = SceneState.Map3;
            }
            else
            {
                return;
            }
        }
    }

    enum Texts
    {
        TitleText,
        ContentText,
    
    }
    enum Buttons
    {
        OKButton,
        CancelButton
    }
    enum GameObjects
    {
        CountSlider
    }

    public override void Init()
    {
        base.Init();
        if (bInit) return;
        bInit = true;

        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        okButton = GetButton((int)Buttons.OKButton);
        title = GetText((int)Texts.TitleText);
        content = GetText((int)Texts.ContentText);
        countSlider = Get<GameObject>((int)GameObjects.CountSlider).GetComponent<Slider>();

        GetButton((int)Buttons.CancelButton).gameObject.AddUIEvent(Cancel);

        countSlider.onValueChanged.AddListener(Function_Slider);
        countSlider.gameObject.SetActive(false);
    }
    public void ShowMessage(string _title, string _content/*, System.Action _on = null*/)
    {
        title.text = _title; //Ÿ��Ʋ�� Ÿ��Ʋ���־��ش�
        content.text = _content;

        //on = _on;
    }


    public void Cancel(PointerEventData data)
    {
        Managers.UI.ClosePopupUI(this);
        if (Managers.UI.StatePopupUI())
        {
            return;
        }
        else
            Managers.UI.isTalk(false);
    }

    //
    public void SceneMoveOk(PointerEventData data)
    {
        if (!SceneMove())
        {
            ShowMessage("����", "���� ����Ʈ�� �Ϸ����ּ���");
            okButton.gameObject.AddUIEvent(Cancel);
            return;
        }

        Managers.UI.isTalk(false);

        Managers.Scene.LoadScene(nextScene);

        Managers.UI.ClosePopupUI(this);
    }



    private void Function_Slider(float _value)
    {

        ShowMessage("����", (int)_value + "�� ����");
       
    }
    bool SceneMove()  // ����Ʈ �Ϸ� �� �ϸ� �����ư�
    {
        SceneState sceneState = FindObjectOfType<BaseScene>().SceneType;
        if (sceneState == SceneState.Tutorial)
        {
            QuestSystem questSystem = FindObjectOfType<QuestSystem>();
            foreach (var quest in questSystem.CompletedQuests)
            {
                foreach (var taskgroup in quest.TaskGroups)
                {
                    foreach (var task in taskgroup.Tasks)
                    {

                        if (task.CodeName == "JUMP" && task.IsComplete)
                        {
                            return true;

                        }
                    }
                }
            }
        }
        else if (sceneState == SceneState.Select)
        {
            QuestSystem questSystem = FindObjectOfType<QuestSystem>();
            foreach (var quest in questSystem.CompletedQuests)
            {
                foreach (var taskgroup in quest.TaskGroups)
                {
                    foreach (var task in taskgroup.Tasks)
                    {

                       // if (task.CodeName == "JUMP" && task.IsComplete)
                        //{
                            return true;

                        //}
                    }
                }
            }
        }
        else if (sceneState == SceneState.Town)
        {
            QuestSystem questSystem = FindObjectOfType<QuestSystem>();
            foreach (var quest in questSystem.CompletedQuests)
            {
                foreach (var taskgroup in quest.TaskGroups)
                {
                    foreach (var task in taskgroup.Tasks)
                    {

                        // if (task.CodeName == "JUMP" && task.IsComplete)
                        //{
                        return true;

                        //}
                    }
                }
            }
        }
        return false;
    }
}
