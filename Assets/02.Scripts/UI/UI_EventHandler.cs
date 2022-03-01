using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


//�� ������Ʈ�� �ٿ��� ���� ���� �����ϴ�
public class UI_EventHandler : MonoBehaviour, /*IBeginDragHandler,*/ IDragHandler,IPointerClickHandler // �̺�Ʈ Ž�� ��ũ��Ʈ 
{
    public Action<PointerEventData> OnBeginDragHandler = null;
    public Action<PointerEventData> OnDragHandler = null;
    public Action<PointerEventData> OnClickHandler = null;
    //public void OnBeginDrag(PointerEventData eventData) //�巡�װ� ���۵ǰų� ������
    //{
    //    if (OnBeginDragHandler != null)
    //        OnBeginDragHandler.Invoke(eventData); // OnBeginDragHandler�� �������� eventData ����
    //}

    public void OnDrag(PointerEventData eventData) // �巡�� ��
    {
        //   transform.position = eventData.position; //�巡���ؼ� �����̱� ����
        if (OnDragHandler != null)
            OnDragHandler.Invoke(eventData); //  OnDragHandler �������� eventData ����
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (OnClickHandler != null)
            OnClickHandler.Invoke(eventData);
    }
}
