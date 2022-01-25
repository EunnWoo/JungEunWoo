using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Map2Portal : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {

        //플레이어라는 태그를 가진 게임 오브젝트가 콜라이더에 닿으면
        if (collision.gameObject.tag == "Player")

        {

            //씬 이동
            SceneManager.LoadScene("Map2");

        }
    }
}