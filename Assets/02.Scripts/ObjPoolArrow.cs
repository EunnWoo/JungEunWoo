using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolArrow : MonoBehaviour
{
    [SerializeField]
    GameObject[] arrow;
    public GameObject arrowPrefab;

    [SerializeField]
    GameObject[] targetPool;

    private void Awake()
    {
        arrow = new GameObject[10];

        Generate();
    }

    void Generate()
    {
        GameObject ObjPoolManager = new GameObject("ObjPoolManager"); // ������Ʈ Ǯ ���� ������Ʈ ����

        for (int i =0; i< arrow.Length;i++)
        {
            arrow[i] = Instantiate(arrowPrefab, ObjPoolManager.transform);
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
                Debug.Log("ȭ�� ����");
                return targetPool[i];
            }
        }

        return null;
    }
}
