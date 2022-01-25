using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> talkData;
    public Player progress;
    void Start()
    {
        talkData = new Dictionary<int, string[]>();
        GenerateData();
    }
    void GenerateData()
    {
        talkData.Add(1000, new string[]{"...."});
        talkData.Add(2000, new string[]{"없는거 빼고 다 있음"});
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
