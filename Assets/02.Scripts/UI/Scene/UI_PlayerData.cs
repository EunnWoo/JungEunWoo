using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerData : UI_Scene
{
    #region sigleton
    public static UI_PlayerData ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion
    Text LevelText;
    Image hpbar, mpbar, expbar;
    Text hpText, mpText, expText;


    enum Texts
    {
        HPText,
        MPText,
        EXPText,
        LevelText
    }

    enum Images
    {
        HPBar,
        MPBar,
        EXPBar
    }

    public override void Init()
    {
        base.Init();
        DontDestroyOnLoad(this);

        hpText = GetText((int)Texts.HPText);
        mpText = GetText((int)Texts.MPText);
        expText = GetText((int)Texts.EXPText);
        LevelText = GetText((int)Texts.LevelText);

        hpbar = GetImage((int)Images.HPBar);
        mpbar = GetImage((int)Images.HPBar);
        expbar = GetImage((int)Images.HPBar);

    }

    public void DisplayHP(float _hp, float _max)
    {
        float _v = _hp / _max;
        hpbar.fillAmount = _v;
        hpText.text = string.Format("{0:0.0}", (_v * 100f)) + "%"; //소수점 한자리까지만 출력하는 함수

    }

    public void DisplayMP(float _mp, float _max)
    {
        float _v = _mp / _max;
        mpbar.fillAmount = _v;
        mpText.text = string.Format("{0:0.0}", (_v * 100f)) + "%"; //소수점 한자리까지만 출력하는 함수
    }

    public void DisplayEXP(float _exp, float _max)
    {
        float _v = _exp / _max;
        expbar.fillAmount = _v;
        expText.text = string.Format("{0:0.0}", (_v * 100f)) + "%"; //소수점 한자리까지만 출력하는 함수
    }
}
