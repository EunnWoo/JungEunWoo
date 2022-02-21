using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAction : MonoBehaviour
{
    [SerializeField]
    DialogManager manager;
    public bool nearNPC = false;
    [SerializeField]
    GameObject scanNPC;
    void Start()
    {
        manager = GameObject.Find("GameManager").GetComponent<DialogManager>();
    }
    public void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Npc")
        {
            nearNPC = true;
            scanNPC = other.gameObject;
        }
        else return;
    }
    public void OnTriggerExit(Collider other) {
         
        nearNPC = false;
        scanNPC = null;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F) && scanNPC != null){
            
            manager.Action(scanNPC);
        }
    }
    
}
