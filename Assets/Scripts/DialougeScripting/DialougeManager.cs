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

        public static DialougeManager theManager;
        Dialougue currentDialouge;

        private void Awake()
        {
            //dialougeParent = transform.Find("Scroll View").gameObject;
            dialougeParent.SetActive(false);

            if (theManager == null)
            {
                theManager = this;
            }
            else
            {
                Destroy(this);
            }
        }

        public void LoadDialouge(Dialougue dialougue)
        {
            Button spawnedButton;
            dialougeParent.SetActive(true);
            currentDialouge = dialougue;
            CleanUpButtons();
            int i = 0;
            foreach (LineOfDialouge item in dialougue.dialougeOptions)
            {
                {
                    spawnedButton = Instantiate(buttonPrefab, buttonPanel).GetComponent<Button>();
                    spawnedButton.GetComponentInChildren<Text>().text = item.topic;

                    int j = i;
                    spawnedButton.onClick.AddListener(delegate { ButtonClicked(j); });
                }
                i++;
            }

            //spawn the goodbye button.
            spawnedButton = Instantiate(buttonPrefab, buttonPanel).GetComponent<Button>();
            spawnedButton.GetComponentInChildren<Text>().text = dialougue.goodbye.topic;
            spawnedButton.onClick.AddListener(EndConversation);

            DisplayResponse(currentDialouge.greeting);
        }

        void EndConversation()
        {
            CleanUpButtons();
            dialougeParent.SetActive(false);

            DisplayResponse(currentDialouge.goodbye.response);

            if(currentDialouge.goodbye.nextDialogue != null)
            {
                LoadDialouge(currentDialouge.goodbye.nextDialogue);
            }    
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }    
        }

        void ButtonClicked(int dialougeNum)
        {
            FactionsManager.instance.FactionsApproval(currentDialouge.faction, currentDialouge.dialougeOptions[dialougeNum].changeApproval);

            if(currentDialouge.dialougeOptions[dialougeNum].nextDialogue != null)
            {
                LoadDialouge(currentDialouge.dialougeOptions[dialougeNum].nextDialogue);
            }
            {
                DisplayResponse(currentDialouge.dialougeOptions[dialougeNum].response);
            }
        }

        private void DisplayResponse(string response)
        {
            responseText.text = response;
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