using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UI_RoomIn : UI_Popup
{
    enum Buttons
    {
        ExitButton,
        StartButton

    }
    enum Images
    {

        RoomListImage
    }
    enum Texts
    {
        RoomNameText
    }

    Button exitButton;
    Button startButton;

    Image roomListImage;

    Text roomName;

    UI_LoginCanvas loginCanvas;

 
    public override void Init()
    {
        base.Init();
        PhotonNetwork.AutomaticallySyncScene = true;
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));
        Bind<Button>(typeof(Buttons));
        roomName = GetText((int)Texts.RoomNameText);
        roomListImage = GetImage((int)Images.RoomListImage);
        exitButton = GetButton((int)Buttons.ExitButton);
        startButton = GetButton((int)Buttons.StartButton);

        roomName.text = PhotonNetwork.CurrentRoom.Name;
        startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);

        JoinRoomSet();

        loginCanvas = FindObjectOfType<UI_LoginCanvas>();

        #region buttonevent

        exitButton.gameObject.AddUIEvent(ExitRoom);
        startButton.gameObject.AddUIEvent(StartGame);
        #endregion

    }
    public override void OnMasterClientSwitched(Player newMasterClient)//������ ������ ������ �ٲ������
    {
        startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);//���常 ���ӽ��� ��ư ������ ����
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)//�ٸ� �÷��̾ �濡 ������ �۵�
    {
        Managers.Resource.Instantiate("PlayerListItem", roomListImage.transform).GetComponent<PlayerListItem>().SetUp(newPlayer);
        //instantiate�� prefab�� playerListContent��ġ�� ������ְ� �� �������� �̸� �޾Ƽ� ǥ��. 
    }
    public void  JoinRoomSet()
    {
        Player[] players = PhotonNetwork.PlayerList;
        for (int i = 0; i < players.Length; i++)
        {
            Managers.Resource.Instantiate("PlayerListItem", roomListImage.transform).GetComponent<PlayerListItem>().SetUp(players[i]);
        }
    }

    public void ExitRoom(PointerEventData data)
    {
        loginCanvas.BackGroundSetActive();
        PhotonNetwork.LeaveRoom();
        Managers.UI.ClosePopupUI(gameObject.GetComponent<UI_RoomIn>());


    }

    public void StartGame(PointerEventData data)
    {
        Managers.Scene.LoadScene(SceneState.Select);
    }


}
