//using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class GameManager 
{
    public bool isOpenInventory = false; //인벤토리 활성화여부
    public bool isOpenSystemMenu = false; //시스템 메뉴 활성화 여부
    public bool isOpenEquipment = false; //장비창 활성화 여부

    GameObject _player;
    public string _name { get; private set; }
    public GameObject GetPlayer() { return _player; }


    public void Init()
    {
        
        

    }
    public void SetName(string name) {  _name = name;  }
    
    public GameObject Spawn(string path, Transform parent = null)
    {
       
        GameObject go = Managers.Resource.Instantiate(path);
        
        _player = go;

        return go;
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.U)) //u를 누를시
        {
            UI_Equipment.ins.OpenEquipment();
            //UI_Equipment ui_equipment = GameObject.FindObjectOfType<UI_Equipment>();
            //ui_equipment.OpenEquipment();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            UI_Inventory ui_inventory = GameObject.FindObjectOfType<UI_Inventory>();
            ui_inventory.OpenInventory();
           
        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            UI_Quest ui_quest = GameObject.FindObjectOfType<UI_Quest>();
            ui_quest.OpenQuest();

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Managers.UI.CloseAllPopupUI();
            Managers.UI.isTalk(false);
        }
        
    }

}
