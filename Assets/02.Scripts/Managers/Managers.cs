using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    public static Managers s_instance;
    public static Managers instance {get{ Init(); return s_instance; } }

    ResourceManager _resource = new ResourceManager();

    public static ResourceManager Resource { get { return instance._resource; } }

    private void Start()
    {
        Init();
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
            }
            DontDestroyOnLoad(managers);
            s_instance = managers.GetComponent<Managers>();
        }
        

    }
}
