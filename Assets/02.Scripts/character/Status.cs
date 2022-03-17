using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{

    #region hp = baseHP + ��� + ����
    public float hp;

    public float baseHP = 300; //�⺻�ִ�ü��
    protected float wearHP = 0; //��� ���� ��� �ִ�ü��
    protected float levelHP = 0; //�������� ��� �ִ�ü��
   
    public float MAX_HP
    {
        get { return baseHP + wearHP + levelHP; }
    }
    #endregion

    #region mp = baseMP + ��� + ����
    public float mp { get; protected set; }
    public float baseMP = 300;
    protected float wearMP = 0;
    protected float levelMP = 0;
    public float MAX_MP
    {
        get { return baseMP + wearMP + levelMP; }
    }
    #endregion


    #region attack = baseAttack + ��� + ����
    public float attack { get { return baseAttack + wearAttack + levelAttack; } }
    public float baseAttack = 30;
    protected float wearAttack = 0;
    protected float levelAttack = 0;

    #endregion


    #region defense = baseDefense + ��� + ����
    public float defense { get { return baseDefense + wearDefense + levelDefense; } }
    protected float baseDefense = 3;
    protected float wearDefense = 0;
    protected float levelDefense = 0;
    #endregion

    bool bDeath;

   

    public  bool TakeDamage(Status attacker) //PlayerStatusTest ��ũ��Ʈ �����(ü��)
    {
        if (bDeath) return false; //���� ����ߴٸ�

        Debug.Log("ȣ��");
        int damage = Mathf.Max(0, (int)attacker.attack - (int)defense);
        hp -= damage;

        UI_Damage ui_Damage = Managers.UI.MakeWorldSpaceUI<UI_Damage>(transform);
        ui_Damage.target = transform;
        ui_Damage.damage = damage;

        if (attacker.tag == "Player")
        {
            UI_MonsterHpBar ui_MonsterHpBar = FindObjectOfType<UI_MonsterHpBar>();
            if (ui_MonsterHpBar != null)
            {
                ui_MonsterHpBar.ChangeMonsterHit(this);
            }
        }



        if (hp <= 0) //hp�� 0�̵Ǹ�
        {
            Debug.Log("@@@���");
            QuestReporter questReporter = GetComponent<QuestReporter>();
            questReporter.Report();
            bDeath = true;

            if (attacker.tag == "Player")
            {
                UI_MonsterHpBar ui_MonsterHpBar = FindObjectOfType<UI_MonsterHpBar>();
                if (ui_MonsterHpBar != null)
                {
                    ui_MonsterHpBar.OffMonsterHpbar();
                }
            }
        }
        //UI_PlayerData.ins.DisplayHP(hp, MAX_HP);
        return bDeath;
    }



}
