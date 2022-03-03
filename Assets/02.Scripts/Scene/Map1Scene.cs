using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1Scene : BaseScene
{
    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        SceneType = SceneState.Select;
        Managers.Game.GetPlayer().transform.position = Vector3.zero;

    }

    public override void Clear()
    {

    }
}

