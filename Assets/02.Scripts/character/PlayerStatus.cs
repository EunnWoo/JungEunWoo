using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    public static PlayerStatus ins;
    private void Awake()
    {
        ins = this;
    }

    //hp = baseHP + ��� + ����
    public float hp;
    private float baseHP = 300; //�⺻�ִ�ü��
    private float wearHP = 0; //��� ���� ��� ü��
    private float LevelHP = 0; //�������� ���ü��
    public float MAX_HP {
        get { return baseHP + wearHP + LevelHP; } 
    }

    //mp = baseHP + ��� + ����
    public float mp;
    private float baseMP = 300;
    private float wearMP = 0;
    private float LevelMP = 0;
    public float MAX_MP {
        get { return baseMP + wearMP + LevelMP; }
    }
    public float exp { get; set; }

    private void Start()
    {
        SetHPMP(baseHP, baseMP); //���۽�hp ,mp�� 0����
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

    public void TakeDamage(float _damage) //�����
    {
        hp -= _damage;
        if(hp <= 0)
        {
            Debug.Log("���");
        }
        UI_PlayerData.ins.DisplayHP(hp, MAX_HP);
    }

    public bool Skill(float _useMP) //�����
    {
        if (mp - _useMP < 0)
        {
            Debug.Log("���� ����");
            return false;
        }
        mp -= _useMP;
        UI_PlayerData.ins.DisplayMP(mp, MAX_MP);
        return true;
    }
}
