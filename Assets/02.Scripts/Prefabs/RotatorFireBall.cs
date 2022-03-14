using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatorFireBall : MonoBehaviour
{

    PlayerAttack playerAttack;

    ParticleSystem particleObject; //��ƼŬ�ý���

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
        //Debug.Log(particleObject.time); 6.3�ʵǸ� ������ ���� �ϴ� �Լ� ����
        lifeTime = particleObject.time; // ���� �ǰ�
        if (playerAttack.attackTarget.layer != (int)Layer.Monster) return;
        transform.position = playerAttack.attackTarget.transform.position;
    }

}
