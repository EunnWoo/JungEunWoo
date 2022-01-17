using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField]
    private Transform camera;
    private DialogManager manager; 
    void Start() {
        manager = GameObject.Find("GameManager").GetComponent<DialogManager>();
    }
    void Update()
    {
        if(!manager.isAction){
            LookAround();
        }        
    }
    private void LookAround(){
       Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"),Input.GetAxis("Mouse Y"));
       Vector3 camAngle = camera.rotation.eulerAngles;
       float x = camAngle.x - mouseDelta.y;
       if(x<100f){
           x = Mathf.Clamp(x, -1f, 20f);
       }
       else{
           x = Mathf.Clamp(x, 335f, 361f);
       }
       camera.rotation = Quaternion.Euler(x, camAngle.y + mouseDelta.x, camAngle.z);
    }
    void LateUpdate() {
        
        
    }
}
