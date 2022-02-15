using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SceneState
{
    Unknown,
    Login,
    Lobby,
    Game,
    Select
}
public abstract class BaseScene : MonoBehaviour
{
    public SceneState SceneType { get; protected set; } = SceneState.Unknown;

    private void Awake()
    {
        Debug.Log("BaseScene ����");
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if (obj == null)
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
    }

    public abstract void Clear(); // �߻��Լ��� Clear������ �ڽľ����� �ñ�
}
