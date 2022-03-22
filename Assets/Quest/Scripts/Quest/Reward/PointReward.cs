using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quest/Reward/Gold", fileName = "GoldReward_")]
public class GoldReward : Reward
{
    PlayerStatus playerStatus;
    private void Awake() {
        playerStatus = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStatus>();
    }
    public override void Give(Quest quest)
    {
        //GameSystem.Instance.AddScore(Quantity);
        playerStatus.gold += Quantity;
    }
}
