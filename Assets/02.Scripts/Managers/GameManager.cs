using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class GameManager 
{

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
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            UI_Inventory.ins.OpenInventory();
           
        }
        if(Input.GetKeyDown(KeyCode.Q))
        { 
            UI_Quest.ins.OpenQuest();

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Managers.UI.StatePopupUI())
            {
                Managers.UI.CloseAllPopupUI();
            }
            else if (!Managers.UI.StatePopupUI())
            {
                UI_Menu.ins.OpenMenu();
            }
            

        }
    }

}
