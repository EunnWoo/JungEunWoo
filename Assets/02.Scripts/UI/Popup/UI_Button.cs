using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_Button : UI_Popup  // 프리팹이름이 UI_Button이고 쓸 컴퍼넌트들을 enum으로 넣기
{

    enum Buttons //버튼이랑 text 이름 동일하게해야함
    {
        TestButton
    }

    enum Texts
    {
        ScoreText
    }
    enum GameObjects
    {
        Object
    }
    enum Images
    {
       ItemIcon
    }


    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GetButton((int)Buttons.TestButton).gameObject.AddUIEvent(OnButtonClicked);

        GameObject go = GetImage((int)Images.ItemIcon).gameObject; // 오브젝트로 뽑은이유 -> ui이벤트 핸들러 추가나 사용하려고
        AddUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
    }
    int _score = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"점수 : {_score}";
    }
}
