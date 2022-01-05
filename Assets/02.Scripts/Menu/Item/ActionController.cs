using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionController : MonoBehaviour
{

    [SerializeField]
    private float checkRange; // 습득 가능한 최대 거리.

    private bool pickupActivated = false; // 습득 가능할 시 true.

    private RaycastHit hitInfo; // 충돌체 정보 저장.
    Camera camera;
    [SerializeField]
    float checkRadius = 0.3f;
    [SerializeField]
    Transform parentTrans;
    [SerializeField]
    Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0);

    // 아이템 레이어에만 반응하도록 레이어 마스크를 설정.
    [SerializeField]
    private LayerMask layerMask;

    // 필요한 컴포넌트.
    [SerializeField]
    private Text actionText;
    float dirDistance;

    private void Start()
    {
        camera = Camera.main;
        Cursor.visible = true;

        Vector3 _dir = parentTrans.position - transform.position;
        dirDistance = _dir.magnitude;
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
            InfoDisappear();
        }
    }

    void InfoDisappear()
    {
        pickupActivated = false;
        actionText.gameObject.SetActive(false);
    }

    private void TryAction()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            CheckItem();
            CanPickUp();
        }
    }

    private void CanPickUp()
    {
        if (pickupActivated)
        {
            if (hitInfo.transform != null)
            {
                Debug.Log(hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득했습니다");
                Destroy(hitInfo.transform.gameObject);
                InfoDisappear();
            }
        }
    }


    private void ItemInfoAppear()
    {
        pickupActivated = true;
        actionText.gameObject.SetActive(true);
        actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().item.itemName + " 획득 " + "<color=yellow>" + "(F)" + "</color>";
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
