using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusTest : MonoBehaviour
{
    //실험용으로 만든 스크립트입니다 나중에 삭제할게요


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) //1번누르면 데미지 받는다
        {
            PlayerStatus.ins.TakeDamage(30); //데미지 30받음
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))//2번누르면 스킬 사용한걸엠피줄어듬
        {
            bool _b= PlayerStatus.ins.Skill(30);
            if(_b)
            {
                Debug.Log("스킬 물약이 충분해서 작동");
            }
        }
    }
}
