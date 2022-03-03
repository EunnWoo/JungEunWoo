using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectScene : BaseScene
{
 //   DialogManager dialogManager;
    GameObject player;
    

    protected override void Init()
    {
        base.Init();
        SceneType = SceneState.Select;

        player = Managers.Game.Spawn("Player");
        Camera.main.gameObject.GetOrAddComponent<CameraFollow>().SetPlayer(player);
        gameObject.GetOrAddComponent<CursorController>();
        Managers.UI.ShowSceneUI<UI_PlayerData>();
        Managers.UI.ShowSceneUI<UI_Money>();
        



    }


    private void Update()
    {
        if (Portal.instance.portalOn && Input.GetKeyDown(KeyCode.K))
        {
            Managers.Scene.LoadScene(SceneState.Town);
        }
    }
   
    public override void Clear()
    {

    }


}