using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField]
    private BoxCollider box;

    void Start()
    {
        box = GetComponent<BoxCollider>();
    }

   
    void Update()
    {
       
    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("�浹");
        other.gameObject.transform.position = new Vector3(0, 1, 0);
       // other.GetComponentInChildren<Transform>().position = new Vector3(0, 1, 0);
    }
}