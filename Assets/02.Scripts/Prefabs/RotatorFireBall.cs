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
//        transform.position = playerAttack.attackTarget.transform.position;

    //  particleObject.emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(6.3f,200) });
        
    }
    private void Update()
    {
        //Debug.Log(particleObject.time); 6.3초되면 데미지 들어가게 하는 함수 실행
        if (playerAttack.attackTarget.layer != (int)Layer.Monster) return;
        transform.position = playerAttack.attackTarget.transform.position;
    }

}
