using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map1Portal : MonoBehaviour
{
        
    private void OnTriggerEnter(Collider collision)
{

    //�÷��̾��� �±׸� ���� ���� ������Ʈ�� �ݶ��̴��� ������
    if (collision.gameObject.tag == "Player")

    {

        //�� �̵�
        SceneManager.LoadScene("Map1");

    }

}
}
