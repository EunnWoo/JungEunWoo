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
        ExitButton

    }
    enum GameObjects
    {

        RoomListImage
    }
    
    Button exitButton;

    Image roomListImage;

    UI_LoginCanvas loginCanvas;
    Transform roomListContent;

    public override void Init()
    {
        base.Init();
        

        loginCanvas = FindObjectOfType<UI_LoginCanvas>();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        exitButton = GetButton((int)Buttons.ExitButton);

        //if (ui_RoomButton == null) return;

        roomListImage = Get<GameObject>((int)GameObjects.RoomListImage).GetComponent<Image>();
        roomListContent = roomListImage.transform;

 

        #region buttonevent

        exitButton.gameObject.AddUIEvent(ExitFindRoomMenu);

        #endregion

    }

    public void JoinRoom(RoomInfo info)
    {
        Managers.UI.ClosePopupUI(gameObject.GetComponent<UI_FindRoomMenu>());
        PhotonNetwork.JoinRoom(info.Name);//���� ��Ʈ��ũ�� JoinRoom��� �ش��̸��� ���� ������ �����Ѵ�. 
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)//������ �� ����Ʈ ���
    {
        foreach (Transform trans in roomListContent)//�����ϴ� ��� roomListContent
        {
            Destroy(trans.gameObject);//�븮��Ʈ ������Ʈ�� �ɶ����� �������
        }
        for (int i = 0; i < roomList.Count; i++)//�� ������ŭ �ݺ�
        {

            Managers.Resource.Instantiate("UI_RoomButton", roomListContent).GetComponent<UI_RoomButton>().SetUp(roomList[i]);
            //instantiate�� prefab�� roomListContent��ġ�� ������ְ� �� �������� i��° �븮��Ʈ�� �ȴ�. 
        }
    }

    

    public void ExitFindRoomMenu(PointerEventData data)
    {
        loginCanvas.BackGroundSetActive();
        Managers.UI.ClosePopupUI(gameObject.GetComponent<UI_FindRoomMenu>());
    }
}
