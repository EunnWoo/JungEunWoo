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

    Transform parentTrans;
    [SerializeField]
    Vector3 screenCenter = new Vector3(0.5f, 0.5f, 0);

    GameObject target;

    // 필요한 컴포넌트.
    [SerializeField]
    private Text actionText;
    Text setText;

    int _mask = (int)Layer.Item;


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

    //private void ItemInfoAppear() // 레이어 마스크에 닿았을때 텍스트 호출
    //{
        
    //    actionText.gameObject.SetActive(true);
    //    actionText.text = hitInfo.transform.GetComponent<ItemPickUp>().itemData.itemName + " 획득 " + "<color=yellow>" + "(F)" + "</color>";
    //}
    //void IteminfoDisappear() // 아이템 안 맞으면 false
    //{
       
    //    actionText.gameObject.SetActive(false);
    //}

    private void TryAction()
    {
        //z클릭시 습득되게
        if (target != null)
        {
            if (hitInfo.transform != null)
            {
                //획득한 아이템에는 ItemPickUp이라는 컴포넌트
                //<ItemPickUp>().ItemData(고유정보 + 가변정보)
                //고유정보 : Itemcode, Itemname등등
                ItemPickUp _Pick = hitInfo.transform.GetComponent<ItemPickUp>();
                Debug.Log(_Pick.itemData.itemName + " 획득했습니다");
              //  IteminfoDisappear(); //메세지창 사라지기

                //인벤토리에 넣어주기
                UI_Inventory.ins.AddItemData(_Pick.itemData);
                _Pick.ClearDestroy();//오브젝트 주우면 필드에 주운아이템은 사라지게하기
                target = null;
            }
        }
    }

}
