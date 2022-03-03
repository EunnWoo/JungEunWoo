using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownScene : BaseScene
{
    [SerializeField]
    Portal[] portals;


    protected override void Init()
    {
        gameObject.GetOrAddComponent<CursorController>();
        portals = GameObject.FindObjectsOfType<Portal>();
        Managers.Game.GetPlayer().transform.position =  Vector3.zero;
    }


    private void Update()
    {
        if (portals[0].portalOn && Input.GetKeyDown(KeyCode.K))
        {
            Managers.Scene.LoadScene(SceneState.Map2);
        }
        else if (portals[1].portalOn && Input.GetKeyDown(KeyCode.K))
        {
            Managers.Scene.LoadScene(SceneState.Map1);
        }
        else if (portals[2].portalOn && Input.GetKeyDown(KeyCode.K))
        {
            Managers.Scene.LoadScene(SceneState.Map3);
        }
    }

    public override void Clear()
    {

    }
}
