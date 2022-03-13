using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorFireBall : MonoBehaviour
{

    PlayerAttack playerAttack;

    public ParticleSystem particleObject; //��ƼŬ�ý���

    private void OnEnable()
    {
        playerAttack = Managers.Game.GetPlayer().GetComponent<PlayerAttack>();
        particleObject = GetComponent<ParticleSystem>();
//        transform.position = playerAttack.attackTarget.transform.position;

    //  particleObject.emission.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(6.3f,200) });
        
    }
    private void Update()
    {
        //Debug.Log(particleObject.time); 6.3�ʵǸ� ������ ���� �ϴ� �Լ� ����
        if (playerAttack.attackTarget.layer != (int)Layer.Monster) return;
        transform.position = playerAttack.attackTarget.transform.position;
    }

}
