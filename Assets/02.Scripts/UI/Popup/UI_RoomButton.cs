using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class UI_RoomButton : MonoBehaviour
{

    //enum Texts
    //{
    //    RoomNameText,
    //    CountText

    //}

    RoomInfo info;
    [SerializeField]
    Text roomNametext;
    [SerializeField]
    Text countText;

    UI_FindRoomMenu ui_FindRoomMenu;
    private void Awake()
    {     
        ui_FindRoomMenu = FindObjectOfType<UI_FindRoomMenu>();
        this.gameObject.AddUIEvent(OnClick);
    
    }
    

    public void SetUp(RoomInfo _info)
    {
        info = _info;
        roomNametext.text = info.Name;
        countText.text = info.PlayerCount +" / " + info.MaxPlayers;

    }
    public void OnClick(PointerEventData data)
    {
        ui_FindRoomMenu.JoinRoom(info);
    }

}
