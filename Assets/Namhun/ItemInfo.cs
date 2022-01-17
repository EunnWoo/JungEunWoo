using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class ItemInfo : MonoBehaviour
{
    public SpriteAtlas attlas;
    #region sigletone
    public static ItemInfo ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion
    public Dictionary<int, ItemInfoBase> dic_Base = new Dictionary<int, ItemInfoBase>();
    public Dictionary<int, ItemInfoWearPart> dic_WearPart = new Dictionary<int, ItemInfoWearPart>();
    SSParser parser = new SSParser();
    string strItemInfo;
    void Start()
    {
        
        strItemInfo = SSUtil.load("txt/iteminfo");

        //wearpart\
        ParseWearPart(strItemInfo, "wearpart");
    }
    #region wearpart

    public string GetItemInfoIcon(int _itemcode)
    {
        string _rtn = null;
        if (dic_Base.ContainsKey(_itemcode))
        {
            _rtn = dic_Base[_itemcode].icon;
        }
        return _rtn;
    }

    public Sprite GetItemInfoSpriteIcon(int _itemcode)
    { 
        return attlas.GetSprite(GetItemInfoIcon(_itemcode));
    }

    public ItemInfoBase GetItemInfoBase(int _itemcode)
    {
        ItemInfoBase _rtn = null;
        if (dic_Base.ContainsKey(_itemcode))
        {
            _rtn = dic_Base[_itemcode];
        }
        return _rtn;
    }

    public ItemInfoWearPart GetItemInfoWearPart(int _itemcode)
    {
        ItemInfoWearPart _rtn = null;
        if (dic_WearPart.ContainsKey(_itemcode))
        {
           _rtn =  dic_WearPart[_itemcode];
        }
        return _rtn;
    }
    void ParseWearPart(string _src, string _label)
    {
        //파트 이름으로 Parsing
        parser.parsing(_src, _label);

        ItemInfoWearPart _data;
        int _itemcode;
        while (parser.next())
        {
            _data = new ItemInfoWearPart();
            _itemcode = parser.getInt("itemcode");

            if (!dic_Base.ContainsKey(_itemcode))
            {
                _data.itemcode      = _itemcode;
                _data.category      = parser.getInt("category");
                _data.subcategory   = parser.getInt("subcategory");
                _data.equpslot      = parser.getInt("equpslot");
                _data.itemname      = parser.getString("itemname");
                _data.activate      = parser.getInt("activate");
                _data.toplist       = parser.getInt("toplist");
                _data.grade         = parser.getInt("grade");
                _data.discount      = parser.getInt("discount");
                _data.icon          = parser.getString("icon");
                _data.playerlv      = parser.getInt("playerlv");
                _data.multistate    = parser.getInt("multistate");
                _data.gamecost      = parser.getInt("gamecost");
                _data.cashcost      = parser.getInt("cashcost");
                _data.buyamount     = parser.getInt("buyamount");
                _data.sellcost      = parser.getInt("sellcost");
                _data.description   = parser.getString("description");

                _data.plusatt = parser.getInt("plusatt");
                _data.plusdef = parser.getInt("plusdef");
                _data.plushp = parser.getInt("plushp");

                //dic에 Add
                dic_Base.Add(_itemcode, _data);
                dic_WearPart.Add(_itemcode, _data);
            }
        }
    }
    #endregion
}
