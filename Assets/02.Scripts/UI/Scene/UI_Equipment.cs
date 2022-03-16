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
