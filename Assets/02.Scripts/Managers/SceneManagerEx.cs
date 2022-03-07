//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;
using Photon.Pun;
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
        Managers.Scene.Clear();
        PhotonNetwork.LoadLevel(GetSceneName(type));
   //     SceneManager.LoadScene(GetSceneName(type)); // SceneManager�� UnityEngine�� SceneManager
    }

    public void Clear()
    {
        CurrentScene.Clear();
    }
}
