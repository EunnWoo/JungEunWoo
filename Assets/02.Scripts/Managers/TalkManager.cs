using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    


    void Start()
    {
        
        GenerateData();
    }
    void GenerateData()
    {
        talkData = new Dictionary<int, string[]>();
        talkData.Add(1000, new string[]{"...."});
        talkData.Add(2000, new string[]{"없는거 빼고 다 있음"});
        talkData.Add(1, new string[] {"궁수" });
        talkData.Add(2, new string[] {"전사" });
        talkData.Add(3, new string[] {"법사" });
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
