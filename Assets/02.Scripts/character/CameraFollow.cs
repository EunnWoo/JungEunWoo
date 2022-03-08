﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
   
    public Vector3 offset;

    [SerializeField]
    GameObject _player = null;
    public void SetPlayer(GameObject player) { _player = player; }
    private void Awake()
    {

        //var obj = FindObjectsOfType<Camera>();

        //if (obj.Length == 1)
        //{
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }
    void Start()
    {
        offset = new Vector3(0, 10, -12);
        DontDestroyOnLoad(this);



    }

    private void LateUpdate()
    {
        if(_player != null && _player.activeSelf)
        transform.position = _player.transform.position + offset;
    }
    //void Update()
    //{
        
        
        
        
    //}

}
