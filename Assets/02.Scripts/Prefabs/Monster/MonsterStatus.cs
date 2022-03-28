using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStatus : Status
{
    List<GameObject> items = new List<GameObject>();
    void Start()
    {
        Hp = MAX_HP;
        
    }
    


    public override void TakeDamage(Status attacker, float ratio)
    {
        base.TakeDamage(attacker, ratio);
        Managers.UI.ui_MonsterHpbar.ChangeMonsterHit(this);
        
     
    }
    protected override void Die()
    {
        base.Die();
        ItemDrop();
        Managers.UI.ui_MonsterHpbar.OffMonsterHpbar();
        QuestReporter questReporter = GetComponent<QuestReporter>();
        questReporter.Report();
        GetComponent<Collider>().isTrigger = true;
        Managers.Resource.Destroy(gameObject, 3f);
    }

   



    public void ItemDrop()
    {
        for (int i = 0; i < 8; i++)
        {
            items.Add(ItemSet(
                (Define.Items)
                Random.Range(0, System.Enum.GetValues(typeof(Define.Items)).Length)));
        }
        foreach(GameObject item in items)
        {
            if(item.GetComponent<ItemPickUp>().itemcode == 30001)
            {
                item.GetComponent<ItemPickUp>().count = (int)Random.Range(100f, 1000f);
            }
            item.transform.position = transform.position;
            item.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(-5,5), Random.Range(5, 10), Random.Range(-5, 5)),ForceMode.Impulse);
        }

    }


    GameObject ItemSet(Define.Items type)
    { 
        string path = System.Enum.GetName(typeof(Define.Items), type);
        return Managers.Resource.Instantiate($"Item/{path}");
    }
}
