using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Transform tr;
    Rigidbody rigid;
    [SerializeField]
    PlayerAttack playerAttack;
    private ParticleSystem ps;
    private float red = 0f;
    private float green = 0f;
    private float blue = 0f;

    public GameObject child;
    private void OnEnable()
    {
        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();
        playerAttack = GameObject.FindWithTag("Player").GetComponent<PlayerAttack>();

        ps = GetComponent<ParticleSystem>();
        red = Random.Range(0, 255);
        green = Random.Range(0, 255);
        blue = Random.Range(0, 255);

        var main = ps.main;
        main.startColor = new Color(red, green,blue);
        main = child.GetComponent<ParticleSystem>().main;
        main.startColor = new Color(red, green, blue);
    }
    void Update()
    {
        
        if (playerAttack != null)
        {
            if (!playerAttack.isAttack)
            {
                if( playerAttack.attackTarget) tr.position = Vector3.Lerp(tr.position, playerAttack.attackTarget.transform.position, 0.1f);
                
            }

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Monster")
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDisable()//������Ʈ ��Ȱ��ȭ
    {
        //�� �ʱ�ȭ
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rigid.Sleep();
    }
}
