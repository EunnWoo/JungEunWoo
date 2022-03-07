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
    public override void OnMasterClientSwitched(Player newMasterClient)//방장이 나가서 방장이 바뀌었을때
    {
        startButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);//방장만 게임시작 버튼 누르기 가능
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)//다른 플레이어가 방에 들어오면 작동
    {
        Managers.Resource.Instantiate("PlayerListItem", roomListImage.transform).GetComponent<PlayerListItem>().SetUp(newPlayer);
        //instantiate로 prefab을 playerListContent위치에 만들어주고 그 프리펩을 이름 받아서 표시. 
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
