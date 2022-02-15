using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    private GameObject prefab;
    void Start()
    {
        prefab = Managers.Resource.Instantiate("quiver");
        Managers.Resource.Destroy(prefab);
    }

}
