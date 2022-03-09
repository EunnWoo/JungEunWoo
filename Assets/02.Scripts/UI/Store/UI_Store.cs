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

    public void Invoke_HiddenStore() //상점끄기
    {
        Body.SetActive(false);
    }

    public void Show_Store() //상점키기
    {
        Body.SetActive(true);
    }

    void OnClickStoreItem(ItemData _itemData)
    {
        Debug.Log("@@@보유머니 확인후 아이템구매");
        //Debug.Log("@@@ 아이템 구매버튼"+_itemData.itemcode);
        bool _bGet = UI_Inventory.ins.AddItemData(_itemData);//인벤토리에 넣어주기
        if( _bGet)//아이템창이 꽉차서 구매를 못할경우
        {
            Debug.Log("@@@ 보유머니 = 보유머니 - 아이템가격");
            UI_Message.ins.ShowMessage("아이템 구매",_itemData.itemName + "을" + _itemData.itemCount + "구매했습니다");

        }
        else
        {
            //Debug.Log("@@아이템 인벤토리가 가득찼습니다");
            UI_Message.ins.ShowMessage("아이템 구매실패", "인벤토리가 가득찾습니다"); ;
        }
    }
}
