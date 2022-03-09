using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class StoreItem : MonoBehaviour, IPointerClickHandler
{
    public Image itemImage;
    public Text itemName;
    public Text itemDescrip;
    public ItemData itemData;
    public System.Action<ItemData> on;

    public void SetItem(int _itemcode, int _itemCount, System.Action<ItemData> _on)
    {
        gameObject.SetActive(true);

        //itemcode -> 아이템을 세팅.
        itemData = new ItemData(_itemcode, _itemCount);
        itemImage.sprite = ItemInfo.ins.GetItemInfoSpriteIcon(itemData.itemcode);
        itemName.text = itemData.itemName;
        itemDescrip.text = itemData.iteminfoBase.description;


        on = _on;
        
        //ItemData _item;
        //item = _Item;
        //icon.sprite = ItemInfo.ins.GetItemInfoSpriteIcon
        
        
        
        //Button _button =  GetComponent<Button>();
        //_button.onClick.AddListener(() =>
        //{
        //    if (_on != null)
        //    {
        //        _on(itemData);
        //    }
        //});
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;

        if (clickCount == 2)//두번 클릭시
        {
           if(on != null)
            {
                on(itemData);
            }
        }
    }

}
