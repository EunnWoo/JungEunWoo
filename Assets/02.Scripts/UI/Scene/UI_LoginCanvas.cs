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
   
    Button loginButton; // 룸 접속 버튼
    Button exitButton;

    InputField idInput;
    public string nickName { get; private set; }
    GameObject backGround;
    // 게임 실행과 동시에 마스터 서버 접속 시도
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        Bind<GameObject>(typeof(GameObjects));

  

        loginButton = GetButton((int)Buttons.LoginButton);

        exitButton = GetButton((int)Buttons.ExitButton);

        idInput = Get<GameObject>((int)GameObjects.IDInputField).GetComponent<InputField>();


       
        
        #region buttonevent
        loginButton.gameObject.AddUIEvent(ClickLogin); // 로그인 클릭시

        exitButton.gameObject.AddUIEvent(Exit);



        #endregion

    }


    public void ClickLogin(PointerEventData data)
    {
        Managers.Game.SetName(idInput.text);
        Managers.Scene.LoadScene(SceneState.Tutorial);
        Destroy(gameObject);
    }
    public void  Exit(PointerEventData data)
    {
        Application.Quit();
    }

}
