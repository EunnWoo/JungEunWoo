using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ScreenMenu : UI_Base
{
    enum Buttons
    {
        BackButton
    }
    enum GameObjects
    {
        FOVSlider
    }
    Slider fovSlider;

   
  
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));
        Bind<GameObject>(typeof(GameObjects));

        fovSlider = Get<GameObject>((int)GameObjects.FOVSlider).GetComponent<Slider>();
        fovSlider.onValueChanged.AddListener(Function_FOVSlider);

        GetButton((int)Buttons.BackButton).gameObject.AddUIEvent(BackClick);
    }


    private void Function_FOVSlider(float _value)
    {
        Camera.main.fieldOfView = _value;

    }

    private void BackClick(PointerEventData data)
    {
        gameObject.SetActive(false);
    }

}
