using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_CoolTime : UI_Scene
{
    #region sigleton
    bool bInit;
    public static UI_CoolTime ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion


    enum Images
    {
        BackGroundSkillImage,
        BackGroundNormalImage,
        NormalImage,
        SkillImage
    }
   
    Image backGroundSkillImage, backGroundNormalImage;
    Image normalImage, skillImage;

    public override void Init()
    {
        if(bInit)return;
        bInit = true;
        base.Init();
        Bind<Image>(typeof(Images));

        backGroundNormalImage = GetImage((int)Images.BackGroundNormalImage);
        backGroundSkillImage = GetImage((int)Images.BackGroundSkillImage);
        normalImage = GetImage((int)Images.NormalImage);
        skillImage = GetImage((int)Images.SkillImage);
    }

    public void SetJobSkillImage(Sprite _s1, Sprite _s2)
    {
        
        if (backGroundNormalImage == null) Init(); //PlayerAttack스크립트에있는 onenable()이 UI_CoolTime스크립트에있는 Init()보다 먼저 실행되어서
                                                   // 초기화가 안되는 현상이있어서 강제로 먼저 호출해서 1회 초기화 하는것
        Debug.Log("바뀜");
        backGroundNormalImage.sprite= _s1;
        normalImage.sprite = _s1;

        backGroundSkillImage.sprite =_s2;
        skillImage.sprite = _s2;
    }

    public void SetCoolTimeImage(float normalrate, float skillrate, float normalDel, float SkillDel)
    {
        backGroundNormalImage.fillAmount = 1 - (normalDel / normalrate);
        backGroundSkillImage.fillAmount = 1- (SkillDel / skillrate);
    }

}
