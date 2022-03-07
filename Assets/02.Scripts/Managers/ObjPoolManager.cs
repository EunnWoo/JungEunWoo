using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolManager 
{
    [SerializeField]
    GameObject[] arrow;
    [SerializeField]
    GameObject[] fireBall;
    GameObject[] targetPool;
    GameObject[] bombSlimePool;

    Transform objPoolManager;





    public void Init()
    {
        Debug.Log("Ǯ ����");


        // ������Ʈ Ǯ ���� ������Ʈ ����
        objPoolManager = new GameObject { name = "ObjPoolManager" }.transform;
        Object.DontDestroyOnLoad(objPoolManager);
        //�� �ִ밳�� ����
        arrow = new GameObject[100];
        fireBall = new GameObject[10];
        bombSlimePool = new GameObject[4];

        for (int i =0; i< arrow.Length;i++)
        {
            arrow[i] = Managers.Resource.Instantiate("Arrow", objPoolManager);

            arrow[i].SetActive(false);
        }
        for (int i = 0; i < fireBall.Length; i++)
        {
            fireBall[i] = Managers.Resource.Instantiate("Magic fire", objPoolManager);
            fireBall[i].SetActive(false);
        }
        for (int i = 0; i < bombSlimePool.Length; i++)
        {
            bombSlimePool[i] = Managers.Resource.Instantiate("Boom_Slime_A", objPoolManager);
            bombSlimePool[i].SetActive(false);
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
            case "Boom_Slime_A":
                targetPool = bombSlimePool;
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
