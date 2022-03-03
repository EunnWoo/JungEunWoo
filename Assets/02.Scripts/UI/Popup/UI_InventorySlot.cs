using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_InventorySlot : UI_Base, IPointerClickHandler
{
    public ItemData item;
    Image icon;
    Text itemCount;

    enum  Images
    {
        Item_Icon,
        Panel
    }
    enum Texts
    {
        Item_Count_Text
    }
    public override void  Init()
    {
        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));
        
        icon = GetImage((int)Images.Item_Icon);
        itemCount = GetText((int)Texts.Item_Count_Text);
        
        if (item == null || (item != null && item.itemCount <= 1))
        {
            itemCount.gameObject.SetActive(false); //아이템 수량 나오는것을 끔
        }


        AddUIEvent(icon.gameObject, OnPointerClick); // ebenthandler 접근하려고 

    }

    public void SetItem(ItemData _item)
    {
        item = _item;
        icon.sprite = ItemInfo.ins.GetItemInfoSpriteIcon(_item.itemcode);
        if (item.itemCount <= 1)
        {
            itemCount.gameObject.SetActive(false);
        }
        else
        {
            itemCount.gameObject.SetActive(true);
            itemCount.text = "x" + item.itemCount;
        }
    }
    public void ReDisplayCount()
    {
        itemCount.gameObject.SetActive(true);
        itemCount.text = "x" + item.itemCount;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;

        if (clickCount == 1) //한번클릭
            OnSingleClick();
        else if (clickCount == 2)//두번 클릭시
            OnDoubleClick();
    }

    void OnSingleClick() //한번 클릭시
    {
        //Debug.Log("Single Clicked");
    }

    void OnDoubleClick() //더블클릭시
    {
        if (item == null || item.itemcode ==0) return;//아이템데이터가 null이면 리턴
        switch(item.itemType)
        {
            case eItemType.Equip:   //장비창에서 더블클릭시
                Debug.Log("더블클릭 >>장비교체");
                break;
            case eItemType.Use:   //소비창에서 더블클릭시

                //물약을 먹음
                ItemInfoUsepart _usePart = ItemInfo.ins.GetItemInfoUsepart(item.itemcode);

                PlayerStatus.ins.SetHPMP(_usePart.hp, _usePart.mp);

                item.itemCount--; //아이템에서 수량을 한개빼줌
                if(item.itemCount <= 0)//아이템 수량이 0개가되면
                {
                    item = null; //아이템을 지움
                    icon.sprite = null; //아이콘을 지움
                    Init();
                }
                else
                {
                    SetItem(item);
                }
                Debug.Log("더블클릭 >>물약먹기 HP:" + _usePart.hp + " MP: " + _usePart.mp);
                break;
            case eItemType.ETC:   //기타창에서 더블클릭시
                Debug.Log("더블클릭 >>작동안함");
                break;
        }
        Debug.Log("Double Clicked");
    }
}

