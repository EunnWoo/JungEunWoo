using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // ����Ƽ�� ���� ������Ʈ��
using Photon.Realtime; // ���� ���� ���� ���̺귯��
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_FindRoomMenu : UI_Popup
{
    enum Buttons
    {
        ExitButton,
        RefreshButton

    }
    enum GameObjects
    {

        RoomListImage
    }
    
    Button exitButton;
    Button refreshButton;
    Image roomListImage;

    UI_LoginCanvas loginCanvas;
    public Transform roomListContent { get; private set; }

    public override void Init()
    {
        base.Init();
        Debug.Log("FindRoomMenu����");
        loginCanvas = FindObjectOfType<UI_LoginCanvas>();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        exitButton = GetButton((int)Buttons.ExitButton);
        refreshButton = GetButton((int)Buttons.RefreshButton);


        roomListImage = Get<GameObject>((int)GameObjects.RoomListImage).GetComponent<Image>();
        roomListContent = roomListImage.transform;

 
        #region buttonevent

        exitButton.gameObject.AddUIEvent(ExitFindRoomMenu);
        #endregion

        gameObject.SetActive(false);
    }

    public void JoinRoom(RoomInfo info) // �濡 ������������ �˾� �μ���
    {
        gameObject.SetActive(false);
        PhotonNetwork.JoinRoom(info.Name);//���� ��Ʈ��ũ�� JoinRoom��� �ش��̸��� ���� ������ �����Ѵ�. 
    }

    public void ExitFindRoomMenu(PointerEventData data)
    {
        loginCanvas.BackGroundSetActive();
        gameObject.SetActive(false);
    }
}
