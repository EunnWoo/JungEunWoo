using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UI_Inventory : MonoBehaviour
{
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
    public List<Item> listEquip = new List<Item>();
    public List<Item> listUse = new List<Item>();
    public List<Item> listETC = new List<Item>();

    // private InventorySlot inven;
    void Start()
    {
        slotsEquip.AddRange (goEquipSlot.transform.GetChild(0).GetComponentsInChildren<InventorySlot>());
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
        body.SetActive(false);
    }

    public void OpenInventory()
    {
        if (!body.activeSelf)
        {
            body.SetActive(true);
        }
        else
        {
            body.SetActive(false);
        }


    }

    public bool AddItem(Item _newItem)
    {
        int _index = -1;
        bool _rtn = false;
        switch (_newItem.itemType)    //탭 에따른 아이템 분류, 그것을 인벤토리 탭 리스트에 추가
        {
            case eItemType.Equip: //장비창
                if (listEquip.Count < MAX_EQUIP)
                {
                    listEquip.Add(_newItem);
                    _index = listEquip.Count - 1;
                    slotsEquip[_index].SetItem(_newItem);
                    _rtn = true;
                }
            break;

            case eItemType.Use: //소비창
                if (listUse.Count < MAX_EQUIP)
                {
                    listUse.Add(_newItem);
                    _index = listUse.Count - 1;
                    slotsUse[_index].SetItem(_newItem);
                    _rtn = true;
                }
                break;

            case eItemType.ETC: //기타창
                if (listETC.Count < MAX_EQUIP)
                {
                    listETC.Add(_newItem);
                    _index = listETC.Count - 1;
                    slotsETC[_index].SetItem(_newItem);
                    _rtn = true;
                }

                break;          
        }
        return _rtn;
    }


   


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            AddItem(DatabaseManager.instance.GetItem(100001));
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AddItem(DatabaseManager.instance.GetItem(200001));
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AddItem(DatabaseManager.instance.GetItem(300001));
        }
        if (Input.GetKeyDown(KeyCode.I ))
            {
            OpenInventory();
            }
    }
}