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

    Transform objPoolManager;





    public void Init()
    {
        Debug.Log("풀 생성");


        // 오브젝트 풀 담을 오브젝트 생성
        objPoolManager = new GameObject { name = "ObjPoolManager" }.transform;
        Object.DontDestroyOnLoad(objPoolManager);
        //각 최대개수 설정
        arrow = new GameObject[100];
        fireBall = new GameObject[10];

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
            if(!targetPool[i].activeSelf) // 비활성화라면
            {
               // targetPool[i].SetActive(true);
                
                return targetPool[i];
            }
        }

        return null;
    }
}
