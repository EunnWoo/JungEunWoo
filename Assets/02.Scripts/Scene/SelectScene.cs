using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SelectScene : BaseScene
{
    DialogManager dialogManager;
    GameObject player;
    

    protected override void Init()
    {
        base.Init();
        SceneType = SceneState.Select;

        player = Managers.Game.Spawn("Player");
        Camera.main.gameObject.GetOrAddComponent<CameraFollow>().SetPlayer(player);

        dialogManager = GetComponent<DialogManager>();
        
        
    }


    private void Update()
    {

        if (Portal.instance.portalOn && Input.GetKeyDown(KeyCode.K))
        {
            Debug.Log("들어옴");
            Managers.Scene.LoadScene(SceneState.Town);
        }
    }
    public void JobExitButton()
    {
        dialogManager.dialogPanel.SetActive(false);
        dialogManager.isAction = false;
    }
    public void JobChoiceButton()
    {
        if (player.GetComponent<JopController>().jobstring != null)
        {

            Debug.Log("직업이 이미 있습니다");

        }
        else
        {
            player.GetComponent<JopController>().JobChoice();
        }
        dialogManager.dialogPanel.SetActive(false);
        dialogManager.isAction = false;

    }
    public override void Clear()
    {

    }


}
public static class Extension
{

    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null)
            component = go.AddComponent<T>();
        return component;
    }

}
