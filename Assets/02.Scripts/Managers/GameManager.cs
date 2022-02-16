//using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static bool isOpenInventory = false; //인벤토리 활성화여부
    public static bool isOpenSystemMenu = false; //시스템 메뉴 활성화 여부
    void Update()
    {

    }
    //public static GameManager instance // 프로퍼티
    //{
    //    get
    //    {
            
    //        // 만약 싱글톤 변수에 아직 오브젝트가 할당되지 않았다면
    //        if (m_instance == null)
    //        {
    //            // 씬에서 GameManager 오브젝트를 찾아 할당
    //            m_instance = FindObjectOfType<GameManager>();
    //        }

    //        // 싱글톤 오브젝트를 반환
    //        return m_instance;
    //    }
    //}
   // private static GameManager m_instance; // 싱글톤이 할당될 static 변수
  //  public GameObject playerPrefab;// 플레이어 프리팹



    private void Start()
    {
       //if(instance ==null)
       // {
       //     GameObject gameManager = GameObject.Find("GameManager");
       //     if(gameManager==null)
       //     {
       //         Managers.Resource.Instantiate("GameManager");
       //     }
       // }
        // 씬에 싱글톤 오브젝트가 된 다른 GameManager 오브젝트가 있다면
       //if (instance != this)
       // {
       //     // 자신을 파괴
       //     Destroy(gameObject);
       // }
       // DontDestroyOnLoad(this);
      
    }
    //private void Start()
    //{


    //    //Vector3 randomSpawnPos = Random.insideUnitSphere * 5f; // 위치 랜덤설정
    //    //randomSpawnPos.y = 2f;

    //    ////네트워크상의 모든 클라이언트에서 생성 실행
    //    ////해당 게임오브젝트의 주도권은 생성 메소드를 직접 실행한 클라이언트에 있음
    //    //PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);


    //}

}
