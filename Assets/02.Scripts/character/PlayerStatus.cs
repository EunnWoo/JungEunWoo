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

    //hp = baseHP + 장비 + 레벨
    public float hp;
    private float baseHP = 300; //기본최대체력
    private float wearHP = 0; //장비를 껴서 얻는 체력
    private float LevelHP = 0; //레벨업시 얻는체력
    public float MAX_HP {
        get { return baseHP + wearHP + LevelHP; } 
    }

    //mp = baseHP + 장비 + 레벨
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
        SetHPMP(baseHP, baseMP); //시작시hp ,mp를 0으로
    }

    public void SetHPMP(float _hp, float _mp)
    {
        //HP MP를 Plus
        hp += _hp; //물약을 먹을시
        hp = hp > MAX_HP ? MAX_HP : hp; //hp가 MAX양을 초과하면 더이상 회복하지않는다
        UI_PlayerData.ins.DisplayHP(hp, MAX_HP); //체력 게이지 이미지 움직임
        //HP MP
        mp += _mp; //물약을 먹을시
        mp = mp > MAX_MP ? MAX_MP : mp;//mp가 MAX양을 초과하면 더이상 회복하지않는다
        UI_PlayerData.ins.DisplayMP(mp, MAX_MP);
    }

    public void TakeDamage(float _damage) //실험용
    {
        hp -= _damage;
        if(hp <= 0)
        {
            Debug.Log("사망");
        }
        UI_PlayerData.ins.DisplayHP(hp, MAX_HP);
    }

    public bool Skill(float _useMP) //실험용
    {
        if (mp - _useMP < 0)
        {
            Debug.Log("엠피 부족");
            return false;
        }
        mp -= _useMP;
        UI_PlayerData.ins.DisplayMP(mp, MAX_MP);
        return true;
    }
}
