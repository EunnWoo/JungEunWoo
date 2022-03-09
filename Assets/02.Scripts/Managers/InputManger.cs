using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Photon.Pun;
public class InputManager
{

    public Action KeyAction = null;

    public string hAxisName = "Horizontal";
    public string vAxisName = "Vertical";
    public string rollName = "Roll";
    public string fireName = "Fire1";
    public string runName = "Run";
    public string jumpName = "Jump";
    public string talkName = "Talk";
   
    public float hAxis { get; private set; }
    public float vAxis { get; private set; }
    public bool roll { get; private set; }
    public bool fire { get; private set; }
    public bool run { get; private set; }
    public bool jump { get; private set; }
    public bool talking { get; private set; }
   
    public bool keyInput { get; private set; }
    
    public void OnUpdate()
    {
        
        hAxis = Input.GetAxisRaw(hAxisName);
        vAxis = Input.GetAxisRaw(vAxisName);
        roll = Input.GetButton(rollName);
        fire = Input.GetButton(fireName);
        jump = Input.GetButton(jumpName);
        talking = Input.GetButtonDown(talkName);

        run = Input.GetButton(runName);

       
    }
}
