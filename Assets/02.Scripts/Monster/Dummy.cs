using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    [SerializeField]
    Collider collider;

    private void Start()
    {
        collider = GetComponent<Collider>();
    }
    public  void TakeDamage(int damage)
    {
        //UI_Damage ui_Damage = Managers.UI.ShowPopupUI<UI_Damage>();
        //ui_Damage.Init();
        ////GameObject hudText = Instantiate(hudDamageText); // ������ �ؽ�Ʈ ������Ʈ
       
        //ui_Damage.transform.position = collider.bounds.max;
        //ui_Damage.damage = damage;
        
        
    }
}
