using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool portalOn { get; private set; }
    public static Portal instance;
    private void Start()
    {
        portalOn = false;
        instance = this;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag =="Player")
        {
            portalOn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            portalOn = false;
        }
    }
}
