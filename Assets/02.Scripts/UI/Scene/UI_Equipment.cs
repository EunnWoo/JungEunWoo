using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//                             0     1       2       3       4
public enum eEquipmentSlot { Head, Armor, Weapon, Boots, NotSlot };
[System.Serializable]
public class EquipmentSlot
{
    public string name; //보기용
    public ItemData itemData;
    public Image icon;
    public SkinnedMeshRenderer skin;
    public  Sprite backboarSprite;
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
    public EquipmentSlot[] slots = new EquipmentSlot[4]; //4개의 배열생성\
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
                playerstatus = _ps;// "나" 라는 플래그가 있다면?
                break;
            }
        }
    }


    public void OpenEquipment() //장비창 키는함수(새로운방식)
    {
        body.SetActive(!body.activeSelf);
    }

    public void Invoke_CloseEquipment() //장비창 끄는함수(기존에 알던 방식)
    {
        body.SetActive(false);
    }

    public bool Equip(ItemData _itemData) //장착
    {
        bool _rtn;
        eEquipmentSlot _slot = _itemData.equipmentSlot;
        int _index = (int)_slot;
        if (_index < slots.Length)
        {
            
            if (slots[_index].itemData != null)
            {
                //slots[_index].itemData.equipmentStatus = false;
                slots[_index].itemData = null;
                slots[_index].icon.sprite = slots[_index].backboarSprite;
                slots[_index].icon.color = new Color(1f, 1f, 1f, 100f / 255f); //아이템 탈착시 색깔 연하게


                //slots[_index].skin.gameObject.SetActive(false);
                Debug.Log("@@@ E장착해제");
            }

            //새것
            slots[_index].itemData = _itemData;
            slots[_index].icon.sprite = _itemData.iconSprite;
            slots[_index].icon.color = new Color(1f, 1f, 1f, 1f);//장착시 색깔이 찐하게
            //slots[_index].itemData.equipmentStatus = true;
            Debug.Log("@@@ UI장착중");

            playerstatus.Equip((int)_slot, _itemData);
            _rtn = true;
        }
        return _rtn;
    }

    public void UnEquip(ItemData _itemData)
    {

    }
}