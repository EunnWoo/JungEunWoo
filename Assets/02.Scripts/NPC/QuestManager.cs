using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int questId;
    public int questActionIndex = 0;
    Dictionary<int,QuestData> quesetList;
    void Awake()
    {
        quesetList = new Dictionary<int, QuestData>();
        GenerateData();
    }
    void GenerateData()
    {
        quesetList.Add(10, new QuestData("마을 사람들과 대화하기.", new int[]{ 1000, 2000 }));
        quesetList.Add(20, new QuestData("드래곤 잡기.", new int[]{ 1000 }));
    }
    public int GetQuestTalkIndex(int id)
    {    
        return questId + questActionIndex;
    }
    public string CheckQuest(int id)
    {
        if(id == quesetList[questId].npcId[questActionIndex]){
            questActionIndex++;
        }
        if(questActionIndex == quesetList[questId].npcId.Length){
            NextQuest();
        }
        return quesetList[questId].questName;
    }
    void NextQuest()
    {
        questId += 10;
        questActionIndex = 0;
    }
}
