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

    public GameObject GetPlayer() { return _player; }


    public void Init()
    {
        
        

    }
    public GameObject Spawn(string path, Transform parent = null)
    {
       
        GameObject go = Managers.Resource.Instantiate(path, parent);
       
       _player = go;

        return go;
    }

  

}
