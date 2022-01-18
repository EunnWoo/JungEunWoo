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

        //Quest Talk
        talkData.Add(10 + 1000, new string[]{"어서와 : 0", "이 마을에는 놀라운 전설이 있다는데 : 1", "상점 주인한테 물어봐."});
        talkData.Add(11 + 2000, new string[]{"여어. : 1", "마을의 전설을 들으러 왔다고? : 0", "그럼 일 좀 하나 해주면 좋을텐데... : 1", "드래곤 10마리만 잡아오겠나?"});

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
