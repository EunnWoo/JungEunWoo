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
        title.text = _title; //타이틀은 타이틀에넣어준다
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
        Managers.UI.isTalk(false);
        Managers.Scene.LoadScene(SceneState.Town);
        Managers.UI.ClosePopupUI(this);
    }



    private void Function_Slider(float _value)
    {
        ShowMessage("구매", (int)_value + "개 구매");
       
    }
}
