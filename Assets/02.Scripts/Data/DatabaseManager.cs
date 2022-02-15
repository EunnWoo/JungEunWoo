using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DatabaseManager : MonoBehaviour
{
    #region singleton
    static public DatabaseManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    //public string[] var_name;
    //public float[] var;
    //public string[] switch_name;
    //public bool switches;
    //public List<Item> itemList = new List<Item>();
    Dictionary<int, Item> dic_ItemInfo = new Dictionary<int, Item>();

    void Start()
    {
        dic_ItemInfo.Add(100001, new Item(100001, "견습막대", "견습용 막대기", eItemType.Equip));
        dic_ItemInfo.Add(200001, new Item(200001, "파란포션", "MP 100을 회복하는 마법의 물약", eItemType.Use));
        dic_ItemInfo.Add(300001, new Item(300001, "퀘템1", "퀘스트 아이템", eItemType.ETC));

    }
    public Item GetItem(int _itemcode)
    {
        Item _item = null;
        if(dic_ItemInfo.ContainsKey(_itemcode))
        {
            _item = dic_ItemInfo[_itemcode];
        }
        return _item;
        
    }
}
