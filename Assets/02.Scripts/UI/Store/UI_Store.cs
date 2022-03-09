using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class StoreItemInfo
{
    public int itemcode;
    public int itemCount = 1;
}

public class UI_Store : MonoBehaviour
{
    #region sigletone
    public static UI_Store ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion

    public GameObject Body;
    public GameObject content;
    public StoreItem prefabStoreItem;
    public List<StoreItemInfo> listInfo = new List<StoreItemInfo>();
    public List<StoreItem> list = new List<StoreItem>();
    void Start()
    {
        Body.SetActive(false);

        StoreItem _storeItem;
        StoreItemInfo _info;
        for (int i = 0; i < listInfo.Count; i++)
        {
            _info = listInfo[i];
            _storeItem = Instantiate(prefabStoreItem, content.transform) as StoreItem;
            _storeItem.SetItem(_info.itemcode, _info.itemCount, OnClickStoreItem);
            

            list.Add(_storeItem);
        }
        Destroy(prefabStoreItem.gameObject);
    }

    public void Invoke_HiddenStore() //��������
    {
        Body.SetActive(false);
    }

    public void Show_Store() //����Ű��
    {
        Body.SetActive(true);
    }

    void OnClickStoreItem(ItemData _itemData)
    {
        Debug.Log("@@@�����Ӵ� Ȯ���� �����۱���");
        //Debug.Log("@@@ ������ ���Ź�ư"+_itemData.itemcode);
        bool _bGet = UI_Inventory.ins.AddItemData(_itemData);//�κ��丮�� �־��ֱ�
        if( _bGet)//������â�� ������ ���Ÿ� ���Ұ��
        {
            Debug.Log("@@@ �����Ӵ� = �����Ӵ� - �����۰���");
            UI_Message.ins.ShowMessage("������ ����",_itemData.itemName + "��" + _itemData.itemCount + "�����߽��ϴ�");

        }
        else
        {
            //Debug.Log("@@������ �κ��丮�� ����á���ϴ�");
            UI_Message.ins.ShowMessage("������ ���Ž���", "�κ��丮�� ����ã���ϴ�"); ;
        }
    }
}
