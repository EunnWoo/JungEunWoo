using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime; // 포톤 서비스 관련 라이브러리
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
        Debug.Log("FindRoomMenu실행");
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

    public void JoinRoom(RoomInfo info) // 방에 접속했을때만 팝업 부수기
    {
        Managers.UI.ClosePopupUI(gameObject.GetComponent<UI_FindRoomMenu>());
        PhotonNetwork.JoinRoom(info.Name);//포톤 네트워크의 JoinRoom기능 해당이름을 가진 방으로 접속한다. 
    }

    public void ExitFindRoomMenu(PointerEventData data)
    {
        loginCanvas.BackGroundSetActive();
        gameObject.SetActive(false);
    }
}
