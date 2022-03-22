//using System.Collections;
//using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx
{
    public BaseScene CurrentScene { get { return GameObject.FindObjectOfType<BaseScene>(); } }

    public string loadSceneName;
   
    string GetSceneName(SceneState type)
    {
        string name = System.Enum.GetName(typeof(SceneState), type); // C#�� Reflection. Scene enum�� 
        return name;
    }

    public void LoadScene(SceneState type)
    {
        Clear(); //���ʿ��� �޸� Ŭ����

        loadSceneName = GetSceneName(type);
        SceneManager.LoadScene("Loading");
     //   ui_Loading.StartCoroutine(ui_Loading.LoadSceneProcess());
     // loadSceneName = GetSceneName(type);
     //  StartCorutine(LoadSceneProcess());

        // SceneManager.LoadScene(); // SceneManager�� UnityEngine�� SceneManager
    }



    //private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    //{
    //    if(arg0.name == loadSceneName)
    //    {
    //        ui_Loading.StartCoroutine(ui_Loading.Fade(false));
    //        SceneManager.sceneLoaded -= OnSceneLoaded;
    //    }
    //}




    public void Clear()
    {
        CurrentScene.Clear();
    }
}
