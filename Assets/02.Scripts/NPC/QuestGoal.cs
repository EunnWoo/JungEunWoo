using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestGoal
{
    public GoalType goalType;
    public int requreAmount;
    public int currentAmount;

    public bool IsReached(){
        return(currentAmount>=requreAmount);
    }
    public void EnemyKilled(){
        if(goalType == GoalType.Kill){
            currentAmount++;
            Debug.Log(currentAmount);
        }
    }
    public void ItemCollected(){
        if(goalType == GoalType.Gathering){
            currentAmount++;
        }
    }
}
public enum GoalType{
    Kill,
    Gathering
}
