using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers s_instance;
    public static Managers instance { get { Init(); return s_instance; } }


    #region managers
    GameManager _game = new GameManager();

    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    ObjPoolManager _pool = new ObjPoolManager();
    MouseInputManager _mouse = new MouseInputManager();
    InputManager _input = new InputManager();
    TalkManager _talk = new TalkManager();

    public static GameManager Game { get { return instance._game; } }

    public static ResourceManager Resource { get { return instance._resource; } }
    public static SceneManagerEx Scene { get { return instance._scene; } }
    public static ObjPoolManager Pool {  get { return instance._pool; } }
    public static MouseInputManager Mouse { get { return instance._mouse; } }
    public static InputManager Input { get { return instance._input; } }
    public static TalkManager talk { get { return instance._talk; } }
    #endregion

    

    private void Start()
    {
        Init();
       
    }
    private void Update()
    {
        _mouse.OnUpdate();
        _input.OnUpdate();
    }

    static void Init()
    {

        if (s_instance == null)
        {
            GameObject managers = GameObject.Find("@Managers");
            if (managers == null)
            {
                managers = new GameObject { name = "@Managers" };
                managers.AddComponent<Managers>();


            }

            DontDestroyOnLoad(managers);
            s_instance = managers.GetComponent<Managers>();

            s_instance._pool.Init();
            s_instance._talk.Init();
            s_instance._game.Init();

        }


    }
    public static void Clear()
    {
        Mouse.Clear();
    }
}
