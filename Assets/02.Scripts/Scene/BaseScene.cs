using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SceneState
{
    Unknown,
    Login,
    Lobby,
    Select,
    Town

}
public abstract class BaseScene : MonoBehaviour
{
    public SceneState SceneType { get; protected set; } = SceneState.Unknown;

    private void Awake()
    {
        
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public abstract void Clear(); // 추상함수로 Clear내용을 자식씬에게 맡김
}
