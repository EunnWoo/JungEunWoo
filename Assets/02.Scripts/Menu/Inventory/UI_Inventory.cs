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

    public bool AddItem(int _itemcode)
    {
        ItemData _itemData = new ItemData(_itemcode);
        int _index = -1;
        bool _rtn = false;
        switch (_itemData.itemType)    //탭 에따른 아이템 분류, 그것을 인벤토리 탭 리스트에 추가
        {
            case eItemType.Equip: //장비창
                if (listEquip.Count < MAX_EQUIP)
                {
                    listEquip.Add(_itemData);
                    _index = listEquip.Count - 1;
                    Debug.Log("장비: " + _index);
                    slotsEquip[_index].SetItem(_itemData);
                    _rtn = true;
                }
                break;

            case eItemType.Use: //소비창
                if (listUse.Count < MAX_EQUIP)
                {
                    listUse.Add(_itemData);
                    _index = listUse.Count - 1;
                    Debug.Log("소비: " + _index);
                    slotsUse[_index].SetItem(_itemData);
                    _rtn = true;
                }
                break;

            case eItemType.ETC: //기타창
                if (listETC.Count < MAX_EQUIP)
                {
                    listETC.Add(_itemData);
                    _index = listETC.Count - 1;
                    slotsETC[_index].SetItem(_itemData);
                    _rtn = true;
                }

                break;
        }
        return _rtn;
    }





    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddItem(20001);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddItem(20002);
        }
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            OpenInventory();
        }
    }
}