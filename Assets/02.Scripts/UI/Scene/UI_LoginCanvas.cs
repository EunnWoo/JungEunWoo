using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime; // 포톤 서비스 관련 라이브러리
using UnityEngine.UI;

public class UI_LoginCanvas : UI_Scene 
{
    enum Buttons
    {
        LoginButton,
        FindRoomButton,
        CreateRoomButton,
        ExitButton
    }
    enum Texts
    {
        IDText,
        connectionInfoText
    }
    enum GameObjects
    {
        RoomNameInputField,
        CreateFindRoomMenu
    }
    private string gameVersion = "1"; // 게임 버전

    Text connectionInfoText; // 네트워크 정보를 표시할 텍스트
    Button loginButton; // 룸 접속 버튼
    InputField roomName;
    // 게임 실행과 동시에 마스터 서버 접속 시도
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        connectionInfoText = GetText((int)Texts.connectionInfoText);
        loginButton = GetButton((int)Buttons.LoginButton);
        roomName = Get<GameObject>((int)GameObjects.RoomNameInputField).GetComponent<InputField>();
        

        //접속에 필요한 정보 설정
        PhotonNetwork.GameVersion = gameVersion;
        //설정한 정보로 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();

        connectionInfoText.text = "마스터 서버에 접속중..";
        //룸 접속 버튼 비활성화
        loginButton.interactable = false;

    }

    // 마스터 서버 접속 성공시 자동 실행
    public override void OnConnectedToMaster()
    {
        //접속 버튼 활성화
        loginButton.interactable = true;
        //접속 정보 표시
        connectionInfoText.text = "온라인 : 마스터 서버와 연결됨";
        

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

    //빈 무작위 룸 접속 시도
    public void Connect()
    {
        //중복 접속 방지를 위한 버튼 비활
        loginButton.interactable = false;

        //마스터 서버에 접속 중이라면
        if (PhotonNetwork.IsConnected)
        {
            //룸 접속 실행
            connectionInfoText.text = "룸에 접속...";
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            //마스터 서버에 접속중이 아니라면 마스터 서버에 접속 시도
            connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n 접속 재시도 중...";
            //재접속시도
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    // (빈 방이 없어)랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        //접속 상태 표시
        connectionInfoText.text = "빈 방이 없음, 새로운 방 생성....";
        //최대 4명을 수용 가능한 빈 방 생성
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 4 });
    }

    // 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom()
    {
        //접속 상태 표시
        connectionInfoText.text = "방 참가 성공";
        // 모든 룸 참가자가 Main 씬을 로드하게함
        PhotonNetwork.LoadLevel("Main");

    }
    public void ClickLogin()
    {
        Get<GameObject>((int)GameObjects.CreateFindRoomMenu).SetActive(true);

    }
    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomName.text)) return;

        PhotonNetwork.CreateRoom(roomName.text);

            
    }

    public void Open()
    {

    }
    public void Close()
    {

    }
}
