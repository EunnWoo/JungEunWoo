using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
[System.Serializable]
public class StoreItemInfo
{
    public int itemcode;
    public int itemCount = 1;
}

public class UI_Store : UI_Popup
{

    enum Buttons
    {
        StoreItem,
        CloseButton
    }
    enum GameObjects
    {
        ContentGroup,
    }

    #region sigletone
    public static UI_Store ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion

    GameObject content;
    StoreItem storeItem; // == StoreItem
    Button closeButton;


    List<StoreItemInfo> listInfo = new List<StoreItemInfo>();
    List<StoreItem> list = new List<StoreItem>();


    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        storeItem = GetButton((int)Buttons.StoreItem).GetComponent<StoreItem>();
        content = Get<GameObject>((int)GameObjects.ContentGroup);
        closeButton = GetButton((int)Buttons.CloseButton);

        ListInit();
        StoreItem[] _storeItem = new StoreItem[listInfo.Count];
        // StoreItemInfo _info;
        for (int i = 0; i < _storeItem.Length; i++)
        {
            _storeItem[i] = Managers.Resource.Instantiate("StoreItem", content.transform).GetComponent<StoreItem>();
            _storeItem[i].Init();
            _storeItem[i].SetItem(listInfo[i].itemcode, listInfo[i].itemCount, OnClickStoreItem);
            AddUIEvent(_storeItem[i].gameObject, _storeItem[i].ItemClick);
            list.Add(_storeItem[i]);
        }
  
        Destroy(storeItem.gameObject);
        closeButton.gameObject.AddUIEvent(OnClose);
        
    }
    public void ListInit()
    {
        listInfo.Add(new StoreItemInfo() { itemcode = 10001, itemCount = 10 });
        listInfo.Add(new StoreItemInfo() { itemcode = 10002, itemCount = 10 });
        listInfo.Add(new StoreItemInfo() { itemcode = 20004, itemCount = 1});
        listInfo.Add(new StoreItemInfo() { itemcode = 20005, itemCount = 1 });
        listInfo.Add(new StoreItemInfo() { itemcode = 20006, itemCount = 1 });
    } // 상점에 아이템 추가



    
    void OnClickStoreItem(ItemData _itemData)
    {
        Debug.Log("@@@보유머니 확인후 아이템구매");
        //Debug.Log("@@@ 아이템 구매버튼"+_itemData.itemcode);
        //100개

        bool _bGet = UI_Inventory.ins.AddItemData(_itemData);//인벤토리에 넣어주기
        
        UI_Message ui_Message =  Managers.UI.ShowPopupUI<UI_Message>();
        ui_Message.Init();
        if ( _bGet)//아이템창이 꽉차서 구매를 못할경우
        {
            Debug.Log("@@@ 보유머니 = 보유머니 - 아이템가격");
            ui_Message.ShowMessage("아이템 구매",_itemData.itemName + "을" + _itemData.itemCount + "개 구매했습니다");

        }
        else
        {
            //Debug.Log("@@아이템 인벤토리가 가득찼습니다");
            ui_Message.ShowMessage("아이템 구매실패", "인벤토리가 가득찾습니다"); ;
        }
    }



    void OnClose(PointerEventData data)
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.isTalk(false);
    }
}
