using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickUp : MonoBehaviour
{
    public Item item;
    public Item GetItem()
    {
        return item;
    }
    public float TurnSpeed = 90f;
    public Vector3 axis = Vector3.up;
    private void Update()
    {
        transform.Rotate(axis * TurnSpeed * Time.deltaTime);
    }
}
