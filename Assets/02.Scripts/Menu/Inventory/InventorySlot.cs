using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Item item;
    public Image icon;
    public void SetItem(Item _item)
    {
        item = _item;
    }
//    public Image icon;
//    public Text itemCount_Text;
//    public GameObject selected_Item;

    //    public void Additem(Item _item)
    //    {
    //        icon.sprite = _item.itemIcon;
    //        if(eItemType.Use == _item.itemType)
    //        {
    //        if(_item.itemCount >0)
    //        itemCount_Text.text = "x" + _item.itemCount.ToString();
    //        else
    //        itemCount_Text.text = "";
    //        }
    //    }

    //    public void RemoveItem()
    //    {
    //        itemCount_Text.text = "";
    //        icon.sprite = null;
    //    }
}
