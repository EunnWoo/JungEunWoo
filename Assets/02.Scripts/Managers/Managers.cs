using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers s_instance;
    public static Managers instance {get{ Init(); return s_instance; } }

    ResourceManager _resource = new ResourceManager();
    SceneManagerEx _scene = new SceneManagerEx();
    ObjPoolManager _pool = new ObjPoolManager();
    TalkManager _talk = new TalkManager();
    DialogManager _Dialog = new DialogManager();
    public static ResourceManager Resource { get { return instance._resource; } }
    public static SceneManagerEx Scene { get { return instance._scene; } }
    public static ObjPoolManager pool { get { return instance._pool; } }
    public static TalkManager talk { get { return instance._talk; } }
    public static DialogManager dialog { get { return instance._Dialog; } }
    private void Start()
    {
        Init();
        pool.Generate();

    }


    static void Init()
    {
       
        if(s_instance == null)
        {
            GameObject managers = GameObject.Find("@Managers");
            if(managers == null)
            {
                managers = new GameObject { name = "@Managers" };
                managers.AddComponent<Managers>();
               // managers.AddComponent<ObjPoolManager>();


            }

            DontDestroyOnLoad(managers);
            s_instance = managers.GetComponent<Managers>();
        }
        

    }
}
