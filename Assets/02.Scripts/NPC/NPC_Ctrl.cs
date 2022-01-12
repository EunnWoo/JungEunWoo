using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Ctrl : MonoBehaviour
{
    Transform playerTr;
    float damp = 6.0f;
    Quaternion rotate;
    Vector3 vec;
    void Start()
    {
        playerTr = GameObject.Find("Character 1").GetComponent<Transform>().GetChild(0).GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        vec = (playerTr.position - transform.position).normalized;        
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag=="Player"){
            rotate = Quaternion.LookRotation(vec);
            transform.rotation = Quaternion.Slerp(transform.rotation,rotate,Time.deltaTime*damp);
        }
    }
}
