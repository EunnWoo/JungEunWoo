using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolManager : MonoBehaviour
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
        GameObject ObjPoolManager = new GameObject("ObjPoolManager"); // 오브젝트 풀 담을 오브젝트 생성

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
            if(!targetPool[i].activeSelf) // 비활성화라면
            {
               // targetPool[i].SetActive(true);
                Debug.Log("화살 리턴");
                return targetPool[i];
            }
        }

        return null;
    }
}
