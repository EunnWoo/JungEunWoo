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
    
    System.Action on;

    enum Texts
    {
        TitleText,
        ContentText
    }
    enum Buttons
    {
        OKButton
    }

    public override void Init()
    {
        base.Init();
        if (bInit) return;
        bInit = true;

        Bind<Text>(typeof(Texts));
        Bind<Button>(typeof(Buttons));


        GetButton((int)Buttons.OKButton).gameObject.AddUIEvent(Invoke_OK);
    }
    public void ShowMessage(string _title, string _content, System.Action _on = null)
    {
        GetText((int)Texts.TitleText).text = _title; //타이틀은 타이틀에넣어준다
        GetText((int)Texts.ContentText).text = _content;

        on = _on;
    }


    public void Invoke_OK(PointerEventData data)
    {
        Managers.UI.ClosePopupUI(this);
    }

}
