using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XmlParserTest : MonoBehaviour
{
    SSParser parserItemInfo = new SSParser();
    string iteminfoTxt;
    void Start()
    {
        // xml > read > string
        iteminfoTxt = SSUtil.load("txt/iteminfo");
        Debug.Log(iteminfoTxt);

        //��Ʈ �̸����� Parsing
        parserItemInfo.parsing(iteminfoTxt, "piecebox");

        // Cursor���� ����Ÿ�� ����
        while(parserItemInfo.next())
        {
            int _itemcode = parserItemInfo.getInt("itemcode");
            Debug.Log(_itemcode);
        }
    }
}