using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectScene : BaseScene
{
 //   DialogManager dialogManager;
    GameObject player;
    [SerializeField]
    Portal[] portals;
    Camera camera;

    protected override void Init()
    {
        base.Init();
        SceneType = SceneState.Select;

        player = Managers.Game.Spawn("Player");

        camera = Managers.Resource.Instantiate("CameraReg").GetOrAddComponent<Camera>();
        camera.gameObject.GetOrAddComponent<CameraFollow>().SetPlayer(player);
      
        gameObject.GetOrAddComponent<CursorController>();
        portals = GameObject.FindObjectsOfType<Portal>();


        Managers.UI.ShowSceneUI<UI_PlayerData>();
        Managers.UI.ShowSceneUI<UI_Money>();
        



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