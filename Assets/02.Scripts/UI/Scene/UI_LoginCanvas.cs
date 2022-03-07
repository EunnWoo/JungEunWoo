using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime; // 포톤 서비스 관련 라이브러리
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
    private string gameVersion = "1"; // 게임 버전

    Text connectionInfoText; // 네트워크 정보를 표시할 텍스트

    Button loginButton; // 룸 접속 버튼
    Button createRoomMenuButton;
    Button exitButton;
    Button findRoomMenuButton;

    InputField idInput;
    GameObject backGround;
    // 게임 실행과 동시에 마스터 서버 접속 시도
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


        //접속에 필요한 정보 설정
        PhotonNetwork.GameVersion = gameVersion;
        //설정한 정보로 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();

        connectionInfoText.text = "마스터 서버에 접속중..";

        //버튼 비활성화
        createRoomMenuButton.interactable = false;
        findRoomMenuButton.interactable = false;
        loginButton.interactable = false;


        #region buttonevent
        loginButton.gameObject.AddUIEvent(ClickLogin); // 로그인 클릭시
        createRoomMenuButton.gameObject.AddUIEvent(CreateRoomMenu); //방생성메뉴버튼 클릭시
        findRoomMenuButton.gameObject.AddUIEvent(FindRoomMenu);
        exitButton.gameObject.AddUIEvent(Exit);



        #endregion

    }

    // 마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster()
    {
        //접속 버튼 활성화
        loginButton.interactable = true;
        //접속 정보 표시
        connectionInfoText.text = "온라인 : 마스터 서버와 연결됨";
        PhotonNetwork.JoinLobby();


    }

    // 마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause)
    {
        //버튼 비활성화
        loginButton.interactable = false;
        //접속정보 표시
        connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n 접속 재시도 중...";

        //서버로 재접속 시도
        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnJoinedLobby()//로비에 연결시 작동
    {
        Debug.Log("Joined Lobby");
        PhotonNetwork.NickName = idInput.text;
        //들어온사람 이름 랜덤으로 숫자붙여서 정해주기
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("JoinedRoom");
        // 룸 참가자가 Select 씬을 로드하게함
        
        Managers.UI.ShowPopupUI<UI_RoomIn>();
    }

    // 룸에 참가 완료된 경우 자동 실행
    public void ClickLogin(PointerEventData data)
    {
        if (string.IsNullOrEmpty(idInput.text))
        {
            //아이디가 비어있을때 
            Debug.Log("이름을 입력해주세요");
            return;
        }

        loginButton.interactable = false; //로그인버튼 비활성화

        createRoomMenuButton.interactable = true;
        findRoomMenuButton.interactable = true;
        PhotonNetwork.JoinLobby(); // 로비 참여

    }
    public void CreateRoomMenu(PointerEventData data)
    {
        BackGroundSetActive();
        Managers.UI.ShowPopupUI<UI_CreateRoomMenu>();

    }
    
    public void FindRoomMenu(PointerEventData data)
    {
        BackGroundSetActive();
        Managers.UI.ShowPopupUI<UI_FindRoomMenu>();
    }




    public void BackGroundSetActive()
    {     
        if(!backGround.activeSelf)
        {
            backGround.SetActive(true); // 메인이 꺼져있을때 호출되면 true해주기
        }
        else
        {
            backGround.SetActive(false); // 메인이 켜져있을때 호출되면 false해주기
        }
    }
   

    public void Exit(PointerEventData data)
    {
        //게임종료
        Application.Quit();
    }


}
