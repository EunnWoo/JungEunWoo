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
            //�κ��丮�� ������ �ִ��� �˻��ϰ�
            //������ true ������ ���ұ�Ŵ
            //������ false ������ ���ҽ�Ű������
            int _itemcode = (int)potionType;
            ItemData _itemData =  UI_Inventory.ins.CheckAndEatHP(_itemcode);
            if(_itemData != null)
            {
                StartCoroutine(PotionDelay(1f, _potionBoard)); //������ 1��(��Ÿ��)
            }
            else
            {
                UI_ErrorText.Instance.SetErrorText(Define.Error.NonePotion);
            }
        }
    }

    

    IEnumerator PotionDelay(float _duration, Image _skillBoard) //���� ��Ÿ��
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
