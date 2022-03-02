using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Equipment : UI_Popup
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
    
    }


    public void OpenEquipment()
    {
        if (!Eqbody.activeSelf)//���â�� ����������
        {
            Managers.Game.isOpenEquipment = true;  //���â�� Ų��
            Eqbody.SetActive(true);
        }
        
        else
        {
            Managers.Game.isOpenEquipment = false;
            Eqbody.SetActive(false);
        }
    }
}
