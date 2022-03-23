using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ui_Cursor : MonoBehaviour
{
    Camera camera;
    public GameObject body;
    public RectTransform rectTrans;
    public Image cursor;
    void Start()
    {
        camera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        if(camera != null)
        {
            Vector3 _viewPort = camera.ScreenToViewportPoint(Input.mousePosition);
            cursor.rectTransform.anchoredPosition = new Vector2(rectTrans.rect.width * _viewPort.x, rectTrans.rect.height * _viewPort.y);
        }
    }
}
