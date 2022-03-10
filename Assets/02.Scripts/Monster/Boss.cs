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
    float attackDelay;
    bool isAttackReady;
    float attackRate;
    public override void Init()
    {
        base.Init();
        //gameObject.SetActive(false);        
    }
    private void Awake() {
        bombSpawn = Util.FindChild(gameObject,"BombSpawn", true).GetComponentsInChildren<Transform>();
        bombSlime = "Boom_Slime_A";
        attackRate = 15f;
    }
    protected override void UpdateMoving(){
        base.UpdateMoving();
    }
    protected override void UpdateAttack()
    {
        base.UpdateAttack();
        attackDelay += Time.deltaTime;
        isAttackReady = attackRate < attackDelay;
        
        if(isAttackReady){
            switch(Random.Range(0,2)){
                case 0:
                for(int i=1; i<bombSpawn.Length; i++){
                    BombSlime = Managers.Pool.MakeObj(bombSlime);
                    BombSlime.transform.position = bombSpawn[i].transform.position;
                    BombSlime.transform.rotation = bombSpawn[i].transform.rotation;
                    BombSlime.SetActive(true);
                    isAttackReady = false;
                    attackDelay = 0;
                }
                break;
                case 1:
                break;
                case 2:
                break;
            }
        }
    }
}
