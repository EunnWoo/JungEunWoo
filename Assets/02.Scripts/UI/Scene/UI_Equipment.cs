using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum eEquipmentSlot { Head, Armor, Weapon, Boots, NotSlot };
[System.Serializable]
public class EquipmentSlot
{
    public string name; //�����
    public ItemData itemData;
    public Image icon;
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
    public EquipmentSlot[] slots = new EquipmentSlot[4]; //4���� �迭����

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        if (bInit) return;
        bInit = true;
        body.SetActive(false);
    }


    public void OpenEquipment() //���â Ű���Լ�(���ο���)
    {
        body.SetActive(!body.activeSelf);
    }

    public void Invoke_CloseEquipment() //���â �����Լ�(������ �˴� ���)
    {
        body.SetActive(false);
    }

    public void Equip(ItemData _itemData)
    {
        eEquipmentSlot _slot = _itemData.equipmentSlot;
        int _index = (int)_slot;
        if (_index < slots.Length)
        {
            
            if (slots[_index].itemData != null)
            {
                slots[_index].itemData.equipmentStatus = false;
                slots[_index].itemData = null;

                //slots[_index].skin.gameObject.SetActive(false);
                Debug.Log("@@@ E��������");
            }

            //����
            slots[_index].itemData = _itemData;
            slots[_index].icon.sprite = ItemInfo.ins.GetItemInfoSpriteIcon(_itemData.itemcode);
            slots[_index].itemData.equipmentStatus = true;
            Debug.Log("@@@ UI������");

            //slots[_index].skin =
            //slots[_index].skin.gameObject.SetActive(true);
        }
    }

    public void UnEquip(ItemData _itemData)
    {

    }
}