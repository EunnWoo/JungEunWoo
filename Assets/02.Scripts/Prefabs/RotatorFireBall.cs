using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorFireBall : MonoBehaviour
{

    PlayerAttack playerAttack;

    ParticleSystem particleObject; //파티클시스템

    public float lifeTime { get; private set; }
    private void OnEnable()
    {
        if (Managers.Game.GetPlayer() != null)
        {
            playerAttack = Managers.Game.GetPlayer().GetComponent<PlayerAttack>();
            
        }
        particleObject = GetComponent<ParticleSystem>();

    }
    private void Update()
    {
        //Debug.Log(particleObject.time); 6.3초되면 데미지 들어가게 하는 함수 실행
        lifeTime = particleObject.time; // 이후 피격
        if (playerAttack.attackTarget.layer != (int)Layer.Monster) return;
        transform.position = playerAttack.attackTarget.transform.position;
    }

}
