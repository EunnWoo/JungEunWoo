using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SelectScene : BaseScene
{
    // Start is called before the first frame update
    protected override void Init()
    {
        base.Init();
        SceneType = SceneState.Select;

    }

    public override void Clear()
    {

    }
}
