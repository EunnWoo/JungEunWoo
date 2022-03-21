using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerJob { None, Archer,Warrior,Magician};

[System.Serializable]
public class PartInfo
{
    //list 를 전부 끄고 -> 해당이름만 on
    public string partName;
    public GameObject partDefault;
    public List<GameObject> partList = new List<GameObject>();

    public void Equip(string _partName)//장착
    {
        for (int i = 0, imax = partList.Count; i < imax; i++)
        {
            if(partList[i].name == _partName) //이름이 같으면 킨다
            {
                partList[i].SetActive(true);
            }
        }
    }

    public void UnEquio(string _partName) //장비 탈착
    {
        for (int i = 0, imax = partList.Count; i < imax; i++)
        {
            if (partList[i].name == _partName) //이름이 같으면 끈다
            {
                partList[i].SetActive(false);
            }
        }
    }
}

public class PlayerStatus : Status
{


    #region PartInfo 정보
    // 0     1       2      3   
    //Head, Armor, Weapon, Boots
    public List<PartInfo> listPartInfos = new List<PartInfo>();

    public void Equip(int _index, ItemData _itemData)
    {
        PartInfo _partInfo = listPartInfos[_index];
        //default off 디폴트를 꺼준다
        if(_partInfo.partDefault != null)
        {
            _partInfo.partDefault.SetActive(false);
        }
        _partInfo.Equip(_itemData.skin);
    }

    public void UnEquip(int _index, ItemData _itemData)
    {
        Debug.Log("@@@장비해제에 따른 파라미터 계산");
        //PartInfo _partInfo = listPartInfos[_index];
        ////default off 디폴트를 꺼준다
        //if (_partInfo.partDefault != null)
        //{
        //    _partInfo.partDefault.SetActive(false);
        //}
        //_partInfo.Equip(_itemData.skin);
    }
    #endregion

    enum eAbiltyKind { LevelHP, LevelMP, LevelAttack, LevelDefense };
    public ParticleSystem psLevelUp;

    float gold1, gold2; 
    public float gold
    {
        //보안성 때문에 변수2개로 지정
        //예) 필드에서 1000골드를 습득할시 gold1에 500원 gold2에 500원 받아와서 둘이 합침
        get { return gold1 + gold2; }
        set
        {
            float _plus =  value - (gold1 + gold2); //value =gold1 + gold2 + _pick.count
            int _g1 = (int)_plus / 2;
            int _g2 = (int)_plus - _g1;

            gold1 += _g1;
            gold2 += _g2;

            UI_Money.ins.DisplayCoin(gold);
        }
    }
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
                    //StartCoroutine(Co_ShowLevelUp(2f));
                    psLevelUp.gameObject.SetActive(true);//레벨업 파티클 실행
                    psLevelUp.Stop();
                    psLevelUp.Play();
                    // Managers 소리넣기
                }
                
                hp = MAX_HP; //레벨업시 hp를 전부 회복
                mp = MAX_MP;
              //  UI_PlayerData.ins.DisplayHP(hp, MAX_HP);
                UI_PlayerData.ins.DisplayMP(mp, MAX_MP);
            }
            float _needExp = GetNeedExp(level) - GetNeedExp(level - 1); //현재레벨 - 전레벨
            float _curExp = totalExp - GetNeedExp(level - 1); //전레벨에서 현재레벨빼기
            UI_PlayerData.ins.DisplayEXP(_curExp, _needExp);
            UI_PlayerData.ins.DisplayLevelText(level);
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
        
        //UI_PlayerData.ins.DisplayLevelText(1);
    }

    float GetNeedExp(float _level) //경험치 계산하는 함수
    {
        return _level <= 0 ? 0 : (_level * 30 + 0); //레벨당 필요경험치
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

        //레벨 테이블에서 레벨값을 찾았는데 못찾을경우 최고레벨(만렙)
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
            case eAbiltyKind.LevelHP:  _rtn = _level * 20; break;   //레벨업시 증가하는HP
            case eAbiltyKind.LevelMP: _rtn = _level * 20; break;    //레벨업시 증가하는MP
            case eAbiltyKind.LevelAttack: _rtn = _level * 5.0f; break;//레벨업시 증가하는 Attack
            case eAbiltyKind.LevelDefense: _rtn = _level * 0.5f; break;//레벨업시 증가하는DEF
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

    public override void Die()
    {
        base.Die();
        
    }



    #region 실험용으로 만든 함수들

#if UNITY_EDITOR
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    //Monster 클래스의 데미지를 줄때 작동하는 코드
        //    //-> trigger 인식(상대방 : 유저를 인식)
        //    //->target.collider.GetComponent<PlayerStatus>().TakeDamage(데미지값);
        //    Debug.Log(">>Test (이코드는 몬스터에서 작동되어서 유저에게 데미지)데미지 주기");
        //    PlayerStatus _user = GetComponent<PlayerStatus>();
        //    if (_user)
        //    {
        //        _user.TakeDamage(10);
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    //자기 자신에서 작동
        //    PlayerStatus _user = GetComponent<PlayerStatus>();
        //    if (_user)
        //    {
        //        _user.Skill(10);
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Alpha3)) //몬스터를 사냥하면 획득하는 경험치
        {
            //자기 자신에서 작동
            //경험치(10) <- _monster = GetComponent<몬스터>();
            exp = exp + 5;

        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) //돈을 1~100원 랜덤 추가하는 함수
        {
            gold += Random.Range(1, 100); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) //돈을 1~100원 빼는함수
        {
            gold -= Random.Range(1, 100);
        }
    }
#endif
    #endregion
}
