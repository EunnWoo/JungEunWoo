using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurorialScene : BaseScene
{

    protected override void Init()
    {
        base.Init();
        SceneType = SceneState.Tutorial;

        gameObject.GetOrAddComponent<CursorController>();

        GameObject  player = Managers.Game.Spawn("Player");
        player.name = Managers.Game._name;
        Camera.main.gameObject.GetOrAddComponent<CameraFollow>().SetPlayer(player);

        Managers.UI.ShowSceneUI<UI_PlayerData>();
        Managers.UI.ShowSceneUI<UI_Money>();
        UI_Quest _quest = Managers.UI.ShowSceneUI<UI_Quest>();
        _quest.Init();
        _quest.questView.SetActive(false);





        ui_Inventory = Managers.UI.ShowSceneUI<UI_Inventory>();
        ui_MonsterHpbar = Managers.UI.ShowSceneUI<UI_MonsterHpBar>();
        //ui_Equipment = Managers.UI.ShowSceneUI<UI_Equipment>();
    }

    protected override void SceneMove()
    {
        base.SceneMove();
    }
    public override void Clear()
    {
        //ui_Equipment.Eqbody.SetActive(false);
        ui_Inventory.OpenInventory();
        ui_MonsterHpbar.OffMonsterHpbar();
    }
}
