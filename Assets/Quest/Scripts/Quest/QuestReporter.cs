using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestReporter : MonoBehaviour
{
    [SerializeField]
    private Category category;
    [SerializeField]
    private TaskTarget target;
    [SerializeField]
    private int successCount;
    [SerializeField]
    private string[] colliderTags;

    private void OnTriggerEnter(Collider other){
        ReportIfPassCondition(other);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        ReportIfPassCondition(other);
    }

    public void Report(){
        QuestSystem.Instance.ReceiveReport(category,target,successCount);
        Debug.Log("Report");
    }
    private void ReportIfPassCondition(Component other){
        if(colliderTags.Any(x => other.CompareTag(x))){
            Report();
            Debug.Log("ReportIfPassCondition");
        }
    }
}