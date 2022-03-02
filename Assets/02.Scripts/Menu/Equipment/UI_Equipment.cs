using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Equipment : MonoBehaviour
{
    public static UI_Equipment ins;
    private void Awake()
    {
        ins = this;
    }

    public static bool isOpenEquipment = false;
    public GameObject Eqbody; 
    void Start()
    {
        
    }

    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)) //u를 누를시
        {
            OpenEquipment();
        }
    }

    
    
    public void Invoke_Close()
    {
        Eqbody.SetActive(false); //장비창을 끈다
    }

    public void OpenEquipment()
    {
        if (!Eqbody.activeSelf)//장비창이 꺼져있을시
        {
            Managers.Game.isOpenEquipment = true;  //장비창을 킨다
            Eqbody.SetActive(true);
        }
        
        else
        {
            Managers.Game.isOpenEquipment = false;
            Eqbody.SetActive(false);
        }
    }
}
