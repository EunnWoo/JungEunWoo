//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    string GetSceneName(SceneState type)
    {
        string name = System.Enum.GetName(typeof(SceneState), type); // C#의 Reflection. Scene enum의 
        return name;
    }

    public void LoadScene(SceneState type)
    {
        Managers.Scene.Clear();
   
        SceneManager.LoadScene(GetSceneName(type)); // SceneManager는 UnityEngine의 SceneManager
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
