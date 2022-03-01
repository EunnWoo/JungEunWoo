using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    //sortorder
    int _order = 0;
    //���� ���߿� ����� �˾� ���� ����� -> stack

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>(); //gameobject��� popup�� �ִ� ���� -> ������Ʈ�� ���۳�Ʈ ���������̶� �ƹ��� ������ ������ �����ʱ� ����

    public T ShowPopupUI<T>(string name =null) where T :UI_Popup// �̸��� T�� ���� �޴� ���� ->name -> prefabs ������ ����  // T��  Ÿ��
    {
        if (string.IsNullOrEmpty(name)) //�̸��� ����ִٸ� ������Ÿ���� name �޾ƿͼ� �־��ֱ�
            name = typeof(T).Name;

        GameObject go= Managers.Resource.Instantiate($"UI/Popup/{name}"); // ������ ��ȯ
        T popup =  Util.GetOrAddComponent<T>(go); // ��ũ��Ʈ �־��ֱ�
        _popupStack.Push(popup); // ���ÿ� �־��ֱ�
        //showpopupui���� ���� ���� �� ���ִ� ���� -> ���� �����Ǿ��ִ� ui�� ��Ʈ���Ҷ� ī���Ͱ� �� �ȴ�.
        return popup;
    }

    public void ClosePopupUI(UI_Popup popup) // �� �� �����ϴ�
    {
        if (_popupStack.Count == 0) //���� �ǵ帱�� ī��Ʈ üũ�ϱ�
            return;

        if(_popupStack.Peek() !=popup) // ���������� ������ popup�� �ƴ϶��
        {
            Debug.Log("Close popup failed!");
            return;
        }
        ClosePopupUI();
    }
    public void ClosePopupUI()
    {
        //���� �����ؼ� �ݱ�
        if (_popupStack.Count == 0) //���� �ǵ帱�� ī��Ʈ üũ�ϱ�
            return;
       UI_Popup popup = _popupStack.Pop(); // �̾ƿ���
        Managers.Resource.Destroy(popup.gameObject); // ���� �� �����
        popup = null; // Ȥ�� �𸣴� null �� �����ұ��
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }


}
