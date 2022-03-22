//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }



    string GetSceneName(SceneState type)
    {
        string name = System.Enum.GetName(typeof(SceneState), type); // C#�� Reflection. Scene enum�� 
        return name;
    }

    public void LoadScene(SceneState type)
    {
        Clear(); //���ʿ��� �޸� Ŭ����
        SceneManager.LoadScene(GetSceneName(type)); // SceneManager�� UnityEngine�� SceneManager
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
