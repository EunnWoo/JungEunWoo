using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestGiver : MonoBehaviour
{
    [SerializeField]
    private Quest[] quests;

    public async void QuestSet()
    {
        foreach (var quest in quests)
        {
            if (quest.IsAcceptable && !QuestSystem.Instance.ContainsInCompleteQuests(quest) && !QuestSystem.Instance.ContainsInActiveQuests(quest))
            {
                QuestSystem.Instance.Register(quest);
            }
        }
    }
}