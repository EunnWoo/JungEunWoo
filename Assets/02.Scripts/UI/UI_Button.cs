using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

public class UI_Button : UI_Base
{
    

    enum Buttons //버튼이랑 text 이름 동일하게해야함
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

        GameObject go = GetImage((int)Images.ItemIcon).gameObject; // 오브젝트로 뽑은이유 -> ui이벤트 핸들러 추가나 사용하려고
        UI_EventHandler evt =  go.GetComponent<UI_EventHandler>();
        evt.OnDragHandler += ((PointerEventData data) => { evt.gameObject.transform.position = data.position; }); // 람다식으로 이벤트 추가
        
    }

}
