using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Reward/Point", fileName = "PointReward_")]
public class GoldReward : Reward
{

    public override void Give(Quest quest)
    {
        PlayerStatus playerStatus = Managers.Game.GetPlayer().GetComponent<PlayerStatus>();
        playerStatus.gold += Quantity;
        playerStatus.exp += Quantity;
    }
}
