using Photon.Pun; //유니티용 포톤 컴포넌트
using Photon.Realtime; // 포톤서비스 관련 라이브러리
using UnityEngine;
using UnityEngine.UI;
public class LoginManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // 게임버전
    public InputField userId;
    public Text connectionInfoText; // 네트워크 정보를 표시할 텍스트
    public Button joinButton; // 룸 접속 버튼
    private void Awake() // 게임 실행과 동시에 마스터 서버 접속 시도
    {
        PhotonNetwork.GameVersion = gameVersion; // 접속에 필요한 정보(게임 버전) 설정
        PhotonNetwork.ConnectUsingSettings(); // 설정한 정보로 마스터 서버 접속 시도

        joinButton.interactable = false; // 접속버튼 비활
        connectionInfoText.text = "마스터 서버에 접속 중...";
    }
    string GetUserId()
    {                   //아이디를 저장 하기위한 예약 키값 설정
        string userId = PlayerPrefs.GetString("USER_ID");
        if (string.IsNullOrEmpty(userId))
        {            //아이디를 입력 하지 않으면 자동으로 랜덤하게 아이디를 부여 
            userId = "GUEST_" + Random.Range(0, 999).ToString("000");
        }
        return userId;
    }
    public override void OnConnectedToMaster() // 마스터 서버 접속 성공시 자동 실행
    {
        joinButton.interactable = true; // 접속버튼 활성화
        connectionInfoText.text = "온라인 : 마스터 서버와 연결됨";
    }

    public override void OnDisconnected(DisconnectCause cause) // 마스터 접속 실패시 자동실행
    {
        joinButton.interactable = false;
        connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n 접속 재시도 중....";

        PhotonNetwork.ConnectUsingSettings(); // 마스터 서버로 재접속 시도
    }
    public void OnConnect() // 룸 접속 시도
    {
        joinButton.interactable = false; // 중복접속 시도를 막기위한 버튼 비활

        if(PhotonNetwork.IsConnected) // 마스터 서버 접속 중이라면
        {
            connectionInfoText.text = " 룸에 접속..."; // 룸접속 실행
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            connectionInfoText.text = "오프라인 : 마스터 서버와 연결되지 않음\n 접속 재시도 중....";  // 재접속 시도
            PhotonNetwork.ConnectUsingSettings();
        }
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message) //빈방이 없어 랜덤 룸 참가에 실패한 경우 자동 실행
    {
        connectionInfoText.text = "빈 방이 없음, 새로운 방 생성....";
        PhotonNetwork.CreateRoom(null, new RoomOptions { MaxPlayers = 3 }); // 최대 3인방 생성
    }

    public override void OnJoinedRoom() // 룸에 가 완료된 경우 자동실행
    {
        connectionInfoText.text = "방 참가 성공";
        PhotonNetwork.LoadLevel("Lobby");

        PhotonNetwork.LocalPlayer.NickName = userId.text;//로컬 플레이어 이름을 설정 
        PlayerPrefs.SetString("USER_ID", userId.text); //플레이어 이름 저장 


    }
}
