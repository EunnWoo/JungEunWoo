using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map1Portal : MonoBehaviour
{
    public string nextMap = "Map1";    
    private void OnTriggerEnter(Collider collision)
    {
        //�÷��̾��� �±׸� ���� ���� ������Ʈ�� �ݶ��̴��� ������
        if (collision.gameObject.tag == "Player")
        {
            //�� �̵�
            SceneManager.LoadScene(nextMap);
        }
    }
}
