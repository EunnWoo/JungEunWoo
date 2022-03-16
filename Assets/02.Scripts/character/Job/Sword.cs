using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : PlayerAttack
{


    private float charge;
    [SerializeField]
    GameObject[] weapons; //
    
    private void Awake()
    {
        attackRate = 0.3f;
        skillRate = 10f;
        range = 2f;
        weapons = GameObject.FindGameObjectsWithTag("Sword");
        
    }

    protected override IEnumerator Use()
    {

        animator.SetTrigger("Attack");
        isAttack = false;
        attackDelay = 0;
        yield return null;
    }

    protected override IEnumerator Skill()
    {
        animator.SetTrigger("IsSkill");
        UI_SkillTime ui_SkillTime = Managers.UI.ShowPopupUI<UI_SkillTime>();
        ui_SkillTime.Init();
        while (true)
        {
            charge += Time.deltaTime;
            ui_SkillTime.SetImage(charge);
            if (Input.GetMouseButtonUp(1) || charge >=5f)
            {
                Managers.UI.ClosePopupUI(ui_SkillTime);
                animator.SetTrigger("Fire");

                skillDelay = 0;
                charge = 0;
                isAttack = false;

                break;

            }
            yield return null;
        }
        
    }
    protected override void OnHitEvent()
    {
        Status playerstatus = Managers.Game.GetPlayer().GetComponent<Status>();
        Vector3 vec = transform.localPosition + transform.forward;
        Collider[] hit = Physics.OverlapSphere(vec, 1.2f, 1 << (int)Layer.Monster);
        for (int i = 0; i < hit.Length; i++)
        {

            Status status = hit[i].GetComponent<Status>();
            status.TakeDamage(playerstatus);
        }


    }
    public void SkillEvent() // ½ºÅ³
    {
        Status playerstatus = Managers.Game.GetPlayer().GetComponent<Status>();
        Collider[] hit = Physics.OverlapSphere(transform.position, 2.2f, 1 << (int)Layer.Monster);


        for (int i = 0; i < hit.Length; i++)
        {
            Status status = hit[i].GetComponent<Status>();
            status.TakeDamage(playerstatus);
        }
    }

}
