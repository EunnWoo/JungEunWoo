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
    public UI_EquipmentSlot equipSlot;
    public SkinnedMeshRenderer skin;
}


public class UI_Equipment : UI_Scene
{
    #region sigletone
    bool bInit;
    public static UI_Equipment ins;
    private void Awake()
    {
        ins = this;      
    }
    #endregion


    enum GameObjects
    {
        Body,
        HelmetBG,
        ArmorBG,
        BootsBG,
        WeponBG
    }
    enum Texts
    {
        Attack_val,
        Hp_val,
        Mp_val,
        DEF_val
    }

    public EquipmentSlot[] defaultSlots = new EquipmentSlot[4];
    public EquipmentSlot[] slots = new EquipmentSlot[4]; //4���� �迭����

    GameObject body;
    GameObject helmetBG, armorBG, bootsBG, weaponBG;
    PlayerStatus playerstatus;
    Text attText, defText, hpText, mpText;


    public override void Init()
    {
        if (bInit) return;
        bInit = true;

        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        attText = GetText((int)Texts.Attack_val);
        defText = GetText((int)Texts.Hp_val);
        hpText = GetText((int)Texts.Mp_val);
        mpText = GetText((int)Texts.DEF_val);

        body = Get<GameObject>((int)GameObjects.Body);

        helmetBG = Get<GameObject>((int)GameObjects.HelmetBG);
        armorBG = Get<GameObject>((int)GameObjects.ArmorBG); 
        bootsBG = Get<GameObject>((int)GameObjects.BootsBG);
        weaponBG = Get<GameObject>((int)GameObjects.WeponBG);

        slots[0].equipSlot = helmetBG.GetComponent<UI_EquipmentSlot>();
        slots[1].equipSlot = armorBG.GetComponent<UI_EquipmentSlot>();
        slots[2].equipSlot = weaponBG.GetComponent<UI_EquipmentSlot>();
        slots[3].equipSlot = bootsBG.GetComponent<UI_EquipmentSlot>();
        




        playerstatus = Managers.Game.GetPlayer().GetComponent<PlayerStatus>();
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

                ItemData _oldItemData = slots[_index].itemData; //������ �ִ� ��� Ż��
                UI_Inventory.ins.AddItemData(slots[_index].itemData);//��� �����ϸ� UI_Inventory ���� �־���
                slots[_index].itemData = null; //���â ������ ����ش�
                slots[_index].equipSlot.SetItem(null); //slots���ִ� SetItem�� �������� ����

                //���� Mesh - �ɷ�ġ
                playerstatus.UnEquip(_index, _oldItemData);// �ش� ������ �����͸� Ż��
            }

            //����
            Debug.Log("@@@ UI������");
            slots[_index].itemData = _itemData; //slots�� ������ �����͸� ����
            slots[_index].equipSlot.SetItem( _itemData); //_itemData���� ������ �����͸� ����

            //���� Mesh + �ɷ�ġ
            playerstatus.Equip(_index, _itemData);  // �ش� ������ �����͸� ����
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

    public void DisplayAttack(float _att)
    {
        attText.text = ((int)_att).ToString();
    }

    public void DisplayDEF(float _def)
    {
        defText.text = ((int)_def).ToString();
    }

    public void DisplayHP(float _max)
    {
        hpText.text = ((int)_max).ToString();

    }

    public void DisplayMP(float _max)
    {
        mpText.text = ((int)_max).ToString();
    }
}