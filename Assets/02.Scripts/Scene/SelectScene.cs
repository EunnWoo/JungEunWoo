using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SelectScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = SceneState.Select;

        gameObject.GetOrAddComponent<CursorController>();
     

        Managers.Sound.Play("BGM/Select",Define.Sound.BGM);

    }

    protected override void SceneMove()
    {
        base.SceneMove();


    }
    public override void Clear()
    {

    }


}