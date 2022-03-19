using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class UI_Inventory : UI_Scene
{
    #region sigletone
    bool bInit;
    public static UI_Inventory ins;
    private void Awake()
    {
        ins = this;
        Init();
    }
    #endregion

    
    public List<UI_InventorySlot> slotsEquip = new List<UI_InventorySlot>();  //인벤토리 슬롯들
    public List<UI_InventorySlot> slotsUse = new List<UI_InventorySlot>();
    public List<UI_InventorySlot> slotsETC = new List<UI_InventorySlot>();

    public Text Description_Text; //아이템에 대한 부연설명
    public string[] tabDescription; //탭 부연설명

    public GameObject goEquipTab;
    public GameObject goEquipSlot;
    public GameObject goUseTab;
    public GameObject goUseSlot;
    public GameObject goETCTab;
    public GameObject goETCSlot;
    public GameObject body;

    public int MAX_EQUIP = 20;
    public List<ItemData> listEquip = new List<ItemData>();
    public List<ItemData> listUse = new List<ItemData>();
    public List<ItemData> listETC = new List<ItemData>();


    public GameObject itemInfo; //인벤토리에서 마우스 올려놓으면 아이템 정보 뜨게하는 오브젝트
    public RectTransform CanvaRect;
    public Vector2 v;
    IEnumerator PointerCoroutine;

    enum GameObjects
    {
        EquipBody,
        UseBody,
        ETCBody,
        Equip_GridSlot,
        Use_GridSlot,
        ETC_GridSlot,
        EquipPanel,
        UsePanel,
        ETCPanel,
        ItemInfo
    }
    enum Buttons
    {
        Equip_Selected_Tab,
        Use_Selected_Tab,
        ETC_Selected_Tab,
        CloseButton
    }

    public override void Init()
    {
        if (bInit) return;
        bInit = true;

        base.Init();
        #region setup
        Bind<GameObject>(typeof(GameObjects));
        Bind<Button>(typeof(Buttons));
        goEquipTab = Get<GameObject>((int)GameObjects.EquipPanel);
        goUseTab = Get<GameObject>((int)GameObjects.UsePanel);
        goETCTab = Get<GameObject>((int)GameObjects.ETCPanel);
        goEquipSlot = Get<GameObject>((int)GameObjects.Equip_GridSlot); 
        goUseSlot = Get<GameObject>((int)GameObjects.Use_GridSlot);
        goETCSlot = Get<GameObject>((int)GameObjects.ETC_GridSlot);
        itemInfo = Get<GameObject>((int)GameObjects.ItemInfo);
        #endregion
        #region invenslotSet
        GameObject equipbody = Get<GameObject>((int)GameObjects.EquipBody);
        GameObject usebody = Get<GameObject>((int)GameObjects.UseBody);
        GameObject etcbody = Get<GameObject>((int)GameObjects.ETCBody);

        GetButton((int)Buttons.CloseButton).gameObject.AddUIEvent(CloseInventory);
        


        itemInfo.SetActive(false);

        SetSlot(equipbody, slotsEquip);
        SetSlot(usebody, slotsUse);
        SetSlot(etcbody, slotsETC);

        goUseSlot.SetActive(false); //켜고 소비랑 기타 꺼주기
        goETCSlot.SetActive(false);
        goUseTab.SetActive(false);
        goETCTab.SetActive(false);
        #endregion


        //탭선택 이벤트 부여
        GetButton((int)Buttons.Equip_Selected_Tab).gameObject.AddUIEvent(Invoke_EquipTab);
        GetButton((int)Buttons.Use_Selected_Tab).gameObject.AddUIEvent(Invoke_UseTab);
        GetButton((int)Buttons.ETC_Selected_Tab).gameObject.AddUIEvent(Invoke_ETCTab);


        body.SetActive(false); //생성되면 꺼주기

    }
    void SetSlot(GameObject _go, List<UI_InventorySlot> _slotList)
    {
        GameObject _itemGO;
        UI_InventorySlot _inventorySlot;
        
        for (int i = 0; i < 20; i++)
        {
            _itemGO = Managers.Resource.Instantiate("UI/Popup/UI_InventorySlot");
            _inventorySlot = _itemGO.GetComponent<UI_InventorySlot>();
            _slotList.Add(_inventorySlot);
            _inventorySlot.Init();
            _itemGO.transform.SetParent(_go.transform);
            _itemGO.transform.localScale = Vector3.one;
        }
    }
    public void Invoke_SetTab()
    {
        goEquipTab.SetActive(false);
        goEquipSlot.SetActive(false);
        goUseTab.SetActive(false);
        goUseSlot.SetActive(false);
        goETCTab.SetActive(false);
        goETCSlot.SetActive(false);
    }
    public void Invoke_EquipTab(PointerEventData data)
    {
        Invoke_SetTab();
        goEquipTab.SetActive(true);
        goEquipSlot.SetActive(true);
        Description_Text.text = tabDescription[0];
    }
    public void Invoke_UseTab(PointerEventData data)
    {
        Invoke_SetTab();
        goUseTab.SetActive(true);
        goUseSlot.SetActive(true);
        Description_Text.text = tabDescription[1];

    }
    public void Invoke_ETCTab(PointerEventData data)
    {
        Invoke_SetTab();
        goETCTab.SetActive(true);
        goETCSlot.SetActive(true);
        Description_Text.text = tabDescription[2];
    }
    public void Invoke_Close()
    {
        //GameManager.isOpenInventory = false;
        body.SetActive(false);
    }

    public void OpenInventory()
    {
        //GameManager.isOpenInventory = true;
        if (!body.activeSelf)
        {
            Managers.Game.isOpenInventory = true;
            body.SetActive(true);
        }
        else
        {
            Managers.Game.isOpenInventory = false;
            body.SetActive(false);
        }


    }
    public void CloseInventory(PointerEventData data)
    {
        Managers.Game.isOpenInventory = false;
        body.SetActive(false);
    }
    ItemData CheckItemData(List <ItemData>_list, ItemData _newItemData, out int _index)//동일한 아이템이 있는지 검사
    {
        _index = -1;
        foreach(ItemData _itemData in _list)
        {
            _index++;
            if(_itemData.itemcode == _newItemData.itemcode)//아이템이 새로들어온게 현재에 있는 아이템과 같을경우
            {
                return _itemData;
            }
        }
        return null;
    }

    //획득한 아이템 클래스를 그대로 받아서 넣어줌
    public bool AddItemData(ItemData _itemDataNew)
    {
        
        int _index = -1;
        bool _rtn = false;
        ItemData _itemDataOld = null;
        switch (_itemDataNew.itemType)    //탭 에따른 아이템 분류, 그것을 인벤토리 탭 리스트에 추가
        {
           case eItemType.Equip: //장비창, 장비템이면 한칸에 한개씩
                if (listEquip.Count < MAX_EQUIP) 
                {
                    //Max 상태가 아니면 추가작업, 인덱스도 증가
                    listEquip.Add(_itemDataNew);
                    _index = listEquip.Count - 1;
                    //Debug.Log("장비: " + _index);
                    slotsEquip[_index].SetItem(_itemDataNew);

                    //추가 되었다는 플래그를 알려줌

                    _rtn = true;
                }
                break;

            case eItemType.Use: //소비창

                //아이템리스트에서 같은 아이템이 있는지 검색해서 같은 아이템이 있으면 추가기능
                _itemDataOld = CheckItemData(listUse, _itemDataNew, out _index);
                if(_itemDataOld != null)
                {
                    //새로 얻은 아이템이 기존에 있는경우
                    //기존값에 plus
                    _itemDataOld.itemCount += _itemDataNew.itemCount;
                    slotsUse[_index].ReDisplayCount();
                    _rtn = true;

                }

                else if (listUse.Count < MAX_EQUIP)
                {   //신규획득 할경우
                    listUse.Add(_itemDataNew);
                    _index = listUse.Count - 1;
                    Debug.Log("소비: " + _index);
                    slotsUse[_index].SetItem(_itemDataNew);
                    _rtn = true;
                }
                break;

            case eItemType.ETC: //기타창
                _itemDataOld = CheckItemData(listETC, _itemDataNew, out _index);
                if (_itemDataOld != null)
                {//새로 얻은 아이템이 기존에 있는경우
                    //기존값에 plus
                    _itemDataOld.itemCount += _itemDataNew.itemCount;
                    slotsETC[_index].ReDisplayCount();
                    _rtn = true;

                }
                else if (listETC.Count < MAX_EQUIP)
                {
                    listETC.Add(_itemDataNew);
                    _index = listETC.Count - 1;
                    slotsETC[_index].SetItem(_itemDataNew);
                    _rtn = true;
                }

                break;
        }
        return _rtn;
    }


    public void RemoveItemData(ItemData _itemDataRemove)
    {

    }

    #region 추후 시간남을시 작업예정
    //public void PointerEnter(PointerEventData data) //마우스가 인벤토리 슬롯 위에 올려져있을때
    //{
    //    PointerCoroutine = PointerEnterDelay();
    //    StartCoroutine(PointerCoroutine);

    //    RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvaRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);
    //    itemInfo.GetComponent<RectTransform>().anchoredPosition = anchoredPos;
    //}


    //IEnumerator PointerEnterDelay() //마우스가 인벤토리 슬롯 위에 올려져있을때 0.5초뒤에 실행
    //{
    //    yield return new WaitForSeconds(0.3f);
    //    itemInfo.SetActive(true);
    //    StoreItem.ins.itemDescrip.text = StoreItem.ins.itemData.iteminfoBase.description;
    //    //ItemInfo.GetComponentInChildren<Text>().text = 
    //}

    //public void PointerExit()//마우스가 인벤토리 슬롯 위에 빠져나갈때
    //{
    //    StopCoroutine(PointerCoroutine);
    //    itemInfo.SetActive(false);
    //}
    #endregion


#if UNITY_EDITOR
    #region 직접입력

    public void AddItem(int _itemcode)
    {
        ItemData _itemData = new ItemData(_itemcode);
        AddItemData(_itemData);
    }


    void Update()
    {
        //스크린린포인트 0,0부터 1920,1080 를 새로운 사각형 위치로 변환
        //RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvaRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);

        //마우스를 아이템위로 올릴시 설명 유아이창이 뜰위치
        //itemInfo.GetComponent<RectTransform>().anchoredPosition = anchoredPos + new Vector2(700,570);
    }

    #endregion
#endif
}