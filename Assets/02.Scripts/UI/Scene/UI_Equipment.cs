using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Equipment : UI_Scene
{
    //public static UI_Equipment ins;
    //private void Awake()
    //{
    //    ins = this;
    //}

    public static bool isOpenEquipment = false;
    public GameObject Eqbody;
    public override void Init()
    {


        Eqbody.SetActive(false);
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
