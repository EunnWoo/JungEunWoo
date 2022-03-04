using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ItemInfoText : UI_Scene
{
    //enum Texts
    //{
    //    ShowText
    //}
    //public override void Init()
    //{
    //    base.Init();

    //    GetText((int)Texts.ShowText).text = "";


    //}
    public override void Init()
    {
        DontDestroyOnLoad(this);
    }
}
