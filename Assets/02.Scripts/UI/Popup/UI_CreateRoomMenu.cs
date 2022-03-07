using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime; // 포톤 서비스 관련 라이브러리
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
    // 게임 실행과 동시에 마스터 서버 접속 시도
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
        createRoomButton.gameObject.AddUIEvent(CreateRoom); //방생성
        exitButton.gameObject.AddUIEvent(ExitCreateRoomMenu);

        #endregion

    }

    public void CreateRoom(PointerEventData data)
    {
        if (string.IsNullOrEmpty(roomNameInput.text))
        {
            Debug.Log("방 이름을 입력해주세요");
            return;
        }

        PhotonNetwork.CreateRoom(roomNameInput.text, new RoomOptions { MaxPlayers = 3 }); // 방생성
        Managers.UI.ClosePopupUI(gameObject.GetComponent<UI_CreateRoomMenu>());

    }


    public override void OnCreateRoomFailed(short returnCode, string message)//방 만들기 실패시 작동
    {
        Debug.Log("방생성 실패");
    }

    public void ExitCreateRoomMenu(PointerEventData data)
    {
        loginCanvas.BackGroundSetActive();
        Managers.UI.ClosePopupUI(gameObject.GetComponent<UI_CreateRoomMenu>());
    }
}
