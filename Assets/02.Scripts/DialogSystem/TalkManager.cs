using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }
    void GenerateData()
    {
        talkData.Add(1000, new string[]{"안녕?","무슨 일?"});
    }
    public string GetTalk(int id, int talkIndex)
    {
        if(talkIndex == talkData[id].Length){
            return null;
        }
        else{
            return talkData[id][talkIndex];
        }
    }
}
