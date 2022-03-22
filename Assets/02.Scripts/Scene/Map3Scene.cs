using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map3Scene : BaseScene
{
    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        SceneType = SceneState.Map3;
        gameObject.GetOrAddComponent<CursorController>();

        Managers.Game.GetPlayer().transform.position = new Vector3(-4f, 1.06f, -22f);
        Managers.Sound.Play("BGM/Map3", Define.Sound.BGM);

    }

    public override void Clear()
    {

    }
}