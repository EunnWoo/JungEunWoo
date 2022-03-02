//using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;




public class GameManager 
{
    public bool isOpenInventory = false; //�κ��丮 Ȱ��ȭ����
    public bool isOpenSystemMenu = false; //�ý��� �޴� Ȱ��ȭ ����
    public bool isOpenEquipment = false; //���â Ȱ��ȭ ����

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

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.U)) //u�� ������
        {
            Managers.UI.ShowPopupUI<UI_Equipment>();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            
            Managers.UI.ShowPopupUI<UI_Inventory>();
        }
    }

}
