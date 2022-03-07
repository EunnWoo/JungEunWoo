using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class PlayerListItem : MonoBehaviourPunCallbacks
{
    [SerializeField] Text text;
    Player player; // ���� ����Ÿ���� Player�� ���� �� �� �ִ�

    public void SetUp(Player _player)
    {
        player = _player;
        text.text = _player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) //�÷��̾ ���� �������� ȣ��
    {
        if(player ==otherPlayer)
        {
            Destroy(gameObject); //�̸�ǥ ����
        }
    }

    public override void OnLeftRoom() // �泪���� ȣ��
    {
        Destroy(gameObject);
    }

}
