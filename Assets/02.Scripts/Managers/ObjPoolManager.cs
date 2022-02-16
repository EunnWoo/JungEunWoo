using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] arrow;

    private GameObject arrowPrefab;
   
    [SerializeField]
    GameObject[] targetPool;

    private void Start()
    {
        arrow = new GameObject[10];

        Generate();
    }

    void Generate()
    {
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
