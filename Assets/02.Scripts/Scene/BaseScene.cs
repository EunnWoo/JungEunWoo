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
      

        if (Managers.UI.ui_MonsterHpbar != null)
        {
            Managers.UI.ui_MonsterHpbar.OffMonsterHpbar();
        }
    }

    protected virtual void SceneMove() { }

    public abstract void Clear(); // Ãß»óÇÔ¼ö·Î Clear³»¿ëÀ» ÀÚ½Ä¾À¿¡°Ô ¸Ã±è
}
