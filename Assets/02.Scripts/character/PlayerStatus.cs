using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerJob { None, Archer,Warrior,Magician};

[System.Serializable]
public class PartInfo
{
    //list �� ���� ���� -> �ش��̸��� on
    public string partName;
    public GameObject partDefault;
    public List<GameObject> partList = new List<GameObject>();

    public void Equip(string _partName)//����
    {
        for (int i = 0, imax = partList.Count; i < imax; i++)
        {
            if(partList[i].name == _partName) //�̸��� ������ Ų��
            {
                partList[i].SetActive(true);
            }
        }
    }

    public void UnEquio(string _partName) //��� Ż��
    {
        for (int i = 0, imax = partList.Count; i < imax; i++)
        {
            if (partList[i].name == _partName) //�̸��� ������ ����
            {
                partList[i].SetActive(false);
            }
        }
    }
}

public class PlayerStatus : Status
{
    #region sigleton

    public static PlayerStatus ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion


    #region PartInfo ����
    // 0     1       2      3   
    //Head, Armor, Weapon, Boots
    public List<PartInfo> listPartInfos = new List<PartInfo>();

    public void Equip(int _index, ItemData _itemData)
    {
        PartInfo _partInfo = listPartInfos[_index];
        //default off ����Ʈ�� ���ش�
        if(_partInfo.partDefault != null)
        {
            _partInfo.partDefault.SetActive(false);
        }
        _partInfo.Equip(_itemData.skin);
    }
    #endregion

    enum eAbiltyKind { LevelHP, LevelMP, LevelAttack, LevelDefense };
    public ParticleSystem psLevelUp;

    float gold1, gold2; 
    public float gold
    {
        //���ȼ� ������ ����2���� ����
        //��) �ʵ忡�� 1000��带 �����ҽ� gold1�� 500�� gold2�� 500�� �޾ƿͼ� ���� ��ħ
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

            if(level != _levelOld) //������ �ҽ�
            {
                if(level >=2) //������2�̻� 
                {
                    //StartCoroutine(Co_ShowLevelUp(2f));
                    psLevelUp.gameObject.SetActive(true);//������ ��ƼŬ ����
                    psLevelUp.Stop();
                    psLevelUp.Play();
                }
                
                hp = MAX_HP; //�������� hp�� ���� ȸ��
                mp = MAX_MP;
              //  UI_PlayerData.ins.DisplayHP(hp, MAX_HP);
                UI_PlayerData.ins.DisplayMP(mp, MAX_MP);
            }
            float _needExp = GetNeedExp(level) - GetNeedExp(level - 1); //���緹�� - ������
            float _curExp = totalExp - GetNeedExp(level - 1); //���������� ���緹������
            UI_PlayerData.ins.DisplayEXP(_curExp, _needExp);
            UI_PlayerData.ins.DisplayLevelText(level);
        }
        
    }
    bool bDeath;

    IEnumerator Start()
    {
        psLevelUp.gameObject.SetActive(false); //���۽� ������ ��ƼŬ ����

        yield return null;
        SetHPMP(baseHP, baseMP); //���۽�hp ,mp�� ó�� baseHP,baseMP ������ ������ ����
        expArray = new float[10 + 1];
        for(int i = 1; i < expArray.Length; i++)
        {
            expArray[i] = GetNeedExp(i);
        }
        
        //UI_PlayerData.ins.DisplayLevelText(1);
    }

    float GetNeedExp(float _level) //����ġ ����ϴ� �Լ�
    {
        return _level <= 0 ? 0 : (_level * 30 + 0); //������ �ʿ����ġ
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

        //���� ���̺��� �������� ã�Ҵµ� ��ã����� �ְ���(����)
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
            case eAbiltyKind.LevelHP:  _rtn = _level * 20; break;   //�������� �����ϴ�HP
            case eAbiltyKind.LevelMP: _rtn = _level * 20; break;    //�������� �����ϴ�MP
            case eAbiltyKind.LevelAttack: _rtn = _level * 5.0f; break;//�������� �����ϴ� Attack
            case eAbiltyKind.LevelDefense: _rtn = _level * 0.5f; break;//�������� �����ϴ�DEF
        }
        return _rtn;
    }


    public void SetHPMP(float _hp, float _mp)
    {
        //HP MP�� Plus
        hp += _hp; //������ ������
        hp = hp > MAX_HP ? MAX_HP : hp; //hp�� MAX���� �ʰ��ϸ� ���̻� ȸ�������ʴ´�
        //UI_PlayerData.ins.DisplayHP(hp, MAX_HP); //ü�� ������ �̹��� ������
        //HP MP
        mp += _mp; //������ ������
        mp = mp > MAX_MP ? MAX_MP : mp;//mp�� MAX���� �ʰ��ϸ� ���̻� ȸ�������ʴ´�
        UI_PlayerData.ins.DisplayMP(mp, MAX_MP);
    }


    public bool Skill(float _useMP) //PlayerStatusTest ��ũ��Ʈ �����(����)
    {
        //mp : ĳ���� Ŭ���� + ��� + ������ ��� ����� mp
        if (mp - _useMP < 0) 
        {
            Debug.Log("���� ����");
            return false; //���ǰ� �����ϸ� false

        }
        else
        {
            mp -= _useMP;
            UI_PlayerData.ins.DisplayMP(mp, MAX_MP);
            return true;

        }
    }




    #region ��������� ���� �Լ���

#if UNITY_EDITOR
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        //{
        //    //Monster Ŭ������ �������� �ٶ� �۵��ϴ� �ڵ�
        //    //-> trigger �ν�(���� : ������ �ν�)
        //    //->target.collider.GetComponent<PlayerStatus>().TakeDamage(��������);
        //    Debug.Log(">>Test (���ڵ�� ���Ϳ��� �۵��Ǿ �������� ������)������ �ֱ�");
        //    PlayerStatus _user = GetComponent<PlayerStatus>();
        //    if (_user)
        //    {
        //        _user.TakeDamage(10);
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.Alpha2))
        //{
        //    //�ڱ� �ڽſ��� �۵�
        //    PlayerStatus _user = GetComponent<PlayerStatus>();
        //    if (_user)
        //    {
        //        _user.Skill(10);
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Alpha3)) //���͸� ����ϸ� ȹ���ϴ� ����ġ
        {
            //�ڱ� �ڽſ��� �۵�
            //����ġ(10) <- _monster = GetComponent<����>();
            exp = exp + 5;

        }

        if (Input.GetKeyDown(KeyCode.Alpha4)) //���� 1~100�� ���� �߰��ϴ� �Լ�
        {
            gold += Random.Range(1, 100); 
        }

        if (Input.GetKeyDown(KeyCode.Alpha5)) //���� 1~100�� �����Լ�
        {
            gold -= Random.Range(1, 100);
        }
    }
#endif
    #endregion
}
