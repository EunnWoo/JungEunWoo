using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    //sortorder
    int _order = 10; // �˾��� �ƴѰŶ� ���Ƽ� 10����
    //���� ���߿� ����� �˾� ���� ����� -> stack

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>(); //gameobject��� popup�� �ִ� ���� -> ������Ʈ�� ���۳�Ʈ ���������̶� �ƹ��� ������ ������ �����ʱ� ����
    UI_Scene _sceneUI = null;

    public bool isAction { get; private set; }
    public GameObject Root
    { get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
                root = new GameObject { name = "@UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)// �ܺο��� �˾��� ������ ��ĵ���� ��û -> order�� �켱���� ä���޶�
    {
        Canvas canvas= Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true; // ��øĵ�������� �θ� � ���� ������ ������ ���ÿ����� �޴´�

        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else // �˾��� �ƴϸ�
        {
            canvas.sortingOrder = 0;
        }
        
    }
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene// �̸��� T�� ���� �޴� ���� ->name -> prefabs ������ ����  // T��  Ÿ��
    {
        if (string.IsNullOrEmpty(name)) //�̸��� ����ִٸ� ������Ÿ���� name �޾ƿͼ� �־��ֱ�
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}"); // ������ ��ȯ
        T sceneUI = Util.GetOrAddComponent<T>(go); // ��ũ��Ʈ �־��ֱ�
        _sceneUI = sceneUI; 

        go.transform.SetParent(Root.transform); //�θ� �����ؼ� �ѹ��� ����

        //showpopupui���� ���� ���� �� ���ִ� ���� -> ���� �����Ǿ��ִ� ui�� ��Ʈ���Ҷ� ī���Ͱ� �� �ȴ�.
        return sceneUI;
    }
    public T ShowPopupUI<T>(string name =null) where T :UI_Popup// �̸��� T�� ���� �޴� ���� ->name -> prefabs ������ ����  // T��  Ÿ��
    {
        if (string.IsNullOrEmpty(name)) //�̸��� ����ִٸ� ������Ÿ���� name �޾ƿͼ� �־��ֱ�
            name = typeof(T).Name;

        GameObject go= Managers.Resource.Instantiate($"UI/PopUp/{name}"); // ������ ��ȯ
        T popup =  Util.GetOrAddComponent<T>(go); // ��ũ��Ʈ �־��ֱ�
        _popupStack.Push(popup); // ���ÿ� �־��ֱ�
     
        go.transform.SetParent(Root.transform); //�θ� �����ؼ� �ѹ��� ����

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

    public bool isTalk(bool isaction) => (isAction = isaction);


}
