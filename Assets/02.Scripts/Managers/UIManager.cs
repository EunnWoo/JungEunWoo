using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager
{
    //sortorder
    int _order = 10; // 팝업이 아닌거랑 같아서 10으로
    //가장 나중에 실행된 팝업 먼저 지우기 -> stack

    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>(); //gameobject대신 popup을 넣는 이유 -> 오브젝트는 컴퍼넌트 패턴형식이라 아무런 정보를 가지고 있지않기 때문
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

    public void SetCanvas(GameObject go, bool sort = true)// 외부에서 팝업이 켜질때 셋캔버스 요청 -> order로 우선순위 채워달라
    {
        Canvas canvas= Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true; // 중첩캔버스에서 부모가 어떤 값을 따르던 본인의 소팅오더를 받는다

        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else // 팝업이 아니면
        {
            canvas.sortingOrder = 0;
        }
    }
    public T ShowSceneUI<T>(string name = null) where T : UI_Scene// 이름과 T를 따로 받는 이유 ->name -> prefabs 연동을 위해  // T는  타입
    {
        if (string.IsNullOrEmpty(name)) //이름이 비어있다면 프리팹타입의 name 받아와서 넣어주기
            name = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{name}"); // 프리팹 소환
        T sceneUI = Util.GetOrAddComponent<T>(go); // 스크립트 넣어주기
        _sceneUI = sceneUI; 

        go.transform.SetParent(Root.transform); //부모 지정해서 한번에 관리

        //showpopupui에서 오더 관리 안 해주는 이유 -> 원래 생성되어있던 ui를 컨트롤할때 카운터가 안 된다.
        return sceneUI;
    }
    public T ShowPopupUI<T>(string name =null) where T :UI_Popup// 이름과 T를 따로 받는 이유 ->name -> prefabs 연동을 위해  // T는  타입
    {
        if (string.IsNullOrEmpty(name)) //이름이 비어있다면 프리팹타입의 name 받아와서 넣어주기
            name = typeof(T).Name;

        GameObject go= Managers.Resource.Instantiate($"UI/PopUp/{name}"); // 프리팹 소환
        T popup =  Util.GetOrAddComponent<T>(go); // 스크립트 넣어주기
        _popupStack.Push(popup); // 스택에 넣어주기
     
        go.transform.SetParent(Root.transform); //부모 지정해서 한번에 관리

        //showpopupui에서 오더 관리 안 해주는 이유 -> 원래 생성되어있던 ui를 컨트롤할때 카운터가 안 된다.
        return popup;
    }
    public bool StatePopupUI() // 현재 켜진 팝업이 있다면
    {
        if (_popupStack.Count != 0)
        {
            return true;
        }
        else
            return false;
    }
    public void ClosePopupUI(UI_Popup popup) // 좀 더 안전하다
    {
        if (_popupStack.Count == 0) //스택 건드릴때 카운트 체크하기
            return;

        if(_popupStack.Peek() !=popup) // 마지막으로 담은게 popup이 아니라면
        {
            Debug.Log("Close popup failed!");
            return;
        }
        ClosePopupUI();
    }
    
    public void ClosePopupUI()
    {
        //스택 추출해서 닫기
        if (_popupStack.Count == 0) //스택 건드릴때 카운트 체크하기
            return;
       UI_Popup popup = _popupStack.Pop(); // 뽑아오기
        Managers.Resource.Destroy(popup.gameObject); // 뽑은 후 지우기
        popup = null; // 혹시 모르니 null 또 접근할까봐
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }

    public bool isTalk(bool isaction) => (isAction = isaction);


}
