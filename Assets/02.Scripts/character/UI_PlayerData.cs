using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PlayerData : MonoBehaviour
{
    #region sigleton
    public static UI_PlayerData ins;
    private void Awake()
    {
        ins = this;
    }
    #endregion
    public Text LevelText;
    public Image hpbar, mpbar, expbar;
    public Text hpText, mpText, expText;

    public void DisplayHP(float _hp, float _max)
    {
        float _v =  _hp / _max;
        hpbar.fillAmount = _v;
        hpText.text = string.Format("{0:0.0}",(_v * 100f)) + "%"; //소수점 한자리까지만 출력하는 함수

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
