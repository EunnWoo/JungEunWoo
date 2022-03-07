using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    protected override void Init()  // ��� ���� Awake() �ȿ��� �����. "LoginScene"�� �ʱ�ȭ
    {
        
        base.Init();
        SceneType = SceneState.Login; // ??LoginScene�� �� ������ LoginScene
        Debug.Log("LoginScene ����");

        Managers.UI.ShowSceneUI<UI_LoginCanvas>();

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {

            Managers.Scene.LoadScene(SceneState.Select);
        }
    }

    public override void Clear()
    {
        Debug.Log("LoginScene Clear!");
    }

}