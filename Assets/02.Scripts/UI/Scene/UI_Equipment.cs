using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//                             0     1       2       3       4
public enum eEquipmentSlot { Head, Armor, Weapon, Boots, NotSlot };
[System.Serializable]
public class EquipmentSlot
{
    public string name; //�����
    public ItemData itemData;
    public UI_EquipmentSlot equipSlot;
    public SkinnedMeshRenderer skin;
}


public class UI_Equipment : MonoBehaviour
{
    #region sigletone
    bool bInit;
    public static UI_Equipment ins;
    private void Awake()
    {
        ins = this;
        Init();
    }
    #endregion

    public GameObject body;
    public EquipmentSlot[] defaultSlots = new EquipmentSlot[4];
    public EquipmentSlot[] slots = new EquipmentSlot[4]; //4���� �迭����\
    PlayerStatus playerstatus;


    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (bInit) return;
        bInit = true;
        body.SetActive(false);

        GameObject[] _arrayGO = GameObject.FindGameObjectsWithTag("Player");
        PlayerStatus _ps;
        for(int i = 0; i< _arrayGO.Length; i++)
        {
            _ps = _arrayGO[i].GetComponent<PlayerStatus>();
            if(_ps != null)
            {
                playerstatus = _ps;// "��" ��� �÷��װ� �ִٸ�?
                break;
            }
        }
    }


    public void OpenEquipment() //���â Ű���Լ�(���ο���)
    {
        body.SetActive(!body.activeSelf);
    }

    public void Invoke_CloseEquipment() //���â �����Լ�(������ �˴� ���)
    {
        body.SetActive(false);
    }

    public bool Equip(ItemData _itemData) //����
    {
        bool _rtn = false;
        eEquipmentSlot _slot = _itemData.equipmentSlot;
        int _index = (int)_slot;
        if (_index < slots.Length)
        {
            
            if (slots[_index].itemData != null && slots[_index].itemData.itemcode != 0)
            {
                Debug.Log("@@@ E��������");

                UI_Inventory.ins.AddItemData(slots[_index].itemData);
                slots[_index].itemData = null;
                slots[_index].equipSlot.SetItem(null);
            }

            //����
            Debug.Log("@@@ UI������");
            slots[_index].itemData = _itemData;
            slots[_index].equipSlot.SetItem( _itemData);

            playerstatus.Equip((int)_slot, _itemData);
            _rtn = true;
        }
        return _rtn;
    }

    public void UnEquip(ItemData _itemData)//Ż��
    {
        eEquipmentSlot _slot = _itemData.equipmentSlot;
        int _index = (int)_slot;
        if (_index < slots.Length)
        {

            if (slots[_index].itemData != null)
            {
                Debug.Log("@@@ E��������");
                slots[_index].itemData = null;
                slots[_index].equipSlot.SetItem(null);

                UI_Inventory.ins.AddItemData(_itemData);
            }
            playerstatus.UnEquip((int)_slot, _itemData);
        }
    }
}