using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Help : UI_Scene
{
    enum GameObjects
    {
        body
    }
    GameObject body;
    public override void Init()
    {
        base.Init();
        Bind<GameObject>(typeof(GameObjects));

        body = Get<GameObject>((int)GameObjects.body);


    }




    public void OpenHelp()
    {
        if (!body.activeSelf)
        {
            body.SetActive(true);
        }
        else
        {
            body.SetActive(false);
        }

    }
}
