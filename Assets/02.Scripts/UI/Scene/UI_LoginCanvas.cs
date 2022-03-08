using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // ����Ƽ�� ���� ������Ʈ��
using Photon.Realtime; // ���� ���� ���� ���̺귯��
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_LoginCanvas : UI_Scene 
{
    enum Buttons
    {
        LoginButton,
        FindRoomMenuButton,
        CreateRoomMenuButton,
        ExitButton,

    }
    enum Texts
    {
        connectionInfoText
    }
    enum GameObjects
    {
        IDInputField,
        BackGround
    }
    private string gameVersion = "1"; // ���� ����

    Text connectionInfoText; // ��Ʈ��ũ ������ ǥ���� �ؽ�Ʈ

    Button loginButton; // �� ���� ��ư
    Button createRoomMenuButton;
    Button exitButton;
    Button findRoomMenuButton;

    InputField idInput;
    GameObject backGround;
    [SerializeField]
    GameObject findMenu;

    // ���� ����� ���ÿ� ������ ���� ���� �õ�
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));

        connectionInfoText = GetText((int)Texts.connectionInfoText);

        loginButton = GetButton((int)Buttons.LoginButton);
        createRoomMenuButton = GetButton((int)Buttons.CreateRoomMenuButton);
        findRoomMenuButton = GetButton((int)Buttons.FindRoomMenuButton);
        exitButton = GetButton((int)Buttons.ExitButton);

        idInput = Get<GameObject>((int)GameObjects.IDInputField).GetComponent<InputField>();
        backGround = Get<GameObject>((int)GameObjects.BackGround);


        //���ӿ� �ʿ��� ���� ����
        PhotonNetwork.GameVersion = gameVersion;
        //������ ������ ������ ���� ���� �õ�
        PhotonNetwork.ConnectUsingSettings();

        connectionInfoText.text = "������ ������ ������..";

        //��ư ��Ȱ��ȭ
        createRoomMenuButton.interactable = false;
        findRoomMenuButton.interactable = false;
        loginButton.interactable = false;




        #region buttonevent
        loginButton.gameObject.AddUIEvent(ClickLogin); // �α��� Ŭ����
        createRoomMenuButton.gameObject.AddUIEvent(CreateRoomMenu); //������޴���ư Ŭ����
        findRoomMenuButton.gameObject.AddUIEvent(FindRoomMenu);
        exitButton.gameObject.AddUIEvent(Exit);



        #endregion


        findMenu = Managers.UI.ShowPopupUI<UI_FindRoomMenu>().gameObject; // �渮��Ʈ ������Ʈ�� ���� �˾� ������ ��Ƽ�� false
        
    }

    // ������ ���� ���� ������ �ڵ� ����
    public override void OnConnectedToMaster()
    {
        //���� ��ư Ȱ��ȭ
        loginButton.interactable = true;
        //���� ���� ǥ��
        connectionInfoText.text = "�¶��� : ������ ������ �����";
        PhotonNetwork.JoinLobby();

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

    public override void OnJoinedLobby()//�κ� ����� �۵�
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = idInput.text;
        //���»�� �̸� �������� ���ںٿ��� �����ֱ�
    }

    // �뿡 ���� �Ϸ�� ��� �ڵ� ����
    public override void OnJoinedRoom()
    {
        Debug.Log("JoinedRoom");
        // �� �����ڰ� Select ���� �ε��ϰ���
        
        Managers.UI.ShowPopupUI<UI_RoomIn>();
    }


    public void ClickLogin(PointerEventData data)
    {
        if (string.IsNullOrEmpty(idInput.text))
        {
            //���̵� ��������� 
            Debug.Log("�̸��� �Է����ּ���");
            return;
        }

        loginButton.interactable = false; //�α��ι�ư ��Ȱ��ȭ

        createRoomMenuButton.interactable = true;
        findRoomMenuButton.interactable = true;
        PhotonNetwork.JoinLobby(); // �κ� ����

    }
    public void CreateRoomMenu(PointerEventData data)
    {
        BackGroundSetActive();
        Managers.UI.ShowPopupUI<UI_CreateRoomMenu>();

    }
    
    public void FindRoomMenu(PointerEventData data)
    {
        BackGroundSetActive();
        findMenu.SetActive(true);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)//������ �� ����Ʈ ���
    {
        Debug.Log(findMenu.GetComponent<UI_FindRoomMenu>().roomListContent);
        foreach (Transform trans in findMenu.GetComponent<UI_FindRoomMenu>().roomListContent)//�����ϴ� ��� roomListContent
        {
            Destroy(trans.gameObject);//�븮��Ʈ ������Ʈ�� �ɶ����� �������
        }
        for (int i = 0; i < roomList.Count; i++)//�� ������ŭ �ݺ�
        {

            Managers.Resource.Instantiate("UI_RoomButton", findMenu.GetComponent<UI_FindRoomMenu>().roomListContent).GetComponent<UI_RoomButton>().SetUp(roomList[i]);
            //instantiate�� prefab�� roomListContent��ġ�� ������ְ� �� �������� i��° �븮��Ʈ�� �ȴ�. 
        }
    }


    public void BackGroundSetActive()
    {     
        if(!backGround.activeSelf)
        {
            backGround.SetActive(true); // ������ ���������� ȣ��Ǹ� true���ֱ�
        }
        else
        {
            backGround.SetActive(false); // ������ ���������� ȣ��Ǹ� false���ֱ�
        }
    }
   

    public void Exit(PointerEventData data)
    {
        //��������
        Application.Quit();
    }


}
