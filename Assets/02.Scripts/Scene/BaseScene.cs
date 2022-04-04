using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum SceneState
{
    Unknown,
    Login,
    Tutorial,
    Select = 6000,
    Town =6001,
    Map1 = 6002,
    Map2 = 6003,
    Map3 = 6004,
    End

}

public abstract class BaseScene : MonoBehaviour
{
    #region SetUp
    public SceneState SceneType { get; protected set; } = SceneState.Unknown;
    public static BaseScene instance;
    


    private void Awake()
    {

        instance = this;
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
      

        if (Managers.UI.ui_MonsterHpbar != null)
        {
            Managers.UI.ui_MonsterHpbar.OffMonsterHpbar();
        }
    }
    #endregion
    protected virtual void SceneMove() { }

    public abstract void Clear(); // 추상함수로 Clear내용을 자식씬에게 맡김 -> 씬이 변하기전에 실행
}
