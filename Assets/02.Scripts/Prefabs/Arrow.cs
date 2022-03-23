using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public float speed = 500f;
    private Rigidbody rigid;

    Vector3 offset;

    public GameObject chargeParticle;
    public GameObject fireParticle;
    PlayerAttack playerAttack;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        // chargeParticle = GameObject.Find("StormCharge");
        //  fireParticle = GameObject.Find("StormCleave");


    }
    private void OnEnable()
    {
        if (Managers.Game.GetPlayer() != null)
        {
            playerAttack = Managers.Game.GetPlayer().GetComponent<PlayerAttack>();
        }
    }

    private void Update()
    {
        if ((Vector3.Distance(transform.position, offset) >= 20f))//사정거리 벗어나면
        {
            DisableArrow();
        }

    }

    public void FireArrow(Transform firepos)
    {
        offset = firepos.position;
        rigid.AddForce(transform.right * speed);

    }
  
    public void DisableArrow() 
    {
        chargeParticle.SetActive(false);
        fireParticle.SetActive(false);
        transform.SetParent(Managers.Pool.objPoolManager);
        gameObject.SetActive(false);
            
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "Monster" ) 
        {
            rigid.Sleep();
            rigid.useGravity = false;
            transform.position = other.ClosestPointOnBounds(transform.position);
            //피격처리
            Status playerstatus = playerAttack.GetComponent<Status>();
            Status status = other.GetComponent<Status>();

            status.TakeDamage(playerstatus,playerAttack.attackRatio);

            DisableArrow();
        }
        else if (other.tag == "Ground")
        {
            rigid.Sleep();
            rigid.useGravity = false;
            transform.position = other.ClosestPointOnBounds(transform.position) + new Vector3(0, 0.5f, 0);
            Invoke("DisableArrow", 3f);
        }
        
    }


    private void OnDisable()//오브젝트 비활성화
    {
        //값 초기화
        
        transform.position = Vector3.zero;
        transform.rotation = Quaternion.identity;
        rigid.Sleep();


    }

    
}
