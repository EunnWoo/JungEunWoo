using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map1Scene : BaseScene
{
    Portal[] portals;
    protected override void Init()
    {
        base.Init();
        SceneType = SceneState.Select;

        portals = GameObject.FindObjectsOfType<Portal>();

        gameObject.GetOrAddComponent<CursorController>();
        Managers.Game.GetPlayer().transform.position = new Vector3(-11.06f,3.08f,-12.63f);



    }

    protected override void SceneMove()
    {
        base.SceneMove();
        if (portals[0].portalOn && Input.GetKeyDown(KeyCode.K))
        {
            Managers.Scene.LoadScene(SceneState.Town);
        }
    }

    public override void Clear()
    {

    }
}

