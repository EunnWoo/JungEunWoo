using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum MouseEvent
{
    Press,
    PointerDown,
    PointerUp,
    Click,
}
public class MouseInputManager
{   
    
    public Action<MouseEvent> MouseAction = null;

    bool _pressed = false;
    float _pressedTime = 0;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0)) //PointerDown ->Press
            {
                if (!_pressed) // �ѹ��� ���������� �����µ� �������� ���°Ŷ��
                {
                    MouseAction.Invoke(MouseEvent.PointerDown); //��������Ʈ�� �̺�Ʈȣ��
                    _pressedTime = Time.time; // �����ð� ��� üũ
                }
                MouseAction.Invoke(MouseEvent.Press); 
                _pressed = true;
            }
            else
            {
                if (_pressed) //Click ->PointerUp(�� ���� �����ִٰ� ���� ��)
                {
                    if (Time.time < _pressedTime + 0.2f) //0.2���̳��� ���� ��
                        MouseAction.Invoke(MouseEvent.Click);
                    MouseAction.Invoke(MouseEvent.PointerUp);
                }
                _pressed = false; // pressed�� �ʱ�ȭ
                _pressedTime = 0; //�� ���� �ʱ�ȭ
            }
        }
    }

    public void Clear()
    {
        MouseAction = null;
    }
}
