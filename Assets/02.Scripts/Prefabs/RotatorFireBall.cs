using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorFireBall : MonoBehaviour
{

    PlayerAttack playerAttack;

    public ParticleSystem particleObject; //파티클시스템

    private void OnEnable()
    {
        playerAttack = Managers.Game.GetPlayer().GetComponent<PlayerAttack>();
        particleObject = GetComponent<ParticleSystem>();
        transform.position = playerAttack.attackTarget.transform.position;
    }
    void OnParticleSystemStopped()
    {
        Debug.Log("끝남");
    }
}
