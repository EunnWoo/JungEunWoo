//using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static bool isOpenInventory = false; //�κ��丮 Ȱ��ȭ����
    public static bool isOpenSystemMenu = false; //�ý��� �޴� Ȱ��ȭ ����
    void Update()
    {

    }
    //public static GameManager instance // ������Ƽ
    //{
    //    get
    //    {
            
    //        // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
    //        if (m_instance == null)
    //        {
    //            // ������ GameManager ������Ʈ�� ã�� �Ҵ�
    //            m_instance = FindObjectOfType<GameManager>();
    //        }

    //        // �̱��� ������Ʈ�� ��ȯ
    //        return m_instance;
    //    }
    //}
   // private static GameManager m_instance; // �̱����� �Ҵ�� static ����
  //  public GameObject playerPrefab;// �÷��̾� ������



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
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
       //if (instance != this)
       // {
       //     // �ڽ��� �ı�
       //     Destroy(gameObject);
       // }
       // DontDestroyOnLoad(this);
      
    }
    //private void Start()
    //{


    //    //Vector3 randomSpawnPos = Random.insideUnitSphere * 5f; // ��ġ ��������
    //    //randomSpawnPos.y = 2f;

    //    ////��Ʈ��ũ���� ��� Ŭ���̾�Ʈ���� ���� ����
    //    ////�ش� ���ӿ�����Ʈ�� �ֵ����� ���� �޼ҵ带 ���� ������ Ŭ���̾�Ʈ�� ����
    //    //PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);


    //}

}
