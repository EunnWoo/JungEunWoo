using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{

    [SerializeField]
    private float checkRange; // ���� ������ �ִ� �Ÿ�.

    private bool pickupActivated = false; // ���� ������ �� true.

    private RaycastHit hitInfo; // �浹ü ���� ����.

    Transform parentTrans;
    [SerializeField]
    Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0);

    GameObject target;

    // �ʿ��� ������Ʈ.
    [SerializeField]
    private Text actionText;
    Text setText;

   // int _mask = (int)Layer.Item;
   public LayerMask _mask = 1<< (int)Layer.Item;


    private void Start()
    {
        // actionText = GameObject.Find("Canvas").transform.Find("ShowText").GetComponent<Text>();
        Managers.Mouse.MouseAction -= OnMouseEvent;
        Managers.Mouse.MouseAction += OnMouseEvent;


    }
    void Update()
    {
        TryAction();
    }
    void OnMouseEvent(Define.MouseEvent evt)
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool raycastHit = Physics.Raycast(ray, out hit, 100.0f, _mask);

        if (raycastHit)
        {

            switch (evt)
            {

                case Define.MouseEvent.PointerDown:
                    target = hit.collider.gameObject;
                    break;
            }

        }
    }

    //private void ItemInfoAppear() // ���̾� ����ũ�� ������� �ؽ�Ʈ ȣ��
    //{
        
    //    actionText.gameObject.SetActive(true);
    //    actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().itemData.itemName + " ȹ�� " + "<color=yellow>" + "(F)" + "</color>";
    //}
    //void IteminfoDisappear() // ������ �� ������ false
    //{
       
    //    actionText.gameObject.SetActive(false);
    //}

    private void TryAction()
    {
        //zŬ���� ����ǰ�
        if (target != null)
        {
            if (hitInfo.transform != null)
            {
                //ȹ���� �����ۿ��� ItemPickUp�̶�� ������Ʈ
                //<ItemPickUp>().ItemData(�������� + ��������)
                //�������� : Itemcode, Itemname���
                ItemPickUp _Pick = hitInfo.transform.GetComponent<ItemPickUp>();
                Debug.Log(_Pick.itemData.itemName + " ȹ���߽��ϴ�");
              //  IteminfoDisappear(); //�޼���â �������

                //�κ��丮�� �־��ֱ�
                UI_Inventory.ins.AddItemData(_Pick.itemData);
                _Pick.ClearDestroy();//������Ʈ �ֿ�� �ʵ忡 �ֿ�������� ��������ϱ�
                target = null;
            }
        }
    }

}
