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
    Player player; // 포톤 리얼타임은 Player를 선언 할 수 있다

    public void SetUp(Player _player)
    {
        player = _player;
        text.text = _player.NickName;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer) //플레이어가 방을 떠났을때 호출
    {
        if(player ==otherPlayer)
        {
            Destroy(gameObject); //이름표 삭제
        }
    }

    public override void OnLeftRoom() // 방나가면 호출
    {
        Destroy(gameObject);
    }

}
