using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    void OnClickStoreItem(ItemData _itemData)
    {
        Debug.Log("@@@ 아이템 구매버튼"+_itemData.itemcode);
    }
}
