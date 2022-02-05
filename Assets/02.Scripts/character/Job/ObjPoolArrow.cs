using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPoolArrow : MonoBehaviour
{
    GameObject[] arrow;
    public GameObject arrowPrefab;


    GameObject[] targetPool;

    private void Awake()
    {
        arrow = new GameObject[10];

        Generate();
    }

    void Generate()
    {
        for(int i =0; i< arrow.Length;i++)
        {
            arrow[i] = Instantiate(arrowPrefab);
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
                targetPool[i].SetActive(true);
                return targetPool[i];
            }
        }

        return null;
    }
}
