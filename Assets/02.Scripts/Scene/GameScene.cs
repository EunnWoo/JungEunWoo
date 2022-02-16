using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : BaseScene
{
    protected override void Init()
    {
        base.Init();
        SceneType = SceneState.Game;
        gameObject.AddComponent<CursorController>();

    }

    public override void Clear()
    {
        
    }


}

