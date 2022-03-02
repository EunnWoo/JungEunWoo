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
    Camera camera;
    [SerializeField]
    float checkRadius = 0.3f;
    [SerializeField]
    Transform parentTrans;
    [SerializeField]
    Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0);

    // ������ ���̾�� �����ϵ��� ���̾� ����ũ�� ����.
    [SerializeField]
    private LayerMask layerMask;

    // �ʿ��� ������Ʈ.
    [SerializeField]
    private Text actionText;
    float dirDistance;

    Text setText;

    private void Start()
    {
        camera = Camera.main;
        Vector3 _dir = parentTrans.position - transform.position;
        dirDistance = _dir.magnitude;
       // actionText = GameObject.Find("Canvas").transform.Find("ShowText").GetComponent<Text>();
        
    }
    void Update()
    {
        CheckItem();
        TryAction();
    }
    private void CheckItem()
    {
        Ray _ray = camera.ViewportPointToRay(screenCenter);
        if (Physics.SphereCast(_ray, checkRadius, out hitInfo, (checkRange + dirDistance), layerMask))
        {
            ItemInfoAppear();
           
        }

        else
        {
            IteminfoDisappear();
        }
    }

    private void ItemInfoAppear() // ���̾� ����ũ�� ������� �ؽ�Ʈ ȣ��
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().itemData.itemName + " ȹ�� " + "<color=yellow>" + "(F)" + "</color>";
    }
    void IteminfoDisappear() // ������ �� ������ false
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void TryAction()
    {
        //�����۰� �����Ÿ� �̻� �����ϰ��ִ� ���¿��� fŰ�� ������ �������� ����
        if (pickupActivated && Input.GetKeyDown(KeyCode.F))
        {
            if (hitInfo.transform != null)
            {
                //ȹ���� �����ۿ��� ItemPickUp�̶�� ������Ʈ
                //<ItemPickUp>().ItemData(�������� + ��������)
                //�������� : Itemcode, Itemname���
                ItemPickUp _Pick = hitInfo.transform.GetComponent<ItemPickUp>();
                Debug.Log(_Pick.itemData.itemName + " ȹ���߽��ϴ�");
                IteminfoDisappear(); //�޼���â �������

                //�κ��丮�� �־��ֱ�
                UI_Inventory.ins.AddItemData(_Pick.itemData);
                _Pick.ClearDestroy();//������Ʈ �ֿ�� �ʵ忡 �ֿ�������� ��������ϱ�
            }
        }
    }

  
    private void OnDrawGizmos()
    {
        Vector3 _dir = parentTrans.position - transform.position;
        float _dirDistance = _dir.magnitude;
        Ray _ray = Camera.main.ViewportPointToRay(screenCenter);
        Vector3 _p0 = _ray.origin;
        Vector3 _p1 = _ray.origin + _ray.direction * (checkRange + _dirDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_p0, checkRadius);
        Gizmos.DrawWireSphere (_p1, checkRadius);
        Gizmos.DrawLine(_p0, _p1);
    }
}
