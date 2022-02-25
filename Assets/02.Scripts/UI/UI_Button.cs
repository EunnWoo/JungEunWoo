using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UI_Button : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    enum Buttons //버튼이랑 text 이름 동일하게해야함
    {
        JobButton
    }

    enum Texts
    {
        JobChoiceText
    }
    private void Start()
    {
        Bind<Button>(typeof(Buttons));
       
        Bind<Text>(typeof(Texts));
    }


    void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);            // 타입의 name들을 리턴해서 저장
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; //names의 길이만큼 오브젝트 배열 크기 할당
        _objects.Add(typeof(T), objects); // 딕셔너리 _objects에 Add(타입(ex Button) , 오브젝트 배열)

        for (int i =0; i<names.Length; i++)
        {
            objects[i] = Util.FindChild<T>(gameObject, names[i], true); // 스크립트가 달린 gameobject, 이름 , 자식까지 찾을건가
        }
    }
}
