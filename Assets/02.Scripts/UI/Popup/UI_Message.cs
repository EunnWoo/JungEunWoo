using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Message : UI_Popup
{
    
    bool bInit;
    Text title, content;

    [HideInInspector]
    public Button okButton;
    public Slider countSlider { get; private set; }

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
    public void ShowMessage(string _title, string _content)
    {
        title.text = _title; //타이틀은 타이틀에넣어준다
        content.text = _content;
    }


    public void Cancel(PointerEventData data)
    {
        Managers.UI.ClosePopupUI(this);
    }

    //
    public void SceneMoveOk(PointerEventData data)
    {
        if (!SceneMove())
        {
            ShowMessage("에러", "이전 퀘스트를 완료해주세요");
            okButton.gameObject.AddUIEvent(Cancel);
            return;
        }

        Managers.Scene.LoadScene(nextScene);

        Managers.UI.ClosePopupUI(this);
    }

    public void GameQuit(PointerEventData data)
    {
        Application.Quit();
    }


    private void Function_Slider(float _value) // value에 따른 개수 변경
    {
        ShowMessage("구매", (int)_value + "개 구매");
       
    }


    bool SceneMove()  // 퀘스트 완료 안 하면 못돌아감
    {
        SceneState sceneState = FindObjectOfType<BaseScene>().SceneType;
        if (sceneState == SceneState.Tutorial)
        {

            foreach (var quest in Managers.Quest.CompletedQuests)
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
        
            foreach (var quest in Managers.Quest.CompletedQuests)
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
      
            foreach (var quest in Managers.Quest.CompletedQuests)
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
