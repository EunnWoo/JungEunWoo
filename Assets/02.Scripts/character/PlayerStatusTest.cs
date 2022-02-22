using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusTest : MonoBehaviour
{
    [Header("완료시 이 스크립트 삭제 해주셔도됩니다")]
    public int x;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //1번누르면 데미지 받는다
        {
            PlayerStatus.ins.TakeDamage(30); //데미지 5받음
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))//2번누르면 엠피줄어듬
        {
            bool _b= PlayerStatus.ins.Skill(30);
            if(_b)
            {
                Debug.Log("스킬 물약이 충분해서 작동");
            }
        }
    }
}
