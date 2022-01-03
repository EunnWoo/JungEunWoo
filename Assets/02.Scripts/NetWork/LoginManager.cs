using Photon.Pun; //����Ƽ�� ���� ������Ʈ
using Photon.Realtime; // ���漭�� ���� ���̺귯��
using UnityEngine;
using UnityEngine.UI;
public class LoginManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // ���ӹ���
    public InputField userId;
    public Text connectionInfoText; // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ
    public Button joinButton; // �� ���� ��ư
    private void Awake() // ���� ����� ���ÿ� ������ ���� ���� �õ�
    {
        PhotonNetwork.GameVersion = gameVersion; // ���ӿ� �ʿ��� ����(���� ����) ����
        PhotonNetwork.ConnectUsingSettings(); // ������ ������ ������ ���� ���� �õ�

        joinButton.interactable = false; // ���ӹ�ư ��Ȱ
        connectionInfoText.text = "������ ������ ���� ��...";
    }
    string GetUserId()
    {                   //���̵� ���� �ϱ����� ���� Ű�� ����
        string userId = PlayerPrefs.GetString("USER_ID");
        if (string.IsNullOrEmpty(userId))
        {            //���̵� �Է� ���� ������ �ڵ����� �����ϰ� ���̵� �ο� 
            userId = "GUEST_" + Random.Range(0, 999).ToString("000");
        }
        return userId;
    }
    public override void OnConnectedToMaster() // ������ ���� ���� ������ �ڵ� ����
    {
        joinButton.interactable = true; // ���ӹ�ư Ȱ��ȭ
        connectionInfoText.text = "�¶��� : ������ ������ �����";
    }

    public override void OnDisconnected(DisconnectCause cause) // ������ ���� ���н� �ڵ�����
    {
        joinButton.interactable = false;
        connectionInfoText.text = "�������� : ������ ������ ������� ����\n ���� ��õ� ��....";

        PhotonNetwork.ConnectUsingSettings(); // ������ ������ ������ �õ�
    }
    public void OnConnect() // �� ���� �õ�
    {
        joinButton.interactable = false; // �ߺ����� �õ��� �������� ��ư ��Ȱ

        if(PhotonNetwork.IsConnected) // ������ ���� ���� ���̶��
        {
            connectionInfoText.text = " �뿡 ����..."; // ������ ����
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "�������� : ������ ������ ������� ����\n ���� ��õ� ��....";  // ������ �õ�
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message) //����� ���� ���� �� ������ ������ ��� �ڵ� ����
    {
        connectionInfoText.text = "�� ���� ����, ���ο� �� ����....";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 3 }); // �ִ� 3�ι� ����
    }

    public override void OnJoinedRoom() // �뿡 �� �Ϸ�� ��� �ڵ�����
    {
        connectionInfoText.text = "�� ���� ����";
        PhotonNetwork.LoadLevel("Town");

        PhotonNetwork.LocalPlayer.NickName = userId.text;//���� �÷��̾� �̸��� ���� 
        PlayerPrefs.SetString("USER_ID", userId.text); //�÷��̾� �̸� ���� 


    }
}
