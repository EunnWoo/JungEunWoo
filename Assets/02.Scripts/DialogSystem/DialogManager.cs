using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DialogManager : MonoBehaviour
{
    [SerializeField]
    private TalkManager talkManager;
    public GameObject dialogPanel;
    public Text talkText;
    public Text nameText;
    public GameObject NPC;
    public int talkIndex;
    public bool isAction = false;
    void Start() {
        talkManager = GameObject.Find("TalkManager").GetComponent<TalkManager>();
    }
    public void Action(GameObject npc){
        NPC = npc;
        ObjData objData = NPC.GetComponent<ObjData>();
        nameText.text = npc.name;
        Talk(objData.id, objData.isNpc);

        dialogPanel.SetActive(isAction);
    }
    void Talk(int id, bool isNpc)
    {
        string talkData = talkManager.GetTalk(id, talkIndex);
        if(talkData == null){
            isAction = false;
            talkIndex = 0;
            return;
        }

        if(isNpc){
            talkText.text = talkData;
        }
        else{
            talkText.text = talkData;
        }
        isAction = true;
        talkIndex++;
    }
}
