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
            Managers.UI.ui_Equipment.OnOffEquipment();
   
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            Managers.UI.ui_Inventory.OnOffInventory();

        }
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Managers.UI.ui_Quest.OpenQuest();

        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(Managers.UI.StatePopupUI())
            {
                Managers.UI.CloseAllPopupUI();
            }
            else if (!Managers.UI.StatePopupUI())
            {
                Managers.UI.ui_Menu.OpenMenu();
            }

        }
    }

}
