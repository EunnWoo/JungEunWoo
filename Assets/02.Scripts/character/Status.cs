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
            

   

    public  virtual void TakeDamage(Status attacker,float ratio = 1f) //�´�Ÿ�� ȣ����
    {
        if (bDeath) return; //���� ����ߴٸ�

        gameObject.GetComponent<Animator>().SetTrigger("Hit");

        int damage = Random.Range( (int)(attacker.attack / 10),  (int)((attacker.attack- (int)defense) * ratio));
        hp -= damage;

        UI_Damage ui_Damage = Managers.UI.MakeWorldSpaceUI<UI_Damage>(transform);
        ui_Damage.target = transform;
        ui_Damage.damage = damage;


        if (hp <= 0) //hp�� 0�̵Ǹ�
        {
            Die();    
        }
       
  
    }


    public virtual void Die()
    {
        Debug.Log("@@@���");
        bDeath = true;
        QuestReporter questReporter = GetComponent<QuestReporter>();
        questReporter.Report();
        gameObject.GetComponent<Animator>().SetTrigger("Dead");
        gameObject.GetComponent<Rigidbody>().isKinematic = false;

    }

}
