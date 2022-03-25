using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text.RegularExpressions;

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
   
    Button loginButton; // ·ë Á¢¼Ó ¹öÆ°
    Button exitButton;

    InputField idInput;
    public string nickName { get; private set; }
    GameObject backGround;
    // °ÔÀÓ ½ÇÇà°ú µ¿½Ã¿¡ ¸¶½ºÅÍ ¼­¹ö Á¢¼Ó ½Ãµµ
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));

        Bind<GameObject>(typeof(GameObjects));

  

        loginButton = GetButton((int)Buttons.LoginButton);

        exitButton = GetButton((int)Buttons.ExitButton);

        idInput = Get<GameObject>((int)GameObjects.IDInputField).GetComponent<InputField>();
        idInput.characterLimit = 10; // ±ÛÀÚ¼ö Á¦ÇÑ



        #region buttonevent
        loginButton.gameObject.AddUIEvent(ClickLogin); // ·Î±×ÀÎ Å¬¸¯½Ã

        exitButton.gameObject.AddUIEvent(Exit);



        #endregion

    }


    public void ClickLogin(PointerEventData data)
    {
        if(Regex.IsMatch(idInput.text, @"[^a-zA-Z0-9°¡-ÆR]"))
        {
            UI_Message ui_Message =  Managers.UI.ShowPopupUI<UI_Message>();
            ui_Message.Init();
            ui_Message.ShowMessage("¿¡·¯", "ÀÌ¸§¿¡ Æ¯¼ö¹®ÀÚ´Â ¾µ ¼ö ¾ø½À´Ï´Ù");
            ui_Message.okButton.gameObject.AddUIEvent(ui_Message.Cancel);
            return;
        }
        Managers.Game.SetName(idInput.text);
        Managers.Scene.LoadScene(SceneState.Tutorial);
        Destroy(gameObject);
    }


    public void  Exit(PointerEventData data)
    {
        Application.Quit();
    }

}
