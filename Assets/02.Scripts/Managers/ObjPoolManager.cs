using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolManager : MonoBehaviour
{
    [SerializeField]
    GameObject[] arrow;
    [SerializeField]
    GameObject[] fireBall;
    GameObject[] targetPool;

    private void Start()
    {
        arrow = new GameObject[20];
        fireBall = new GameObject[5];
        
        Generate();
    }

    void Generate()
    {
        GameObject ObjPoolManager = new GameObject("ObjPoolManager"); // ������Ʈ Ǯ ���� ������Ʈ ����
        GameObject FireballGroup = new GameObject("FireballGroup");

        for (int i =0; i< arrow.Length;i++)
        {
            arrow[i] = Managers.Resource.Instantiate("Arrow", ObjPoolManager.transform);

            arrow[i].SetActive(false);
        }
        for (int i = 0; i < fireBall.Length; i++)
        {
            fireBall[i] = Managers.Resource.Instantiate("Magic fire");
            fireBall[i].SetActive(false);
        }
    }

    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "Arrow":
                targetPool = arrow;
                break;
            case "FireBall":
                targetPool = fireBall;
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
