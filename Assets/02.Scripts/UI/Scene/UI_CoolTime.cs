using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_CoolTime : UI_Scene
{
    enum Images
    {
        BackGroundSkillImage,
        BackGroundNormalImage
    }

    Image backGroundSkillImage, backGroundNormalImage;

    public override void Init()
    {
        base.Init();
        Bind<Image>(typeof(Images));

        backGroundNormalImage = GetImage((int)Images.BackGroundNormalImage);
        backGroundSkillImage = GetImage((int)Images.BackGroundSkillImage);
    }

    public void SetCoolTimeImage(float normalrate, float skillrate, float normalDel, float SkillDel)
    {
        backGroundNormalImage.fillAmount = 1 - (normalDel / normalrate);
        backGroundSkillImage.fillAmount = 1- (SkillDel / skillrate);
    }

}
