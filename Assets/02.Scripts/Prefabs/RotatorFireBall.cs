using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorFireBall : MonoBehaviour
{

    PlayerAttack playerAttack;
    Transform tr;
    ParticleSystem particleObject; //파티클시스템
    
    bool explosion;
    void Start()
    {
        particleObject = GetComponent<ParticleSystem>();
        tr = GetComponent<Transform>();
    }
    private void OnEnable()
    {
        if (Managers.Game.GetPlayer() != null)
        {
            playerAttack = Managers.Game.GetPlayer().GetComponent<PlayerAttack>();
            
        }
        
        explosion = false;

    }
    private void Update()
    {
       
        if (playerAttack.attackTarget.layer != (int)Layer.Monster || explosion) return;
        tr.position = playerAttack.attackTarget.transform.position;
        if(particleObject.time >= 6.3f)
        {
            explosion = true;
            Status status= playerAttack.attackTarget.GetComponent<Status>(); // 몬스터 status
            Status playerStatus = playerAttack.GetComponent<Status>();
            status.TakeDamage(playerStatus, playerAttack.skillRatio);
            status.TakeDamage(playerStatus, playerAttack.skillRatio);
            status.TakeDamage(playerStatus, playerAttack.skillRatio);


        }
    }
    private void OnDisable()
    {
        

    }
}
