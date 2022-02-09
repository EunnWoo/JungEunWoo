using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    private void Start()
    {
        if (item == null || (item != null && item.itemCount <= 1))
        {
            itemCount.gameObject.SetActive(false);
        }
    }
    public ItemData item;
    public Image icon;
    public Text itemCount;
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
}
