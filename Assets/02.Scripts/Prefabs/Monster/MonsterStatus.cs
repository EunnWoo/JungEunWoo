using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : Status
{
    List<GameObject> items = new List<GameObject>();
    // QuestReporter questReporter;
    Rigidbody rigidbody;
    protected override void Init()
    {
        base.Init();
        Hp = MAX_HP;
        // questReporter = GetComponent<QuestReporter>();
        rigidbody = GetComponent<Rigidbody>();
    }
    


    public override void TakeDamage(Status attacker, float ratio)
    {
        base.TakeDamage(attacker, ratio);
        Managers.UI.ui_MonsterHpbar.ChangeMonsterHit(this);
        
     
    }
    protected override void Die()
    {
        base.Die();
        Managers.UI.ui_MonsterHpbar.OffMonsterHpbar();
        rigidbody.isKinematic = true;
        PlayerStatus playerStatus = Managers.Game.GetPlayer().GetComponent<PlayerStatus>();
        playerStatus.exp += 10;
        Managers.Resource.Destroy(gameObject, 2f);
        //ItemDrop();
    }

   



    public void ItemDrop()
    {
        for (int i = 0; i < 8; i++)
        {
            items.Add(ItemSet(
                (Define.Items)
                Random.Range(0, System.Enum.GetValues(typeof(Define.Items)).Length)));
        }
        for(int i =0; i <items.Count; i++)
        {
            if (items[i].GetComponent<ItemPickUp>().itemcode == 30001)
            {
                items[i].GetComponent<ItemPickUp>().count = (int)Random.Range(100f, 1000f);
            }
            items[i].transform.position = transform.position;
            items[i].GetComponent<Rigidbody>().
                AddForce(new Vector3(Random.Range(-5, 5), Random.Range(5, 10), Random.Range(-5, 5)), 
                ForceMode.Impulse);

        }
        

    }


    GameObject ItemSet(Define.Items type)
    { 
        string path = System.Enum.GetName(typeof(Define.Items), type);
        return Managers.Resource.Instantiate($"Item/{path}");
    }
}
