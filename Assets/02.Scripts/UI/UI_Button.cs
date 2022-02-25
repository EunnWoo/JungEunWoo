using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class UI_Button : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    enum Buttons //��ư�̶� text �̸� �����ϰ��ؾ���
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
        string[] names = Enum.GetNames(type);            // Ÿ���� name���� �����ؼ� ����
        UnityEngine.Object[] objects = new UnityEngine.Object[names.Length]; //names�� ���̸�ŭ ������Ʈ �迭 ũ�� �Ҵ�
        _objects.Add(typeof(T), objects); // ��ųʸ� _objects�� Add(Ÿ��(ex Button) , ������Ʈ �迭)

        for (int i =0; i<names.Length; i++)
        {
            objects[i] = Util.FindChild<T>(gameObject, names[i], true); // ��ũ��Ʈ�� �޸� gameobject, �̸� , �ڽı��� ã���ǰ�
        }
    }
}
