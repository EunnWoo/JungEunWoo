using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_Button : UI_Base
{
    

    enum Buttons //��ư�̶� text �̸� �����ϰ��ؾ���
    {
        JobButton
    }

    enum Texts
    {
        JobChoiceText
    }
    enum GameObjects
    {
        Object
    }
    enum Images
    {
       ItemIcon
    }
    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        GameObject go = GetImage((int)Images.ItemIcon).gameObject; // ������Ʈ�� �������� -> ui�̺�Ʈ �ڵ鷯 �߰��� ����Ϸ���
        UI_EventHandler evt =  go.GetComponent<UI_EventHandler>();
        evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; }); // ���ٽ����� �̺�Ʈ �߰�
        
    }

}
