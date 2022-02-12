using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
    public static bool isOpenInventory = false; //�κ��丮 Ȱ��ȭ����
    public static bool isOpenSystemMenu = false; //�ý��� �޴� Ȱ��ȭ ����
    void Update()
    {
        //if(isOpenInventory || isOpenSystemMenu) //�κ��丮, �ý��۸޴� �� ����������
        //{
        //    Cursor.lockState = CursorLockMode.None; //���콺 Ŀ�� ������ ������
        //    Cursor.visible = true; //���콺 Ŀ���� ���̰���
        //}
        //else
        //{
        //    Cursor.lockState = CursorLockMode.Locked; //���콺 Ŀ����������Ŵ
        //    Cursor.visible = false; //���콺 Ŀ���� �Ⱥ��̰���
        //}
    }
    public static GameManager instance // ������Ƽ
    {
        get
        {
            // ���� �̱��� ������ ���� ������Ʈ�� �Ҵ���� �ʾҴٸ�
            if (m_instance == null)
            {
                // ������ GameManager ������Ʈ�� ã�� �Ҵ�
                m_instance = FindObjectOfType<GameManager>();
            }

            // �̱��� ������Ʈ�� ��ȯ
            return m_instance;
        }
    }
    private static GameManager m_instance; // �̱����� �Ҵ�� static ����
    public GameObject playerPrefab;// �÷��̾� ������

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) //�ֱ������� �ڵ� ����Ǵ� ����ȭ �޼ҵ�
    {
        if (stream.IsWriting) // ���� ������Ʈ��� ����κ��� �����
        {
            //stream.SendNext()
        }
        else
        {
            //����Ʈ��� �б� ReceiveNext()
        }
    }

    private void Awake()
    {
        //Cursor.lockState = CursorLockMode.Locked; //���콺�� ����� ����
        //Cursor.visible = false; //���콺 Ŀ���� ������ �ʰ���
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
        if (instance != this)
        {
            // �ڽ��� �ı�
            Destroy(gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {


        Vector3 randomSpawnPos = Random.insideUnitSphere * 5f; // ��ġ ��������
        randomSpawnPos.y = 2f;

        //��Ʈ��ũ���� ��� Ŭ���̾�Ʈ���� ���� ����
        //�ش� ���ӿ�����Ʈ�� �ֵ����� ���� �޼ҵ带 ���� ������ Ŭ���̾�Ʈ�� ����
        PhotonNetwork.Instantiate(playerPrefab.name, randomSpawnPos, Quaternion.identity);


    }

}
