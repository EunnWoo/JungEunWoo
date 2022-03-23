using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Menu : UI_Scene
{
    enum Buttons
    {
        ResumeButton,
        SoundButton,
        ScreenButton,
        ExitButton
    }
    enum GameObjects
    {
        Body
    }

    GameObject body;
    public override void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        body = Get<GameObject>((int)GameObjects.Body);

        GetButton((int)Buttons.ResumeButton).gameObject.AddUIEvent(ResumeButton);
        GetButton((int)Buttons.SoundButton).gameObject.AddUIEvent(SoundButton);
        GetButton((int)Buttons.ScreenButton).gameObject.AddUIEvent(ScreenButton);
        GetButton((int)Buttons.ExitButton).gameObject.AddUIEvent(ExitButton);





        body.SetActive(false);
    }



    public void ResumeButton(PointerEventData data)
    {

    }

    public void SoundButton(PointerEventData data)
    {

    }
    public void ScreenButton(PointerEventData data)
    {


    }
    public void ExitButton(PointerEventData data)
    {

    }
}
