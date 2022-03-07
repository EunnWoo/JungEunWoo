using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // ����Ƽ�� ���� ������Ʈ��
using Photon.Realtime; // ���� ���� ���� ���̺귯��
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UI_CreateRoomMenu : UI_Popup
{
    enum Buttons
    {
        ExitButton,
        CreateRoomButton

    }
    enum GameObjects
    {
        RoomNameInputField
    }


    Button exitButton;
    Button createRoomButton;

    InputField roomNameInput;

    UI_LoginCanvas loginCanvas;
    // ���� ����� ���ÿ� ������ ���� ���� �õ�
    public override void Init()
    {
        base.Init();
        loginCanvas = FindObjectOfType<UI_LoginCanvas>();

        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        exitButton = GetButton((int)Buttons.ExitButton);

        createRoomButton = GetButton((int)Buttons.CreateRoomButton);
        roomNameInput = Get<GameObject>((int)GameObjects.RoomNameInputField).GetComponent<InputField>();


        #region buttonevent
        createRoomButton.gameObject.AddUIEvent(CreateRoom); //�����
        exitButton.gameObject.AddUIEvent(ExitCreateRoomMenu);

        #endregion

    }

    public void CreateRoom(PointerEventData data)
    {
        if (string.IsNullOrEmpty(roomNameInput.text))
        {
            Debug.Log("�� �̸��� �Է����ּ���");
            return;
        }

        PhotonNetwork.CreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = 3 }); // �����
        Managers.UI.ClosePopupUI(gameObject.GetComponent<UI_CreateRoomMenu>());

    }


    public override void OnCreateRoomFailed(short returnCode, string message)//�� ����� ���н� �۵�
    {
        Debug.Log("����� ����");
    }

    public void ExitCreateRoomMenu(PointerEventData data)
    {
        loginCanvas.BackGroundSetActive();
        Managers.UI.ClosePopupUI(gameObject.GetComponent<UI_CreateRoomMenu>());
    }
}
