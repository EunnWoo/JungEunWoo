using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class StoreItem : UI_Base  
{
    enum Images
    {
        ItemImage
    }
    enum Texts
    {
        ItemName,
        ItemDescrip
    }

    #region sigletone
    public static StoreItem ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion

    bool bInit;

    Image itemImage;
    Text itemName;
    [HideInInspector]
    public Text itemDescrip;

    [HideInInspector]
    public ItemData itemData;

    public System.Action<ItemData> on;

    public override void Init()
    {
        if (bInit) return;
        bInit = true;


        Bind<Image>(typeof(Images));
        Bind<Text>(typeof(Texts));

        itemImage = GetImage((int)Images.ItemImage);

        itemName = GetText((int)Texts.ItemName);
        itemDescrip = GetText((int)Texts.ItemDescrip);
 
    }
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

    public void ItemClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;
        Debug.Log(clickCount);
        if (clickCount == 2)//두번 클릭시
        {
           if(on != null)
            {
                on(itemData);
            }
        }
    }

}
