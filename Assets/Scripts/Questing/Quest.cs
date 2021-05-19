using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public enum QuestStage
    {
        Locked,
        Unlocked,
        InProgress,
        RequirementsMet,
        Complete
    }

    [System.Serializable]
    public abstract class Quest : MonoBehaviour
    {
        public string title;
        [TextArea] public string description;

        public QuestRewards reward;

        public QuestStage stage;
        
        [Tooltip("The title of the previous quests in the chain.")]
        public string previousQuest;
        [Tooltip("The title of the quests to be unlocked.")]
        public string[] unlockedQuests;

        public abstract bool CheckQuestCompletion();
    }

    [System.Serializable]
    public struct QuestRewards
    {
        public float experience;
        public int gold;
    }
}
