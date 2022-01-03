using Photon.Pun; //유니티용 포톤 컴포넌트
using Photon.Realtime; // 포톤서비스 관련 라이브러리
using UnityEngine;
using UnityEngine.UI;
public class LoginManager : MonoBehaviourPunCallbacks
{
    private string gameVersion = "1"; // 게임버전

    public Text connectionInfoText; // 네트워크 정보를 표시할 텍스트
    public Button joinButton; // 룸 접속 버튼
    private void Start() // 게임 실행과 동시에 마스터 서버 접속 시도
    {
        PhotonNetwork.GameVersion = gameVersion; // 접속에 필요한 정보(게임 버전) 설정
        PhotonNetwork.ConnectUsingSettings(); // 설정한 정보로 마스터 서버 접속 시도

        joinButton.interactable = false; // 접속버튼 비활
        connectionInfoText.text = "마스터 서버에 접속 중...";
    }

    public override void OnConnectedToMaster() // 마스터 서버 접속 성공시 자동 실행
    {
        base.OnConnectedToMaster();
    }

    public override void OnDisconnected(DisconnectCause cause) // 마스터 접속 실패시 자동실행
    {
        base.OnDisconnected(cause);
    }
    public void OnConnect() // 룸 접속 시도
    {
        
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message) //빈방이 없어 랜덤 룸 참가에 실패한 경우 자동 실행
    {
        base.OnJoinRandomFailed(returnCode, message);
    }

    public override void OnJoinedRoom() // 룸에 참ㅁ가 완료된 경우 자동실행
    {
<<<<<<< HEAD
        connectionInfoText.text = "방 참가 성공";
        PhotonNetwork.LoadLevel("Town");

        PhotonNetwork.LocalPlayer.NickName = userId.text;//로컬 플레이어 이름을 설정 
        PlayerPrefs.SetString("USER_ID", userId.text); //플레이어 이름 저장 


=======
        base.OnJoinedRoom();
>>>>>>> parent of 0574268 (Merge branch 'main' of https://github.com/EunnWoo/TeamProjectUnity)
    }
}
