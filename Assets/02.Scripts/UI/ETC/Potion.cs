using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ePotionType { HpPotion = 10001, MpPotion = 10002 };

public class Potion : MonoBehaviour
{
    public ePotionType potionType;
    bool bPotion;
    public void Invork_Potion(Image _potionBoard)
    {
        if (bPotion) return;
        {
            //인벤토리에 물약이 있는지 검사하고
            //있으면 true 갯수를 감소기킴
            //없으면 false 갯수를 감소시키지않음
            int _itemcode = (int)potionType;
            ItemData _itemData =  UI_Inventory.ins.CheckAndEatHP(_itemcode);
            if(_itemData != null)
            {
                StartCoroutine(PotionDelay(1f, _potionBoard)); //딜레이 1초(쿨타임)
            }
            else
            {
                Debug.Log("물약없음");
            }
        }
    }

    

    IEnumerator PotionDelay(float _duration, Image _skillBoard) //포션 쿨타임
    {
        bPotion = true;
        float _speed = 1f / _duration;
        float _percent = 0;
        while (_percent < 1f)
        {
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
