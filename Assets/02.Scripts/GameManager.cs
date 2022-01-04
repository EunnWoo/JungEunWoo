using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviourPunCallbacks, IPunObservable
{
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
        // ���� �̱��� ������Ʈ�� �� �ٸ� GameManager ������Ʈ�� �ִٸ�
        if (instance != this)
        {
            // �ڽ��� �ı�
            Destroy(gameObject);
        }
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
