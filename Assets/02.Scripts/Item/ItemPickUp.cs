using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public int itemcode;
    public int count = 1;
    public ItemData itemData;
    //public ItemData GetItemData()
    //{
    //    return itemData;
    //}

    private void Start()
    {
        InitData(itemcode, count);
    }

    public void ClearDestroy()
    {
        Destroy(gameObject);
    }

    public void InitData(int _itemcode = -1, int _count = 1)
    {
        if(_itemcode == -1)
        {
            _itemcode = itemcode;
            _count = count;
        }
        else
        {
            itemcode = _itemcode;
            count = _count;
        }    
        itemData = new ItemData(_itemcode, count);
    }

    public float TurnSpeed = 90f;
    public Vector3 axis = Vector3.up;
    private void Update()
    {
        transform.Rotate(axis * TurnSpeed * Time.deltaTime);
    }
}
