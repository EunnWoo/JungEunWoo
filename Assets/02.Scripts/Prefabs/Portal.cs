using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool portalOn { get; private set; }

   

    private void Start()
    {
        portalOn = false;
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
