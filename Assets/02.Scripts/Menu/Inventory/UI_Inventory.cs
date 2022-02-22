using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Inventory : MonoBehaviour
{
    #region sigletone
    public static UI_Inventory ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion
    public List<InventorySlot> slotsEquip = new List<InventorySlot>();  //인벤토리 슬롯들
    public List<InventorySlot> slotsUse = new List<InventorySlot>();
    public List<InventorySlot> slotsETC = new List<InventorySlot>();

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


    public GameObject ItemInfo; //인벤토리에서 마우스 올려놓으면 아이템 정보 뜨게하는 오브젝트
    public RectTransform CanvaRect;
    IEnumerator PointerCoroutine;


    // private InventorySlot inven;
    void Start()
    {
        slotsEquip.AddRange(goEquipSlot.transform.GetChild(0).GetComponentsInChildren<InventorySlot>());
        slotsUse.AddRange(goUseSlot.transform.GetChild(0).GetComponentsInChildren<InventorySlot>());
        slotsETC.AddRange(goETCSlot.transform.GetChild(0).GetComponentsInChildren<InventorySlot>());
    }


    public void Invoke_SelectTab(int _kind)
    {
        goEquipTab.SetActive(false);
        goEquipSlot.SetActive(false);
        goUseTab.SetActive(false);
        goUseSlot.SetActive(false);
        goETCTab.SetActive(false);
        goETCSlot.SetActive(false);
        switch (_kind)
        {
            case 0:
                goEquipTab.gameObject.SetActive(true);
                goEquipSlot.SetActive(true);
                Description_Text.text = tabDescription[_kind];


                break;
            case 1:
                goUseTab.SetActive(true);
                goUseSlot.SetActive(true);
                Description_Text.text = tabDescription[_kind];

                break;
            case 2:
                goETCTab.SetActive(true);
                goETCSlot.SetActive(true);
                Description_Text.text = tabDescription[_kind];

                break;

        }

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
            GameManager.isOpenInventory = true;
            body.SetActive(true);
        }
        else
        {
            GameManager.isOpenInventory = false;
            body.SetActive(false);
        }


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
                    Debug.Log("장비: " + _index);
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

    public void PointerEnter(int slotNum) //마우스가 인벤토리 슬롯 위에 올려져있을때
    {
        PointerCoroutine = PointerEnterDelay(slotNum);
        StartCoroutine(PointerCoroutine);
        //ItemInfo.GetComponentInChildren<Text>().text = goEquipSlot[slotNum].Name;
        
    }

    IEnumerator PointerEnterDelay(int slotNum) //마우스가 인벤토리 슬롯 위에 올려져있을때 0.5초뒤에 실행
    {
        yield return new WaitForSeconds(0.3f);
        ItemInfo.SetActive(true);
    }

    public void PointerExit(int slotNum)//마우스가 인벤토리 슬롯 위에 빠져나갈때
    {
        StopCoroutine(PointerCoroutine);
        ItemInfo.SetActive(false);
    }


#if UNITY_EDITOR
    #region 직접입력

    public void AddItem(int _itemcode)
    {
        ItemData _itemData = new ItemData(_itemcode);
        AddItemData(_itemData);
    }


    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory();
        }
        //스크린린포인트 0,0부터 1920,1080 를 새로운 사각형 위치로 변환
        RectTransformUtility.ScreenPointToLocalPointInRectangle(CanvaRect, Input.mousePosition, Camera.main, out Vector2 anchoredPos);

        //유아이창이 뜰위치
        ItemInfo.GetComponent<RectTransform>().anchoredPosition = anchoredPos + new Vector2(700,570);
    }

    #endregion
#endif
}