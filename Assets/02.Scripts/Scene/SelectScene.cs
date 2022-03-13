using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SelectScene : BaseScene
{
 //   DialogManager dialogManager;
    GameObject player;
    [SerializeField]
    Portal[] portals;


    protected override void Init()
    {
        base.Init();
        SceneType = SceneState.Select;

        player = Managers.Game.Spawn("Player");
        player.name = Managers.Game._name;
        Camera.main.gameObject.GetOrAddComponent<CameraFollow>().SetPlayer(player);
        gameObject.GetOrAddComponent<CursorController>();
        portals = GameObject.FindObjectsOfType<Portal>();


        Managers.UI.ShowSceneUI<UI_PlayerData>();
        Managers.UI.ShowSceneUI<UI_Money>();
        Managers.UI.ShowSceneUI<UI_Inventory>().body.SetActive(false);



    }

    protected override void SceneMove()
    {
        base.SceneMove();


    }
    public override void Clear()
    {

    }


}