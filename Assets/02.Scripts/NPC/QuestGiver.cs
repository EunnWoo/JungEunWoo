using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestGiver : MonoBehaviour
{
    public Quest quest;
    public questObject questobj = new questObject();
    //Dictionary<int,string[]>questData;

    public Player player;
    public GameObject questWindow;
    private Text text;
    public Text titleText;
    public Text descriptionText;
    public Text expText;
    public Text goldText;
    int progress;
    void Start() {
        //questData = new Dictionary<int, string[]>();
        GenerateQuest();
    }
    void GenerateQuest(){
        quest.questData.Add(1,new string[]{"슬라임 잡기","슬라임 10마리를 잡아오자","90","780"});
        quest.questData.Add(2,new string[]{"재료 수집","a를 1개, b를 1개 구해오자","110","900"});
    }
    void Update() {
        progress = player.questProgress;
    }
    // public string GetTalk(int id, int talkIndex)
    // {
    //     if(talkIndex == questData[id].Length){
    //         return null;
    //     }
    //     else{
    //         return questData[id][talkIndex];
    //     }
    // }
    public void OpenQuestWindow(){
        questWindow.SetActive(true);
        for(int i=0;i<quest.questData[progress].Length;i++){
            text = questWindow.transform.GetChild(0).GetChild(i).GetComponent<Text>();
            text.text = quest.questData[progress][i];
        }        
    }
    public void AcceptQuest(){
        questWindow.SetActive(false);
        quest.isActive = true;
        player.quest = quest;
    }
}
