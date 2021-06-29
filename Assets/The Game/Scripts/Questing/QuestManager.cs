using System;
using System.Collections;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Quests
{
    public class QuestManager : MonoBehaviour
    {
        public static QuestManager instance = null;

        public List<Quest> quests = new List<Quest>();

        private List<Quest> activeQuests = new List<Quest>();
        private Dictionary<string, Quest> questDatabase = new Dictionary<string, Quest>();

        public List<Quest> GetActiveQuests() => activeQuests;

        [NonSerialized] public Quest selectedQuest = null;

        [Header("Quest UI")]
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private GameObject activeQuestsGameObject;
        [SerializeField] private GameObject questsContent;
        [SerializeField] private GameObject questUIButtons;
        [SerializeField] private GameObject claimRewardButton;
        [SerializeField] private GameObject rewardPanel;
        [SerializeField] private Text rewardText;
        [SerializeField] private GameObject requirementsMetText;
        [SerializeField] private GameObject cantAcceptQuestPanel;
        [SerializeField] private Text cantAcceptQuestText;
        [SerializeField] private GameObject foundQuestItemPanel;
        [SerializeField] private Transform spawnLocation;

        [Header("Selected Quest Display")]
        [SerializeField] private Text questTitle;
        [SerializeField] private Text questDescription;

        [SerializeField] private Plr.Inventory inventory;

        
        private void Awake()
        {
            // If the instance isn't set, set it to this gameObject
            if (instance == null)
            {
                instance = this;
            }
            // If the instance is already set and it isn't this, destroy this gameobject.
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            activeQuestsGameObject.SetActive(false);
            claimRewardButton.SetActive(false);
            requirementsMetText.SetActive(false);
            cantAcceptQuestPanel.SetActive(false);
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (activeQuestsGameObject.activeSelf)
                {
                    activeQuestsGameObject.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = false;
                }
                else
                {
                    
                    activeQuestsGameObject.SetActive(true);
                    DisplayActiveQuestsCanvas();
                    selectedQuest = null;
                    DisplaySelectedQuestOnCanvas(selectedQuest);
                    // Set buttons unactive when accessing quests with tab.
                    questUIButtons.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
        
        private void GiveReward(Quest quest)
        {
            rewardPanel.SetActive(true);
            rewardText.text = "Money: " + quest.reward.rewardItem.Amount.ToString();

            inventory.AddItem(quest.reward.rewardItem);
        }
        
        public Quest GetQuest(string title)
        {
            questDatabase.TryGetValue(title, out Quest quest);
            return quest;
        }
        
        public void ClaimRewardsButton()
        {
            CompleteQuest(selectedQuest.title);
        }
        
        public void DeclineQuestButton()
        {
            activeQuestsGameObject.SetActive(false);
        }
        
        public void AcceptQuestButton()
        {
            if (selectedQuest != null)
            {
                if (selectedQuest.requiredLevel <= PlayerStats.CoolPlayerStats.levelInt)
                {
                    AcceptQuest(selectedQuest.title);
                    selectedQuest = null;
                    DisplayQuestsCanvas();
                    DisplaySelectedQuestOnCanvas(selectedQuest);
                }
                else
                {
                    cantAcceptQuestPanel.SetActive(true);
                    cantAcceptQuestText.text = "You need to be level " + selectedQuest.requiredLevel.ToString() + " to accept this quest";
                    Debug.Log("You need to be " + selectedQuest.requiredLevel.ToString() + " to accept this quest");
                }

            }
            else
                Debug.Log("No quest selected");
        }
        
        public void LoadQuests()
        {
            if (activeQuestsGameObject.activeSelf)
            {
                activeQuestsGameObject.SetActive(false);
            }
            else
            {
                activeQuestsGameObject.SetActive(true);
                //Set the buttons visable when accessing quests from the quest board
                questUIButtons.SetActive(true);
                DisplayQuestsCanvas();
                
            }
        }
        
        private void DisplayQuestsCanvas()
        {
            DestroyAllChildren(questsContent.transform);
            foreach (Quest quest in quests)
            {           
                // Put a test in here to test if the quest hass been unlocked yet??
                if (quest.stage == QuestStage.Unlocked ||quest.stage == QuestStage.InProgress || quest.stage == QuestStage.RequirementsMet)
                {                               
                    Button buttonGo = Instantiate<Button>(buttonPrefab, questsContent.transform);
                    Text buttonText = buttonGo.GetComponentInChildren<Text>();
                    buttonGo.name = quest.title + " button";
                    buttonText.text = quest.title;

                    Quest _quest = quest;
                    buttonGo.onClick.AddListener(delegate { DisplaySelectedQuestOnCanvas(_quest); });
                }
            }
        }
        
        private void DisplayActiveQuestsCanvas()
        {
            DestroyAllChildren(questsContent.transform);
            foreach (Quest quest in activeQuests)
            {
                // Put a test in here to test if the quest hass been unlocked yet??
                if (quest.stage == QuestStage.InProgress || quest.stage == QuestStage.RequirementsMet)
                {
                    Button buttonGo = Instantiate<Button>(buttonPrefab, questsContent.transform);
                    Text buttonText = buttonGo.GetComponentInChildren<Text>();
                    buttonGo.name = quest.title + " button";
                    buttonText.text = quest.title;

                    Quest _quest = quest;
                    buttonGo.onClick.AddListener(delegate { DisplaySelectedQuestOnCanvas(_quest); });
                }
            }
        }
        
        public void DisplaySelectedQuestOnCanvas(Quest _quest)
        {
            selectedQuest = _quest;

            if (_quest == null)
            {
                questTitle.text = "";
                questDescription.text = "";
            }
            else
            {
                questTitle.text = selectedQuest.title;
                questDescription.text =
                    $" {selectedQuest.description} \n Required Level: {selectedQuest.requiredLevel} \n Reward: {selectedQuest.reward.gold} ";
                if (_quest.stage == QuestStage.RequirementsMet)
                {
                    requirementsMetText.SetActive(true);
                    claimRewardButton.SetActive(true);
                }
                else
                {
                    requirementsMetText.SetActive(false);
                    claimRewardButton.SetActive(false);
                }
            }
        }

        public void DestroyAllChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }
        
        public void UpdateQuest(string _id)
        {
            // This is the same as checking if the key exits, if it does returning it
            // TryGetValue return s a boolean if it successfully got the item
            if(questDatabase.TryGetValue(_id, out Quest quest))
            {
                if(quest.stage == QuestStage.InProgress)
                {
                    //Check if the quest is ready to complete, if it is , update the stage, otherwise retain the stage.
                    quest.stage = quest.CheckQuestCompletion() ? QuestStage.RequirementsMet : quest.stage;
                }
                if(quest.stage == QuestStage.RequirementsMet)
                {
                    GameObject spawnPanel = Instantiate(foundQuestItemPanel, spawnLocation);
                    Destroy(spawnPanel, 3f);
                }
            }
        }

        // Take in the player!
        public void CompleteQuest(string _id)
        {
            if(questDatabase.TryGetValue(_id, out Quest quest))
            {
                if (quest.stage == QuestStage.RequirementsMet)
                {
                    quest.stage = QuestStage.Complete;
                    //Give the player their reward
                    GiveReward(quest);

                    activeQuests.Remove(quest);

                    //Find all related quests that are going to be unlocked
                    foreach (string questId in quest.unlockedQuests)
                    {
                        if (questDatabase.TryGetValue(questId, out Quest unlocked))
                        {
                            //Update their stages
                            unlocked.stage = QuestStage.Unlocked;                            
                        }
                    }
                    selectedQuest = null;
                    DisplayQuestsCanvas();
                    DisplaySelectedQuestOnCanvas(selectedQuest);


                }
                else
                    Debug.Log("You havent completed this quest yet");
            }
        }

        public void AcceptQuest(string _id)
        {
            if(questDatabase.TryGetValue(_id, out Quest quest))
            { 
                if(quest.stage == QuestStage.Unlocked)
                {
                    quest.stage = QuestStage.InProgress;
                    activeQuests.Add(quest);
                }
                if (quest.stage == QuestStage.InProgress)
                {
                    UpdateQuest(quest.title);
                }
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
    }
}
