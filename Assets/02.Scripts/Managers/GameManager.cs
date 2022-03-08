//using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;



public class GameManager 
{
    public bool isOpenInventory = false; //인벤토리 활성화여부
    public bool isOpenSystemMenu = false; //시스템 메뉴 활성화 여부
    public bool isOpenEquipment = false; //장비창 활성화 여부

    GameObject _player;

    public GameObject GetPlayer() { return _player; }


    public void Init()
    {
        
        

    }
    public GameObject Spawn(string path, Transform parent = null)
    {

        GameObject go = PhotonNetwork.Instantiate(path, Vector3.zero, Quaternion.identity);   //네트워크로 공유하는 프리팹은 PhotonNetwork.Instantiate 로 생성
            //Managers.Resource.Instantiate(path, parent);
       
       _player = go;

        return go;
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.U)) //u를 누를시
        {
            UI_Equipment ui_equipment = GameObject.FindObjectOfType<UI_Equipment>();
            if (ui_equipment == null)
            {
                Managers.UI.ShowSceneUI<UI_Equipment>();
            }
            else
            {
                ui_equipment.OpenEquipment();
            }
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            UI_Inventory ui_inventory = GameObject.FindObjectOfType<UI_Inventory>();
            if (ui_inventory == null)
            {
                Managers.UI.ShowSceneUI<UI_Inventory>();
            }
            else
            {
                ui_inventory.OpenInventory();
            }

        }
    }

}
