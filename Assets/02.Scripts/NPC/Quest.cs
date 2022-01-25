using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class questObject{
    Player player;
    Quest quest;
    private int progress = 0;
    public int Progress{
       get;set;
    }
}

[System.Serializable]
public class Quest
{
    public Dictionary<int,string[]> questData = new Dictionary<int, string[]>();
    questObject progress = new questObject();
    public bool isActive;    
    public QuestGoal questGoal;
    public void Complete(){
        isActive = false;
        progress.Progress += 1;
        Debug.Log(progress.Progress);
    }
}
