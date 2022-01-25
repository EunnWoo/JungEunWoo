using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int questProgress = 1;
    public Quest quest;
    public QuestGoal goal;
    public void enemyKill(){
        if(quest.isActive){
            quest.questGoal.EnemyKilled();
            if(quest.questGoal.IsReached()){
                //npc위에 완료 표시 && 대화하고 퀘스트 완료 버튼 활성화
                quest.Complete();
            }
        }
    }
    private void OnTriggerStay(Collider other) {
        if(other.gameObject.tag == "Monster"){
            if(Input.GetKeyDown(KeyCode.G)){
                enemyKill();
            }
        }
    }
}