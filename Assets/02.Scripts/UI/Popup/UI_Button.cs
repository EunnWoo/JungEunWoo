using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_Button : UI_Popup  // �������̸��� UI_Button�̰� �� ���۳�Ʈ���� enum���� �ֱ�
{

    enum Buttons //��ư�̶� text �̸� �����ϰ��ؾ���
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

        GameObject go = GetImage((int)Images.ItemIcon).gameObject; // ������Ʈ�� �������� -> ui�̺�Ʈ �ڵ鷯 �߰��� ����Ϸ���
        AddUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);


    }
    int _score = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        _score++;
        GetText((int)Texts.ScoreText).text = $"���� : {_score}";
    }
}