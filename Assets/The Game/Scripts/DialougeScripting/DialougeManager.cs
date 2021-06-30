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
        [SerializeField] GameObject dialougeParent;
        [SerializeField] TextMeshProUGUI responseText;

        public static bool isTalking;
        
        public static DialougeManager theManager;

        GameObject dialouguePanel;
        
        Dialougue currentDialouge;

        private void Awake()
        {
            isTalking = false;
            
            dialouguePanel = transform.Find("Scroll View").gameObject;
            dialouguePanel.SetActive(false);

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
            dialougeParent.SetActive(true);
            currentDialouge = dialougue;
            CleanUpButtons();

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
            dialougeParent.SetActive(false);
            responseText.text = currentDialouge.goodbye.response;

            if(currentDialouge.goodbye.nextDialogue != null)
            {
                LoadDialouge(currentDialouge = currentDialouge.goodbye.nextDialogue);
            }    
            else
            {
                CleanUpButtons();
                dialouguePanel.SetActive(false);
                isTalking = false;
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = false;
            }    
        }

        void ButtonClicked(int dialougeNum)
        {
            FactionsManager.instance.FactionsApproval(currentDialouge.faction, currentDialouge.dialougeOptions[dialougeNum].changeApproval);
            
            
            responseText.text = currentDialouge.dialougeOptions[dialougeNum].response;
            if(currentDialouge.dialougeOptions[dialougeNum].nextDialogue != null)
            {
                LoadDialouge(currentDialouge.dialougeOptions[dialougeNum].nextDialogue);
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