using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorFireBall : MonoBehaviour
{

    PlayerAttack playerAttack;

    ParticleSystem particleObject; //��ƼŬ�ý���

    bool explosion;
    private void OnEnable()
    {
        if (Managers.Game.GetPlayer() != null)
        {
            playerAttack = Managers.Game.GetPlayer().GetComponent<PlayerAttack>();
            
        }
        particleObject = GetComponent<ParticleSystem>();
        explosion = false;

    }
    private void Update()
    {
       
        if (playerAttack.attackTarget.layer != (int)Layer.Monster || explosion) return;
        transform.position = playerAttack.attackTarget.transform.position;
        if(particleObject.time >= 6.3f)
        {
            explosion = true;
            Status status= playerAttack.attackTarget.GetComponent<Status>(); // ���� status
            Status playerStatus = playerAttack.GetComponent<Status>();
            status.TakeDamage(playerStatus);
           

        }
    }
    private void OnDisable()
    {
        

    }
}
