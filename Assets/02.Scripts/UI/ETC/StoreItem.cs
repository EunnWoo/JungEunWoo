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
        //itemcode -> �������� ����.
        itemData = new ItemData(_itemcode, _itemCount);

        itemImage.sprite = ItemInfo.ins.GetItemInfoSpriteIcon(itemData.itemcode);


        itemName.text = itemData.itemName;
        itemDescrip.text = itemData.iteminfoBase.description;


        on = _on; //�Լ� ���
      
    }
    public void OnClickStoreItem(ItemData _itemData)
    {
        Debug.Log("@@@�����Ӵ� Ȯ���� �����۱���");
        //Debug.Log("@@@ ������ ���Ź�ư"+_itemData.itemcode);
        bool _bGet = UI_Inventory.ins.AddItemData(_itemData);//�κ��丮�� �־��ֱ�
        if (_bGet)//������â�� ������ ���Ÿ� ���Ұ��
        {
         //   Debug.Log("@@@ �����Ӵ� = �����Ӵ� - �����۰���");
         //   ui_Message.ShowMessage("������ ����", _itemData.itemName + "��" + _itemData.itemCount + "�� �����߽��ϴ�");

        }
        else
        {
            //Debug.Log("@@������ �κ��丮�� ����á���ϴ�");
          //  ui_Message.ShowMessage("������ ���Ž���", "�κ��丮�� ����ã���ϴ�"); ;
        }
        Managers.UI.ClosePopupUI(ui_Message);
      
    }

    public void ItemClick(PointerEventData eventData) // ������ ����Ŭ�� ������ �Һ����̸� ���� / ����� �׳� �����ϰڴ��� ���
    {
        int clickCount = eventData.clickCount;
        if (clickCount == 2)//�ι� Ŭ����
        {
           if(on != null)
            {
                //���⼭ showmessage ȣ���������
                ui_Message = Managers.UI.ShowPopupUI<UI_Message>();
                ui_Message.Init();
                ui_Message.okButton.gameObject.AddUIEvent(BuyOk);

                if (itemData.itemcode / 10000 == 1)
                {
                    ui_Message.countSlider.gameObject.SetActive(true);
                }
                else if (itemData.itemcode / 10000 == 2)
                {
                    ui_Message.ShowMessage("����", itemData.itemName + "�� �����ϰڽ��ϱ�?");
                }
            }
        }
    }

    public void BuyOk(PointerEventData data)
    {
        if (on != null)
        {
            itemData.itemCount = (int)ui_Message.countSlider.value; // �����̴� ���� ����ɶ� showmessage ȣ���ؼ� ������ �ؽ�Ʈ �����
            on(itemData); // OnClickStoreItem ȣ��
        }
    }
}
