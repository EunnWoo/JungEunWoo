using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Potion : MonoBehaviour
{
    public void Invork_Potion(Image _potionBoard)
    {
        if (bPotion) return;
        StartCoroutine(PotionDelay(1f, _potionBoard)); //µô·¹ÀÌ 1ÃÊ(ÄðÅ¸ÀÓ)
    }

    bool bPotion;

    IEnumerator PotionDelay(float _duration, Image _skillBoard) //Æ÷¼Ç ÄðÅ¸ÀÓ
    {
        bPotion = true;
        float _speed = 1f / _duration;
        float _percent = 0;
        while (_percent < 1f)
        {
            Debug.Log("1");
            _percent += _speed * Time.deltaTime;
            _skillBoard.fillAmount = 1f - _percent;
            yield return null;
        }
        _skillBoard.fillAmount = 0f;
        bPotion = false;
    }
    public void TakePotion()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // PlayerStatus.ins.SetHPMP<>;
        }
    }
}
