using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Equipment : MonoBehaviour
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
        if (Input.GetKeyDown(KeyCode.U)) //u�� ������
        {
            OpenEquipment();
        }
    }

    
    
    public void Invoke_Close()
    {
        Eqbody.SetActive(false); //���â�� ����
    }

    public void OpenEquipment()
    {
        if (!Eqbody.activeSelf)//���â�� ����������
        {
            GameManager.isOpenEquipment = true;  //���â�� Ų��
            Eqbody.SetActive(true);
        }
        
        else
        {
            GameManager.isOpenEquipment = false;
            Eqbody.SetActive(false);
        }
    }
}
