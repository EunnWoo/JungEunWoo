using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonsterController
{
    [SerializeField]
    private Transform[] bombSpawn;
    private GameObject BombSlime;
    //private float moveSpeed = 8f, rotateSpeed = 3f;
    private string bombSlime;
    public ParticleSystem RangeParticle;
    public ParticleSystem DamageParticle;

    protected override void Init()
    {
        base.Init();
        //gameObject.SetActive(false);        
    }
    private void Awake() {
        bombSpawn = Util.FindChild(gameObject,"BombSpawn", true).GetComponentsInChildren<Transform>();
        bombSlime = "Boom_Slime_A";
        //particleSystem =  GetComponent<ParticleSystem>();
        attackRate = 4f;
        skillRate = 7f;

    }
    protected override void UpdateMoving(){
        base.UpdateMoving();
    }
    protected override void UpdateAttack()
    {
        base.UpdateAttack();

        if(isSkillReady){
            switch(Random.Range(5,7)){
                case 5:
                for(int i=1; i<bombSpawn.Length; i++){
                    BombSlime = Managers.Pool.MakeObj(bombSlime);
                    BombSlime.transform.position = bombSpawn[i].transform.position;
                    BombSlime.transform.rotation = bombSpawn[i].transform.rotation;
                    BombSlime.SetActive(true);
                    skillDelay = 0;
                }
                break;
                case 6:
                    animator.SetInteger("state",6);
                break;
                case 7:
                break;
            }
        }
    }

    protected override void OnAttack()
    {
        Vector3 vec = transform.localPosition + (-transform.forward * 5);
        Collider[] hit = Physics.OverlapSphere(vec, 4f,1<<(int)Layer.Player);

        for (int i = 0; i < hit.Length; i++)
        {
           
            Status status = hit[i].GetComponent<Status>();
            status.TakeDamage(GetComponent<Status>());
        }
    }
    void SkillStart()
    {
        RangeParticle.gameObject.SetActive(true);
    }
    void SkillAttack()
    {
        DamageParticle.gameObject.SetActive(true);
        Collider[] hit = Physics.OverlapSphere(DamageParticle.transform.position, 4.5f, 1 << (int)Layer.Player);

        for (int i = 0; i < hit.Length; i++)
        {
            Debug.Log("hit");
            Status status = hit[i].GetComponent<Status>();
            status.TakeDamage(GetComponentInParent<Status>());
        }
        RangeParticle.gameObject.SetActive(false);
        DamageParticle.gameObject.SetActive(false);

    }
    void SkillEnd()
    {
        
    }
}
