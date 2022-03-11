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

    UI_Message ui_Message;
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


        on = _on; //함수 담기
      
    }
    public void OnClickStoreItem(ItemData _itemData)
    {
        Debug.Log("@@@보유머니 확인후 아이템구매");
        //Debug.Log("@@@ 아이템 구매버튼"+_itemData.itemcode);
        bool _bGet = UI_Inventory.ins.AddItemData(_itemData);//인벤토리에 넣어주기
        if (_bGet)//아이템창이 꽉차서 구매를 못할경우
        {
            Debug.Log("@@@ 보유머니 = 보유머니 - 아이템가격");
            ui_Message.ShowMessage("아이템 구매", _itemData.itemName + "을" + _itemData.itemCount + "개 구매했습니다");

        }
        else
        {
            //Debug.Log("@@아이템 인벤토리가 가득찼습니다");
            ui_Message.ShowMessage("아이템 구매실패", "인벤토리가 가득찾습니다"); ;
        }

      
    }

    public void ItemClick(PointerEventData eventData)
    {
        int clickCount = eventData.clickCount;
        if (clickCount == 2)//두번 클릭시
        {
           if(on != null)
            {
                //여기서 showmessage 호출해줘야함
                ui_Message = Managers.UI.ShowPopupUI<UI_Message>();
                ui_Message.Init();
                ui_Message.okButton.gameObject.AddUIEvent(BuyOk);
                
                ui_Message.countSlider.gameObject.SetActive(true);

            }
        }
    }

    public void BuyOk(PointerEventData data)
    {
        if (on != null)
        {
            Debug.Log("함수호출");
            ui_Message.okButton.gameObject.AddUIEvent(ui_Message.Cancel);
            itemData.itemCount = (int)ui_Message.countSlider.value;
            on(itemData); // OnClickStoreItem 호출
            
        }

    }
}
