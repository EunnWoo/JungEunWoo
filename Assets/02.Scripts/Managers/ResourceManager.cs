using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager
{
    public T Load<T>(string path) where T : Object   // T�� GameObject�� �����̶� where T : Object�� �ȴ�.
    {
        return Resources.Load<T>(path);
    }

    public GameObject Instantiate(string path, Transform parent = null) // parent -> ������ null �ڵ� ����
    {
        GameObject prefab = Load<GameObject>($"Prefabs/{path}");   // $ 

        if(prefab ==null)
        {
            Debug.Log($"{path}�� �����ϴ�");
            return null;
        }
        return Object.Instantiate(prefab, parent);  // ������Ʈ �� ���̸� ������� �Լ� �����ȴ�.
    }
    public RuntimeAnimatorController Instantiate_Ani(string path, Transform parent = null)
    {
        RuntimeAnimatorController controller = Load<RuntimeAnimatorController>($"Animator/{path}");


        if (controller == null)
        {
            Debug.Log($"{path}�� �����ϴ�");
            return null;
        }
        return Object.Instantiate(controller, parent);  // ������Ʈ �� ���̸� ������� �Լ� �����ȴ�.
    }
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;
        Object.Destroy(go,3f);
    }
}
