using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusTest : MonoBehaviour
{
    //��������� ���� ��ũ��Ʈ�Դϴ� ���߿� �����ҰԿ�


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //1�������� ������ �޴´�
        {
            PlayerStatus.ins.TakeDamage(30); //������ 30����
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))//2�������� ��ų ����Ѱɿ����پ��
        {
            bool _b= PlayerStatus.ins.Skill(30);
            if(_b)
            {
                Debug.Log("��ų ������ ����ؼ� �۵�");
            }
        }
    }
}
