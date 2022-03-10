using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_LoginCanvas : UI_Scene 
{
    enum Buttons
    {
        LoginButton, 
        ExitButton
    }


    enum GameObjects
    {
        IDInputField,
        BackGround
    }
   
    Button loginButton; // �� ���� ��ư
    Button exitButton;

    InputField idInput;
    public string nickName { get; private set; }
    GameObject backGround;
    // ���� ����� ���ÿ� ������ ���� ���� �õ�
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        Bind<GameObject>(typeof(GameObjects));

  

        loginButton = GetButton((int)Buttons.LoginButton);

        exitButton = GetButton((int)Buttons.ExitButton);

        idInput = Get<GameObject>((int)GameObjects.IDInputField).GetComponent<InputField>();


       
        
        #region buttonevent
        loginButton.gameObject.AddUIEvent(ClickLogin); // �α��� Ŭ����

        exitButton.gameObject.AddUIEvent(Exit);



        #endregion

    }


    public void ClickLogin(PointerEventData data)
    {
        Managers.Game.SetName(idInput.text);
        Managers.Scene.LoadScene(SceneState.Select);
    }
    public void  Exit(PointerEventData data)
    {
        Application.Quit();
    }

}
