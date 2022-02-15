//using Photon.Pun; //유니티용 포톤 컴포넌트
//using Photon.Realtime; // 포톤서비스 관련 라이브러리
//using UnityEngine;
//using UnityEngine.UI;
//public class LoginManager : MonoBehaviourPunCallbacks
//{
//    private string gameVersion = "1"; // 게임버전

//    public Text connectionInfoText; // 네트워크 정보를 표시할 텍스트
//    public Button joinButton; // 룸 접속 버튼
//    private void Start() // 게임 실행과 동시에 마스터 서버 접속 시도
//    {
        
//    }

//    public override void OnConnectedToMaster() // 마스터 서버 접속 성공시 자동 실행
//    {
//        base.OnConnectedToMaster();
//    }

//    public override void OnDisconnected(DisconnectCause cause) // 마스터 접속 실패시 자동실행
//    {
//        base.OnDisconnected(cause);
//    }
//    public void OnConnect() // 룸 접속 시도
//    {
        
//    }
    
//    public override void OnJoinRandomFailed(short returnCode, string message) //빈방이 없어 랜덤 룸 참가에 실패한 경우 자동 실행
//    {
//        base.OnJoinRandomFailed(returnCode, message);
//    }

//    public override void OnJoinedRoom() // 룸에 참ㅁ가 완료된 경우 자동실행
//    {
//        base.OnJoinedRoom();
//    }
//}
