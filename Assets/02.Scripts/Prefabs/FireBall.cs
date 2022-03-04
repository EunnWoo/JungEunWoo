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


    private float red = 0f;
    private float green = 0f;
    private float blue = 0f;

    [SerializeField]
    ParticleSystem ps;
    [SerializeField]
    ParticleSystem childps;


    Vector3 offset;
    public float speed = 500f;

    private void Start()
    {
        ps = GetComponent<ParticleSystem>();
        childps = this.transform.Find("Fire").GetComponent<ParticleSystem>();
        red = Random.Range(0, 255);
        green = Random.Range(0, 255);
        blue = Random.Range(0, 255);
        var main = ps.main;
        main.startColor = new Color(red, green, blue, 255);

        main = childps.main;
        main.startColor = new Color(red, green, blue, 255);
    }
    private void OnEnable()
    {

        tr = GetComponent<Transform>();
        rigid = GetComponent<Rigidbody>();

        if (Managers.Game.GetPlayer() != null)
        {
            playerAttack = Managers.Game.GetPlayer().GetComponent<PlayerAttack>();
            animator = Managers.Game.GetPlayer().GetComponentInChildren<Animator>();
        }
        
        




    }
    void Update()
    {
     
        if ((Vector3.Distance(transform.position, offset) >= 20f))//사정거리 벗어나면
        {
            DisableFireBall();
        }

        if (playerAttack != null)
        {
            if (animator.GetBool("Fire"))
            {
                if (playerAttack.attackTarget.layer == (int)Layer.Monster)
                {
                    Vector3 vec = playerAttack.attackTarget.transform.position;
                    var cal = playerAttack.attackTarget.GetComponent<Collider>();
                    vec.y += cal.bounds.size.y / 2; // 몹의 중앙에 파이어볼 향하게
                    tr.position = Vector3.Lerp(tr.position, vec, 0.1f);
                }
                else if (playerAttack.attackTarget.layer == (int)Layer.Ground)
                {
                    rigid.AddForce(transform.forward * 30f);
                }
                

            }

        }
    }
    public void FireFireBall(Transform firepos)
    {
        offset = firepos.position;
        rigid.AddForce(transform.right * speed);

    }
    public void DisableFireBall()
    {
        gameObject.SetActive(false);
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
