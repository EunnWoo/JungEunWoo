using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // ����Ƽ�� ���� ������Ʈ��
using Photon.Realtime; // ���� ���� ���� ���̺귯��
using UnityEngine.UI;

public class UI_LoginCanvas : UI_Scene 
{
    enum Buttons
    {
        LoginButton,
        FindRoomButton,
        CreateRoomButton,
        ExitButton
    }
    enum Texts
    {
        IDText,
        connectionInfoText
    }
    enum GameObjects
    {
        RoomNameInputField,
        CreateFindRoomMenu
    }
    private string gameVersion = "1"; // ���� ����

    Text connectionInfoText; // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ
    Button loginButton; // �� ���� ��ư
    InputField roomName;
    // ���� ����� ���ÿ� ������ ���� ���� �õ�
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        connectionInfoText = GetText((int)Texts.connectionInfoText);
        loginButton = GetButton((int)Buttons.LoginButton);
        roomName = Get<GameObject>((int)GameObjects.RoomNameInputField).GetComponent<InputField>();
        

        //���ӿ� �ʿ��� ���� ����
        PhotonNetwork.GameVersion = gameVersion;
        //������ ������ ������ ���� ���� �õ�
        PhotonNetwork.ConnectUsingSettings();

        connectionInfoText.text = "������ ������ ������..";
        //�� ���� ��ư ��Ȱ��ȭ
        loginButton.interactable = false;

    }

    // ������ ���� ���� ������ �ڵ� ����
    public override void OnConnectedToMaster()
    {
        //���� ��ư Ȱ��ȭ
        loginButton.interactable = true;
        //���� ���� ǥ��
        connectionInfoText.text = "�¶��� : ������ ������ �����";
        

    }

    // ������ ���� ���� ���н� �ڵ� ����
    public override void OnDisconnected(DisconnectCause cause)
    {
        //��ư ��Ȱ��ȭ
        loginButton.interactable = false;
        //�������� ǥ��
        connectionInfoText.text = "�������� : ������ ������ ������� ����\n ���� ��õ� ��...";

        //������ ������ �õ�
        PhotonNetwork.ConnectUsingSettings();

    }

    //�� ������ �� ���� �õ�
    public void Connect()
    {
        //�ߺ� ���� ������ ���� ��ư ��Ȱ
        loginButton.interactable = false;

        //������ ������ ���� ���̶��
        if (PhotonNetwork.IsConnected)
        {
            //�� ���� ����
            connectionInfoText.text = "�뿡 ����...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            //������ ������ �������� �ƴ϶�� ������ ������ ���� �õ�
            connectionInfoText.text = "�������� : ������ ������ ������� ����\n ���� ��õ� ��...";
            //�����ӽõ�
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // (�� ���� ����)���� �� ������ ������ ��� �ڵ� ����
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //���� ���� ǥ��
        connectionInfoText.text = "�� ���� ����, ���ο� �� ����....";
        //�ִ� 4���� ���� ������ �� �� ����
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    // �뿡 ���� �Ϸ�� ��� �ڵ� ����
    public override void OnJoinedRoom()
    {
        //���� ���� ǥ��
        connectionInfoText.text = "�� ���� ����";
        // ��� �� �����ڰ� Main ���� �ε��ϰ���
        PhotonNetwork.LoadLevel("Main");

    }
    public void ClickLogin()
    {
        Get<GameObject>((int)GameObjects.CreateFindRoomMenu).SetActive(true);

    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomName.text)) return;

        PhotonNetwork.CreateRoom(roomName.text);

            
    }

    public void Open()
    {

    }
    public void Close()
    {

    }
}
