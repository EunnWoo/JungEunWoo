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
        PhotonNetwork.JoinRoom(info.Name);//포톤 네트워크의 JoinRoom기능 해당이름을 가진 방으로 접속한다. 
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)//포톤의 룸 리스트 기능
    {
        foreach (Transform trans in roomListContent)//존재하는 모든 roomListContent
        {
            Destroy(trans.gameObject);//룸리스트 업데이트가 될때마다 싹지우기
        }
        for (int i = 0; i < roomList.Count; i++)//방 개수만큼 반복
        {

            Managers.Resource.Instantiate("UI_RoomButton", roomListContent).GetComponent<UI_RoomButton>().SetUp(roomList[i]);
            //instantiate로 prefab을 roomListContent위치에 만들어주고 그 프리펩은 i번째 룸리스트가 된다. 
        }
    }

    

    public void ExitFindRoomMenu(PointerEventData data)
    {
        loginCanvas.BackGroundSetActive();
        Managers.UI.ClosePopupUI(gameObject.GetComponent<UI_FindRoomMenu>());
    }
}
