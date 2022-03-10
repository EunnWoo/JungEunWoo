using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun; // ����Ƽ�� ���� ������Ʈ��
using Photon.Realtime; // ���� ���� ���� ���̺귯��
public class PhotonJoin : MonoBehaviourPunCallbacks
{

    private string gameVersion = "1"; // ���� ����

    void Start()
    {
        //���ӿ� �ʿ��� ���� ����
        PhotonNetwork.GameVersion = gameVersion;
        //������ ������ ������ ���� ���� �õ�
        PhotonNetwork.ConnectUsingSettings();
    }

}
