using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{

    #region hp = baseHP + 장비 + 레벨
    public float hp;

    public float baseHP = 300; //기본최대체력
    protected float wearHP = 0; //장비를 껴서 얻는 최대체력
    protected float levelHP = 0; //레벨업시 얻는 최대체력
   
    public float MAX_HP
    {
        get { return baseHP + wearHP + levelHP; }
    }
    #endregion

    #region mp = baseMP + 장비 + 레벨
    public float mp { get; protected set; }
    public float baseMP = 300;
    protected float wearMP = 0;
    protected float levelMP = 0;
    public float MAX_MP
    {
        get { return baseMP + wearMP + levelMP; }
    }
    #endregion


    #region attack = baseAttack + 장비 + 레벨
    public float attack { get { return baseAttack + wearAttack + levelAttack; } }
    public float baseAttack = 100;
    protected float wearAttack = 0;
    protected float levelAttack = 0;

    #endregion


    #region defense = baseDefense + 장비 + 레벨
    public float defense { get { return baseDefense + wearDefense + levelDefense; } }
    protected float baseDefense = 3;
    protected float wearDefense = 0;
    protected float levelDefense = 0;
    #endregion

    bool bDeath;

   

    public  bool TakeDamage(Status attacker) //PlayerStatusTest 스크립트 실험용(체력)
    {
        if (bDeath) return false; //만약 사망했다면

        Debug.Log(attack);
        int damage = Mathf.Max(0, (int)attacker.attack - (int)defense);
        hp -= damage;

        UI_Damage ui_Damage = Managers.UI.MakeWorldSpaceUI<UI_Damage>(transform);
        ui_Damage.target = transform;
        ui_Damage.damage = damage;
        //Collider collider = GetComponent<Collider>();
        //UI_Damage ui_Damage = Managers.UI.ShowPopupUI<UI_Damage>();
        //ui_Damage.transform.position = collider.bounds.max;
        //ui_Damage.damage = damage;



        if (hp <= 0) //hp가 0이되면
        {
            Debug.Log("@@@사망");
            QuestReporter questReporter = GetComponent<QuestReporter>();
            questReporter.Report();
            bDeath = true;
        }
        //UI_PlayerData.ins.DisplayHP(hp, MAX_HP);
        return bDeath;
    }



}
