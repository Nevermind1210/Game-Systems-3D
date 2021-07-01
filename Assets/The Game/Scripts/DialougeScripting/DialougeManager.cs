using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Faction;
using TMPro;

namespace Dialouge
{
    public class DialougeManager : MonoBehaviour
    {
        [SerializeField] GameObject buttonPrefab;
        [SerializeField] Transform buttonPanel;
        [SerializeField] GameObject responsePanel;
        [SerializeField] TextMeshProUGUI responseText;
        [SerializeField] private GameObject crosshairs;

        public static bool isTalking = false;

        GameObject dialoguePanel;

        Dialougue currentDialogue;

        public static DialougeManager theManager;
        private void Awake()
        {
            isTalking = false;
            
            dialoguePanel = transform.Find("Scroll View").gameObject;
            dialoguePanel.SetActive(false);

            if (theManager == null)
            {
                theManager = this;
            }
            else
            {
                Destroy(this);
            }
        }

        // Lets load the NPC'S dialogue!
        public void LoadDialouge(Dialougue dialougue)
        { 
            isTalking = true;
            crosshairs.SetActive(false);
            dialoguePanel.SetActive(true);
            responsePanel.SetActive(true);
            CleanUpButtons();
            currentDialogue = dialougue;

            responseText.text = dialougue.greeting;
            
            int i = 0;
            foreach (LineOfDialouge item in dialougue.dialougeOptions)
            {
                float? currentApproval = FactionsManager.instance.FactionsApproval(dialougue.faction);
                if(currentApproval != null && currentApproval > item.minApproval)
                { 
                    Button spawnedButton = Instantiate(buttonPrefab, buttonPanel).GetComponent<Button>();
                    spawnedButton.GetComponentInChildren<Text>().text = item.topic;

                    int j = i;
                    spawnedButton.onClick.AddListener(delegate { ButtonClicked(j); });
                }
                i++;
            }

            //spawn the goodbye button.
            Button byeButton = Instantiate(buttonPrefab, buttonPanel).GetComponent<Button>();
            byeButton.GetComponentInChildren<Text>().text = dialougue.goodbye.topic;
            byeButton.onClick.AddListener(EndConversation);
            
        }

        void EndConversation()
        {
            responsePanel.SetActive(true);
            responseText.text = currentDialogue.goodbye.response;
                        
            if(currentDialogue.goodbye.nextDialogue != null)
            {
                LoadDialouge(currentDialogue = currentDialogue.goodbye.nextDialogue);
            }
            else
            {
                CleanUpButtons();
                dialoguePanel.SetActive(false);
                crosshairs.SetActive(true);
                isTalking = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
            }
        }

        void ButtonClicked(int dialougeNum)
        {
            FactionsManager.instance.FactionsApproval(currentDialogue.faction, currentDialogue.dialougeOptions[dialougeNum].changeApproval);
            
            responsePanel.SetActive(true);
            responseText.text = currentDialogue.dialougeOptions[dialougeNum].response;
            if (currentDialogue.dialougeOptions[dialougeNum].nextDialogue != null)
            {
                LoadDialouge(currentDialogue = currentDialogue.dialougeOptions[dialougeNum].nextDialogue);
            }
        }
        

        void CleanUpButtons()
        {
            foreach (Transform child in buttonPanel)
            {
                Destroy(child.gameObject);
            }
        }
    }
}