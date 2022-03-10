using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime; // 포톤 서비스 관련 라이브러리
public class PhotonJoin : MonoBehaviourPunCallbacks
{

    private string gameVersion = "1"; // 게임 버전

    void Start()
    {
        //접속에 필요한 정보 설정
        PhotonNetwork.GameVersion = gameVersion;
        //설정한 정보로 마스터 서버 접속 시도
        PhotonNetwork.ConnectUsingSettings();
    }

}
