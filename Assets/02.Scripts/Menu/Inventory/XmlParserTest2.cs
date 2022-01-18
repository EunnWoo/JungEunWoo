using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class XmlParserTest2 : MonoBehaviour
{
    
    public List<ItemData> list = new List<ItemData>();
    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            ItemData _itemData = new ItemData(20001);
            list.Add(_itemData);
        }
        else if(Input.GetKeyDown(KeyCode.Alpha2))
         {
            ItemData _itemData = new ItemData(20002);
            list.Add(_itemData);
        }
    }
}
