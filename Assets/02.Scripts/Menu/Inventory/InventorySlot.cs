using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public int itemCount;
    public Image icon;

    //필요한 컴퍼넌트
    [SerializeField]
    private Text text_Count;
    
    public void SetItem(Item _item) //아이템 획득
    {
        item = _item;
        icon.sprite = item.itemIcon;
        text_Count.text = itemCount.ToString();
    }

    public void SetSlotCount(int _count) //아이템 갯수조정
    {
        itemCount += _count;
        text_Count.text = itemCount.ToString();
        if (itemCount <= 0)
            ClearSlot();
    }

    private void ClearSlot()    //아이템이 0일시 슬롯 초기화
    {
        item = null;
        itemCount = 0;
        icon.sprite = null;
    }
}
