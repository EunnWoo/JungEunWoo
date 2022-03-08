//using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;



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

        GameObject go = PhotonNetwork.Instantiate(path, Vector3.zero, Quaternion.identity);   //��Ʈ��ũ�� �����ϴ� �������� PhotonNetwork.Instantiate �� ����
            //Managers.Resource.Instantiate(path, parent);
       
       _player = go;

        return go;
    }

    public void OnUpdate()
    {
        if (Input.GetKeyDown(KeyCode.U)) //u�� ������
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
