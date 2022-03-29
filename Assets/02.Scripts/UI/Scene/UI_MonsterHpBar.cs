using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_MonsterHpBar : UI_Scene
{

    enum GameObjects
    {
        Body,
        HpBarImage,
        NameText
    }

    Image hpBarImage;
    Text nameText;
    GameObject body;

    float time;
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));

        body = Get<GameObject>((int)GameObjects.Body);
        hpBarImage = Get<GameObject>((int)GameObjects.HpBarImage).GetComponent<Image>();
        nameText = Get<GameObject>((int)GameObjects.NameText).GetComponent<Text>();

        body.SetActive(false);
    }

    public void ChangeMonsterHit(Status status)
    {
        if (status.bDeath == true) return;
        Debug.Log("hpÄÑÁü È£Ãâ");
        time = 0;
        body.SetActive(true);
        nameText.text = status.name;
        hpBarImage.fillAmount = status.Hp / status.MAX_HP;

    }
    public void OffMonsterHpbar()
    {
        if (body.activeSelf == true)
        {
            Debug.Log("hp²¨Áü È£Ãâ");
            body.SetActive(false);
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        if(time >= 7)
        {
            body.SetActive(false);
        }
    }

}
