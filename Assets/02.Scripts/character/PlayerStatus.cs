using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ePlayerJob { None, Archer,Warrior,Magician};
public class PlayerStatus : MonoBehaviour
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

    #region hp = baseHP + ��� + ����
    public float hp { get; private set; }
    private float baseHP = 300; //�⺻�ִ�ü��
    private float wearHP = 0; //��� ���� ��� �ִ�ü��
    private float levelHP = 0; //�������� ��� �ִ�ü��

    public float MAX_HP {
        get { return baseHP + wearHP + levelHP; } 
    }
    #endregion


    #region mp = baseMP + ��� + ����
    public float mp { get; private set; }
    private float baseMP = 300;
    private float wearMP = 0;
    private float levelMP = 0;
    public float MAX_MP {
        get { return baseMP + wearMP + levelMP; }
    }
    #endregion


    #region attack = baseAttack + ��� + ����
    public float attack { get { return baseAttack + wearAttack + levelAttack; } }
    private float baseAttack = 5; 
    private float wearAttack = 0; 
    private float levelAttack = 0; 
    
    #endregion


    #region defense = baseDefense + ��� + ����
    public float defense { get { return baseDefense + wearDefense + levelDefense; } }
    private float baseDefense = 3; 
    private float wearDefense = 0; 
    private float levelDefense = 0; 
    #endregion


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

            if(level != _levelOld) //������ �ҽ�
            {
                if(level >=2) //������2�̻� 
                {
                    StartCoroutine(Co_ShowLevelUp(2f));
                    psLevelUp.gameObject.SetActive(true);//������ ��ƼŬ ����
                }
                
                hp = MAX_HP; //�������� hp�� ���� ȸ��
                mp = MAX_MP;
                UI_PlayerData.ins.DisplayHP(hp, MAX_HP);
                UI_PlayerData.ins.DisplayMP(mp, MAX_MP);
            }
            float _needExp = GetNeedExp(level) - GetNeedExp(level - 1); //���緹�� - ������
            float _curExp = totalExp - GetNeedExp(level - 1); //���������� ���緹������
            UI_PlayerData.ins.DisplayEXP(_curExp, _needExp);
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
    }
   
    float GetNeedExp(float _level) //����ġ ����ϴ� �Լ�
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

        //���� ���̺��� �������� ã�Ҵµ� ��ã����� �ְ���
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
        //HP MP�� Plus
        hp += _hp; //������ ������
        hp = hp > MAX_HP ? MAX_HP : hp; //hp�� MAX���� �ʰ��ϸ� ���̻� ȸ�������ʴ´�
        UI_PlayerData.ins.DisplayHP(hp, MAX_HP); //ü�� ������ �̹��� ������
        //HP MP
        mp += _mp; //������ ������
        mp = mp > MAX_MP ? MAX_MP : mp;//mp�� MAX���� �ʰ��ϸ� ���̻� ȸ�������ʴ´�
        UI_PlayerData.ins.DisplayMP(mp, MAX_MP);
    }




    public bool TakeDamage(float _damage) //PlayerStatusTest ��ũ��Ʈ �����(ü��)
    {
        if (bDeath) return false; //���� ����ߴٸ�

        _damage = Mathf.Max(0, (_damage - defense)); //���� �������� ���ؼ� ������ - ������ ���� hp�� ����
        hp -= _damage;
        if(hp <= 0) //hp�� 0�̵Ǹ�
        {
            Debug.Log("@@@���");
            bDeath = true;
        }
        UI_PlayerData.ins.DisplayHP(hp, MAX_HP);
        return bDeath;
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

    //LevelUp ��ƼŬ �ý��� �����ֱ� (�����ð�����)
    IEnumerator Co_ShowLevelUp(float _duration)
    {
        psLevelUp.gameObject.SetActive(true); //������ ��ƼŬ�ѱ�
        while(_duration > 0)
        {
            _duration -= Time.deltaTime; //�����ð�����
            yield return null;
        }
        psLevelUp.gameObject.SetActive(false);//������ ��ƼŬ����
    }




#if UNITY_EDITOR
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //Monster Ŭ������ �������� �ٶ� �۵��ϴ� �ڵ�
            //-> trigger �ν�(���� : ������ �ν�)
            //->target.collider.GetComponent<PlayerStatus>().TakeDamage(��������);
            Debug.Log(">>Test (���ڵ�� ���Ϳ��� �۵��Ǿ �������� ������)������ �ֱ�");
             PlayerStatus _user =  GetComponent<PlayerStatus>();
            if (_user)
            {
                _user.TakeDamage(10);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //�ڱ� �ڽſ��� �۵�
            PlayerStatus _user = GetComponent<PlayerStatus>();
            if (_user)
            {
                _user.Skill(10);
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3)) //���͸� ����ϸ� ȹ���ϴ� ����ġ
        {
            //�ڱ� �ڽſ��� �۵�
            //����ġ(10) <- _monster = GetComponent<����>();
            exp = exp + 3;
            
        }
    }
#endif
}
