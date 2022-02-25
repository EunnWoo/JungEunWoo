using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util 
{
    //�ֻ��� �θ� , �̸� ->���� Ÿ�Ը� , recursive -> �ڽ��� �ڽĵ� ã�����ΰ�
    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object
    {
        if (go == null) // gameobject�� ����ٸ� null ����
            return null;

        if (recursive == false)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i);
                if (string.IsNullOrEmpty(name) || transform.name == name) // �̸����� �Ÿ���
                {
                    T component = transform.GetComponent<T>(); // �̸��� �¾����� ���۳�Ʈ�� �ִ��� Ȯ���ϱ�
                    if (component != null)  // ���� �ƴϸ� ���ϱ� -> ����
                        return component;
                }
            }
        }
        else
        {
            foreach(T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name) // �̸��� ����ְų� Ÿ���� ���ٸ�
                    return component;
            }
        }

        return null;

    }
}
