using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{

    //public GameObject dialogPanel;
    //public Image portraitImg;//Npc 얼굴
    //public Text talkText;
    //public Text nameText;
    //public GameObject NPC;
    //public int talkIndex;


    //public bool isAction = false;

    //public void Action(GameObject npc){
        
    //    NPC = npc;
    //    ObjData objData = NPC.GetComponent<ObjData>();
    //    nameText.text = npc.name;
    //    Talk(objData.id, objData.isNpc);
    //    dialogPanel.SetActive(isAction);
    //}
    //public void Talk(int id, bool isNpc)
    //{
       
    //  //  int questTalkIndex = questManager.GetQuestTalkIndex(id);
        
    //    string talkData = Managers.talk.GetTalk(id, talkIndex);
    //    if(id == 2000){
    //        dialogPanel.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
    //    }
    //    if(talkData == null){//End Talk
    //        isAction = false;
    //        talkIndex = 0;
    //        return;
    //    }
    //    if(isNpc){
    //        talkText.text = talkData;
    //        //portraitImg.color = new Color(1,1,1,1);
    //    }
    //    else{
    //        talkText.text = talkData;
    //        //portraitImg.color = new Color(1,1,1,0);
    //    }
    //    isAction = true;
    //    talkIndex++;
    //}

}
