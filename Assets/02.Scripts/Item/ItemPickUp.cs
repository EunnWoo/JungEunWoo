using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public int itemcode;
    public int count = 1;
    public ItemData item;
    public ItemData GetItem()
    {
        return item;
    }
    private void Start()
    {
        item = new ItemData(itemcode,count);
    }


    public float TurnSpeed = 90f;
    public Vector3 axis = Vector3.up;
    private void Update()
    {
        transform.Rotate(axis * TurnSpeed * Time.deltaTime);
    }
}
