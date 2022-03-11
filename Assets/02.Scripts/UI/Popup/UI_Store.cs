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
            _storeItem[i].SetItem(listInfo[i].itemcode, listInfo[i].itemCount, OnClickStoreItem); // itemdata ����
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
    } // ������ ������ �߰�


    
    
    public void OnClickStoreItem(ItemData _itemData)
    {
        Debug.Log("@@@�����Ӵ� Ȯ���� �����۱���");
        //Debug.Log("@@@ ������ ���Ź�ư"+_itemData.itemcode);
        //100��
        
        UI_Message ui_Message =  Managers.UI.ShowPopupUI<UI_Message>();


        bool _bGet = UI_Inventory.ins.AddItemData(_itemData);//�κ��丮�� �־��ֱ�
        ui_Message.Init();
        if ( _bGet)//������â�� ������ ���Ÿ� ���Ұ��
        {
            Debug.Log("@@@ �����Ӵ� = �����Ӵ� - �����۰���");
            ui_Message.ShowMessage("������ ����",_itemData.itemName + "��" + _itemData.itemCount + "�� �����߽��ϴ�");

        }
        else
        {
            //Debug.Log("@@������ �κ��丮�� ����á���ϴ�");
            ui_Message.ShowMessage("������ ���Ž���", "�κ��丮�� ����ã���ϴ�"); ;
        }
    }



    void OnClose(PointerEventData data)
    {
        Managers.UI.ClosePopupUI(this);
        Managers.UI.isTalk(false);
    }
}
