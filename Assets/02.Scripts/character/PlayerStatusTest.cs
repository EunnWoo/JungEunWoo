using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusTest : MonoBehaviour
{
    [Header("�Ϸ�� �� ��ũ��Ʈ ���� ���ּŵ��˴ϴ�")]
    public int x;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //1�������� ������ �޴´�
        {
            PlayerStatus.ins.TakeDamage(30); //������ 5����
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))//2�������� �����پ��
        {
            bool _b= PlayerStatus.ins.Skill(30);
            if(_b)
            {
                Debug.Log("��ų ������ ����ؼ� �۵�");
            }
        }
    }
}
