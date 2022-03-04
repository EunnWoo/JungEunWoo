using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState {
    Idle = 0,
    Walk = 1,
    Attack = 2,
    Trace = 3,
    Die = 4
}

public class MonsterController : BaseController
{
    private float moveSpeed = 8f, rotateSpeed = 3f;
    public Vector3 limitRange_Min, limitRange_Max;
    private MonsterState monsterState;
    [SerializeField]
    private GameObject player;
    private Vector3 movePos;
    [SerializeField]
    float _scanRange = 10;

    [SerializeField]
    float _attackRange = 4;

    private float distance = 0f;

    public float attackRange { get; protected set; }
    public float scanRange { get; protected set; }
    public override void Init()
    {
        player = Managers.Game.GetPlayer();
        InvokeRepeating("RandomPos", 2f, 3f);
        monsterState = MonsterState.Idle;
        Anim = GetComponent<Animator>();
        //초기화 하는 함수
    }
    protected virtual void PlayerScan()// --> 상속받은 monster들 마다 스캔방식 다르게  ex) rayhit(보는 방향)골드메탈 참고 Or 적과 플레이어 distance 값 받아서 범위 스캔
    {
        //ex RaycastHit[] rayHits = Physics.SphereCastAll(transform.position,반지름 , tarnsform.forward, scanRange, LayMask.GetMask("Player"));
        //ex 
        if (player == null)
         return;
        distance = (player.transform.position - transform.position).magnitude;
        if(distance <= _attackRange){
            monsterState = MonsterState.Attack;
            return;
        }
        else if (distance <= _scanRange)
        {
            _lockTarget = player;
            monsterState = MonsterState.Trace;
            return;
        }
        else{
            monsterState = MonsterState.Walk;
        }
    }

    protected override void UpdateMoving()
    {
        PlayerScan();
        Debug.Log(monsterState);
        Debug.Log(_attackRange);
        switch(monsterState){
            case MonsterState.Idle:
            if(Random.Range(1, 20) == 2)
                monsterState = MonsterState.Walk;
            break;
            case MonsterState.Walk:
            LookDirection(transform, movePos);
            RigidMovePos(transform, movePos, moveSpeed);
            LimitMoveRange(transform, limitRange_Min, limitRange_Max);
            if(Random.Range(1, 20) == 2)
                monsterState = MonsterState.Idle;
            Anim.SetInteger("state", (int)monsterState);
            break;
            case MonsterState.Trace:
            LookTarget(transform, player.transform, rotateSpeed);
            if(distance >= _attackRange){
                RigidMovePos(transform, player.transform.position - transform.position, moveSpeed);
            }
            LimitMoveRange(transform, limitRange_Min, limitRange_Max);
            Anim.SetInteger("state", (int)monsterState);
            //if( attackRange)
            break;
            case MonsterState.Attack:
            UpdateAttack();
            break;
        }
        
    }
    
    protected override void UpdateAttack()
    {
        Anim.SetInteger("state", (int)monsterState);
    }
    private void RandomPos(){
        movePos = new Vector3(Random.Range(-1, 2), transform.position.y, Random.Range(-1, 2));
    }
    public void LookDirection(Transform objTransform, Vector3 moveDir) {
        objTransform.rotation = Quaternion.LookRotation(-moveDir.x * Vector3.right + -moveDir.z * Vector3.forward);
    }

    public void LookTarget(Transform objTransform, Transform targetTransform, float speed) {
        Vector3 dir = new Vector3(objTransform.position.x - targetTransform.transform.position.x, 0, objTransform.position.z - targetTransform.transform.position.z);
        objTransform.rotation = Quaternion.Lerp(objTransform.rotation, Quaternion.LookRotation(dir), speed * Time.fixedDeltaTime);
    }

    public void RigidMovePos(Transform objTransform, Vector3 dir, float speed) {
        objTransform.gameObject.GetComponent<Rigidbody>().MovePosition(objTransform.position + new Vector3(dir.x, 0, dir.z).normalized * speed * Time.fixedDeltaTime);
    }

    public void LimitMoveRange(Transform objTransform, Vector3 minRange, Vector3 maxRange) {
        objTransform.position = new Vector3(Mathf.Clamp(objTransform.position.x, minRange.x, maxRange.x), objTransform.position.y, 
                                Mathf.Clamp(objTransform.position.z, minRange.z, maxRange.z));
    }
}
