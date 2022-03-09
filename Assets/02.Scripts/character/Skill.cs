using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public void Invork_Skill(Image _skillBoard)
    {
        if (bSkill) return;
        StartCoroutine(Co_Skill(2f, _skillBoard));
    }

    bool bSkill;

    IEnumerator Co_Skill(float _duration, Image _skillBoard)
    {
        bSkill = true;
        float _speed = 1f / _duration;
        float _percent = 0;
        while(_percent < 1f)
        {
            Debug.Log("1");
            _percent += _speed * Time.deltaTime;
            _skillBoard.fillAmount = 1f - _percent;
            yield return null; 
        }
        _skillBoard.fillAmount = 0f;
        bSkill = false;
    }
}
