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
    public Item item;
    public Image icon;
    public Text itemCount;
    public void SetItem(Item _item)
    {
        item = _item;
        icon.sprite = item.itemIcon;
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
