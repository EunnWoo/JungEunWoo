using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolManager
{
    GameObject[] arrow;
    GameObject[] targetPool;

    private void Awake()
    {
       

      //  Generate();
    }

    public void Generate()
    {
        arrow = new GameObject[10];
        GameObject ObjPoolManager = new GameObject("ObjPoolManager"); // ������Ʈ Ǯ ���� ������Ʈ ����

        for (int i =0; i< arrow.Length;i++)
        {
            arrow[i] = Managers.Resource.Instantiate("Arrow", ObjPoolManager.transform);

            arrow[i].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "Arrow":
                targetPool = arrow;
                break;
                
        }
        for(int i =0; i< targetPool.Length;i++)
        {
            if(!targetPool[i].activeSelf) // ��Ȱ��ȭ���
            {
               // targetPool[i].SetActive(true);
                
                return targetPool[i];
            }
        }

        return null;
    }
}
