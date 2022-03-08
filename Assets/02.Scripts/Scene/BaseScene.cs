using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
public enum SceneState
{
    Unknown,
    Login,
    Lobby,
    Select,
    Town,
    Map1,
    Map2,
    Map3,

}



public abstract class BaseScene : MonoBehaviourPun
{
    public SceneState SceneType { get; protected set; } = SceneState.Unknown;

    private void Awake()
    {
        
        Init();
        
    }
    private void Update()
    {
        SceneMove();
    }
    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    protected virtual void SceneMove() { }

    public abstract void Clear(); // Ãß»óÇÔ¼ö·Î Clear³»¿ëÀ» ÀÚ½Ä¾À¿¡°Ô ¸Ã±è
}
