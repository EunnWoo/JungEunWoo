using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum eItemType
{
	Use,//�Һ�
	Equip,//���
	ETC,//��Ÿ
	Coin //��

}


public class ItemInfoClass 
{
    
}
[System.Serializable]

public class ItemData
{
	public int itemcode;
	public ItemInfoBase iteminfoBase;
	public int itemCount = 1;
	public int upgradeCount = 0;	
	
	public ItemData(int _itemcode, int _count = 1)
    {
		itemcode = _itemcode;
		itemCount = _count;
		upgradeCount = 0;

		iteminfoBase = ItemInfo.ins.GetItemInfoBase(_itemcode);
	}

	public eEquipmentSlot equipmentSlot
    {
		get
		{
			eEquipmentSlot _slot = eEquipmentSlot.NotSlot;
			switch (iteminfoBase.subcategory) //iteminfoBase�� subcategory�־��ָ�
			{
				case 0:
					_slot = eEquipmentSlot.Head;
					break;

				case 1:
					_slot = eEquipmentSlot.Armor;
					break;

				case 2:
					_slot = eEquipmentSlot.Weapon;
					break;

				case 3:
					_slot = eEquipmentSlot.Boots;
					break;
			}
			return _slot;
		}
	}

	//public bool equipmentStatus;

	public string skin { 
		get {
			//skin wear part -> �⺻Ŭ���� -> wear part Ŭ���� ��ȯ
			ItemInfoWearPart _item = (ItemInfoWearPart)iteminfoBase;
			if (_item != null) return _item.skin;
			else return "NOSKIN";
		} 
	}

	public string skin2
	{
		get
		{
			//skin wear part -> �⺻Ŭ���� -> wear part Ŭ���� ��ȯ
			ItemInfoWearPart _item = (ItemInfoWearPart)iteminfoBase;
			if (_item != null) return _item.skin2;
			else return "NOSKIN";
		}
	}

	public string itemName 
	{
		get 
		{

			return iteminfoBase != null ? iteminfoBase.itemname : "";
		}
	}

	public eItemType itemType
	{
		get
		{
			return iteminfoBase.itemType;
		}
	}
	//public string GetItemName()
	//{
	//	return iteminfoBase.itemname;
	//}

	//public string GetIcon()
	//{
	//	return iteminfoBase.icon;
	//}
	public string icon { get { return iteminfoBase.icon; } }
	public Sprite iconSprite { get { return ItemInfo.ins.GetSprite(iteminfoBase.icon); } }
	// public string itemIcon {	get	{return iteminfoBase.icon;}	}
}

[System.Serializable]
public class ItemInfoBase
{
	public int itemcode;
	private int category_;
	public int category 
	{
        get { return category_; }
        set
        {
			category_ = value;
			if(value == 1)
            {
				itemType = eItemType.Equip;
            }
			else if (value == 2)
			{
				itemType = eItemType.Use;
			}
			else if (value == 3)
			{
				itemType = eItemType.ETC;
			}
			else if (value == 4)
			{
				itemType = eItemType.Coin;
			}
		}
	}
	public int subcategory;
	public int equpslot;
	public string itemname;
	public int activate;
	public int toplist;
	public int grade;
	public int discount;
	public string icon;
	public int playerlv;
	public int multistate;
	public int gamecost;
	public int cashcost;
	public int buyamount;
	public int sellcost;
	public string description;

	public eItemType itemType;
}

[System.Serializable]
public class ItemInfoWearPart : ItemInfoBase
{
	public int plusatt;
	public int plusdef;
	public int plushp;
	public string skin;
	public string skin2;
}


[System.Serializable]
public class ItemInfoUsepart : ItemInfoBase
{
	public float hp;
	public float mp;
}

[System.Serializable]
public class ItemInfoETCpart : ItemInfoBase
{
	public int hp;
	public int mp;
}

[System.Serializable]
public class ItemInfoCoinpart : ItemInfoBase
{
	//
}