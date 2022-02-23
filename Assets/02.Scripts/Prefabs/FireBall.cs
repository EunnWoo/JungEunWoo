using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    Transform tr;
    Rigidbody rigid;
    [SerializeField]
    PlayerAttack playerAttack;
    Animator animator;

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
        animator = GameObject.FindWithTag("Player").GetComponentInChildren<Animator>();
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
            if (animator.GetBool("Fire"))
            {
                if (playerAttack.attackTarget.layer == (int)Layer.Monster) tr.position = Vector3.Lerp(tr.position, playerAttack.attackTarget.transform.position, 0.1f);
               
                
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

    private void OnDisable()//오브젝트 비활성화
    {
        //값 초기화
        tr.position = Vector3.zero;
        tr.rotation = Quaternion.identity;
        rigid.Sleep();
    }
}
