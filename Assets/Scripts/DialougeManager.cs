using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialougeManager : MonoBehaviour
{
    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform buttonPanel;

    GameObject dialougeParent;
   

    public static DialougeManager theManager;
    Dialougue currentDialouge;

    private void Awake()
    {
        dialougeParent = transform.Find("Scroll View").gameObject;
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
        CleanUpButtons();
        int i = 0;
        foreach (LineOfDialouge item in dialougue.dialougeOptions)
        {
            spawnedButton = Instantiate(buttonPrefab,buttonPanel).GetComponent<Button>();
            spawnedButton.GetComponentInChildren<Text>().text = item.topic;

            int j = i;
            spawnedButton.onClick.AddListener(delegate { ButtonClicked(j); });
            i++;
        }

        //spawn the goodbye button.
        spawnedButton = Instantiate(buttonPrefab, buttonPanel).GetComponent<Button>();
        spawnedButton.GetComponentInChildren<Text>().text = dialougue.goodbye.topic;
        spawnedButton.onClick.AddListener(EndConversation);

    }

    void EndConversation()
    {
        print(currentDialouge.goodbye.response);
        CleanUpButtons();
        dialougeParent.SetActive(false);
    }

    void ButtonClicked(int dialougeNum)
    {
        print(currentDialouge.dialougeOptions[dialougeNum].response);
    }

    void CleanUpButtons()
    {
        foreach (Transform child in buttonPanel)
        {
            Destroy(child.gameObject);
        }
    }
     
    // Update is called once per frame
    void Update()
    {
       
    }
}
