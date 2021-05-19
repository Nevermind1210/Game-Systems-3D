using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager instance = null;

        public List<Quest> quests = new List<Quest>();

        private List<Quest> activeQuests = new List<Quest>();
        private Dictionary<string, Quest> questDatabase = new Dictionary<string, Quest>();

        public List<Quest> GetActiveQuests() => activeQuests;

        public void UpdateQuest(string _id)
        {
            // This is the same as checking if the key exits, if it does returning it
            // TryGetValue return s a boolean if it successfully got the item
            if(questDatabase.TryGetValue(_id, out Quest quest))
            {
                if(quest.stage == QuestStage.InProgress)
                {
                    //Check if the quests is ready to complete, if it is, update the stage
                    // otherwise retain the stage
                    quest.stage = quest.CheckQuestCompletion() ?
                        QuestStage.RequirementsMet : quest.stage;
                }
            }
        }

        // Take in the player!
        public void CompleteQuest(string _id)
        {
            if(questDatabase.TryGetValue(_id, out Quest quest))
            {
                quest.stage = QuestStage.Complete;
                activeQuests.Remove(quest);
                foreach (string questId in quest.unlockedQuests)
                {
                    if(questDatabase.TryGetValue(questId, out Quest unlocked))
                    {
                        //Update their stages
                        unlocked.stage = QuestStage.Unlocked;
                    }
                }
            }
        }

        public void AcceptQuest(string _id)
        {
            if(questDatabase.TryGetValue(_id, out Quest quest))
            { 

            }
        }
        private void Awake()
        {
            if(instance == null)
            {
                instance = this;
            }
            else if(instance != this)
            {
                Destroy(gameObject);
            }
        }

        // Start is called before the first frame update
        void Start()
        {
            //Find all the quests in the game/scene
            quests.Clear(); // prevents null references.
            quests.AddRange(FindObjectsOfType<Quest>());

            // Foreach function is Specific to list types that just functions
            // like a foreach loop with lamdas...
            quests.ForEach(quests =>
            {
                //
                if (!questDatabase.ContainsKey(quests.title))
                    questDatabase.Add(quests.title, quests);
                else
                    Debug.LogError("THAT QUESTS ALREADY EXISTS YOU DINGUS");
            });
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
