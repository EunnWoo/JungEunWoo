﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_EquipmentSlot : MonoBehaviour, IPointerClickHandler
{
    bool bInit;
    public ItemData itemData;
    public Image icon;
    private Sprite defaulticonSprite;


    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        if (bInit) return;
        bInit = true;
        defaulticonSprite = icon.sprite;
    }


    public void SetItem(ItemData _itemData)
    {
        if(_itemData ==null)
        {
            //장비 해제
        itemData = null;
        icon.sprite = defaulticonSprite;
        icon.color = new Color(1f, 1f, 1f, 100f / 255f);//아이템 탈착시 색깔 연하게
        }
        else
        {
            itemData = _itemData;
            icon.sprite = _itemData.iconSprite;
            icon.color = new Color(1f, 1f, 1f, 1f);//아이템 장착시 색깔 진하게
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;

          
        if (clickCount == 2)//두번 클릭시
            OnDoubleClick();
    }

    

    void OnDoubleClick() //더블클릭시
    {
        //장비교체
        if (itemData == null || itemData.itemcode ==0) return;//아이템데이터가 null이면 리턴
 
        switch(itemData.itemType)
        {
            case eItemType.Equip:   //장비창에서 더블클릭시
                UI_Equipment.ins.UnEquip(itemData);
                break;
        }
    }
}

