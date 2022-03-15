using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerJob { None, Archer,Warrior,Magician};
public class PlayerStatus : Status
{
    #region sigleton

    public static PlayerStatus ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion

    enum eAbiltyKind { LevelHP, LevelMP, LevelAttack, LevelDefense };
    public ParticleSystem psLevelUp;

    public float gold;
    public float level;

    float totalExp;
    float[] expArray;
    public float exp { 
        get { return totalExp; }
        set {
            float _plus = value - totalExp;
            totalExp += _plus;
            float _levelOld = level; 

            level           = GetLevel(totalExp);
            levelHP         = GetAbility(eAbiltyKind.LevelHP);
            levelMP         = GetAbility(eAbiltyKind.LevelMP);
            levelAttack     = GetAbility(eAbiltyKind.LevelAttack);
            levelDefense    = GetAbility(eAbiltyKind.LevelDefense);

            if(level != _levelOld) //레벨업 할시
            {
                if(level >=2) //레벨이2이상 
                {
                    StartCoroutine(Co_ShowLevelUp(2f));
                    psLevelUp.gameObject.SetActive(true);//레벨업 파티클 실행
                }
                
                hp = MAX_HP; //레벨업시 hp를 전부 회복
                mp = MAX_MP;
              //  UI_PlayerData.ins.DisplayHP(hp, MAX_HP);
                UI_PlayerData.ins.DisplayMP(mp, MAX_MP);
            }
            float _needExp = GetNeedExp(level) - GetNeedExp(level - 1); //현재레벨 - 전레벨
            float _curExp = totalExp - GetNeedExp(level - 1); //전레벨에서 현재레벨빼기
            UI_PlayerData.ins.DisplayEXP(_curExp, _needExp);
        }
    }
    bool bDeath;

    IEnumerator Start()
    {
        psLevelUp.gameObject.SetActive(false); //시작시 레벨업 파티클 끄기

        yield return null;
       SetHPMP(baseHP, baseMP); //시작시hp ,mp를 처음 baseHP,baseMP 선언한 값으로 시작
        expArray = new float[10 + 1];
        for(int i = 1; i < expArray.Length; i++)
        {
            expArray[i] = GetNeedExp(i);
        }
    }
   
    float GetNeedExp(float _level) //경험치 계산하는 함수
    {
        return _level <= 0 ? 0 : (_level * 10 + 10);
    }


    float GetLevel(float _totalExp)
    {
        float _level = 1;
        bool _bFind = false;
        for (int i = 0; i < expArray.Length -1; i++)
        {
            if(_totalExp >= expArray[i] && _totalExp < expArray[i + 1])
            {
                _level = i + 1;
                _bFind = true;
            }
        }

        //레벨 테이블에서 레벨값을 찾았는데 못찾을경우 최고레벨
        if(!_bFind)
        {
            _level = expArray.Length;
        }
        return _level;
    }

    float GetAbility(eAbiltyKind _kind)
    {
        float _rtn = 0;
        float _level = level - 1;
        switch (_kind)
        {
            case eAbiltyKind.LevelHP:  _rtn = _level * 6; break;
            case eAbiltyKind.LevelMP: _rtn = _level * 3; break;
            case eAbiltyKind.LevelAttack: _rtn = _level * 1.8f; break;
            case eAbiltyKind.LevelDefense: _rtn = _level * 0.5f; break;
        }
        return _rtn;
    }


    public void SetHPMP(float _hp, float _mp)
    {
        //HP MP를 Plus
        hp += _hp; //물약을 먹을시
        hp = hp > MAX_HP ? MAX_HP : hp; //hp가 MAX양을 초과하면 더이상 회복하지않는다
        //UI_PlayerData.ins.DisplayHP(hp, MAX_HP); //체력 게이지 이미지 움직임
        //HP MP
        mp += _mp; //물약을 먹을시
        mp = mp > MAX_MP ? MAX_MP : mp;//mp가 MAX양을 초과하면 더이상 회복하지않는다
        UI_PlayerData.ins.DisplayMP(mp, MAX_MP);
    }


    public bool Skill(float _useMP) //PlayerStatusTest 스크립트 실험용(엠피)
    {
        //mp : 캐릭터 클래스 + 장비 + 레벨이 모두 적용된 mp
        if (mp - _useMP < 0) 
        {
            Debug.Log("엠피 부족");
            return false; //엠피가 부족하면 false

        }
        else
        {
            mp -= _useMP;
            UI_PlayerData.ins.DisplayMP(mp, MAX_MP);
            return true;

        }
    }

    //LevelUp 파티클 시스템 보여주기 (일정시간동안)
    IEnumerator Co_ShowLevelUp(float _duration)
    {
        psLevelUp.gameObject.SetActive(true); //레벨업 파티클켜기
        while(_duration > 0)
        {
            _duration -= Time.deltaTime; //일정시간동안
            yield return null;
        }
        psLevelUp.gameObject.SetActive(false);//레벨업 파티클끄기
    }




//#if UNITY_EDITOR
//    private void Update()
//    {
//        if (Input.GetKeyDown(KeyCode.Alpha1))
//        {
//            //Monster 클래스의 데미지를 줄때 작동하는 코드
//            //-> trigger 인식(상대방 : 유저를 인식)
//            //->target.collider.GetComponent<PlayerStatus>().TakeDamage(데미지값);
//            Debug.Log(">>Test (이코드는 몬스터에서 작동되어서 유저에게 데미지)데미지 주기");
//             PlayerStatus _user =  GetComponent<PlayerStatus>();
//            if (_user)
//            {
//                _user.TakeDamage(10);
//            }
//        }

//        if (Input.GetKeyDown(KeyCode.Alpha2))
//        {
//            //자기 자신에서 작동
//            PlayerStatus _user = GetComponent<PlayerStatus>();
//            if (_user)
//            {
//                _user.Skill(10);
//            }
//        }

//        if (Input.GetKeyDown(KeyCode.Alpha3)) //몬스터를 사냥하면 획득하는 경험치
//        {
//            //자기 자신에서 작동
//            //경험치(10) <- _monster = GetComponent<몬스터>();
//            exp = exp + 3;
            
//        }
//    }
//#endif
}
