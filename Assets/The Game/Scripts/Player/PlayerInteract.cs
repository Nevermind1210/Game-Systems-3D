using Dialouge;
using Quests;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerInteract : MonoBehaviour
    {
        [SerializeField] private Inventory inventory;
        [SerializeField] private Text pickUpText;
        
        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.E))
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 5))
                {
                    if (hit.transform.tag == "NPC")
                    {
                        Dialougue npcDialouge = hit.transform.GetComponents<Dialougue>()[0];
                        if (npcDialouge)
                        {
                            DialougeManager.theManager.LoadDialouge(npcDialouge);
                            Cursor.lockState = CursorLockMode.None;
                            Cursor.visible = true;
                        }
                    }

                    if (hit.transform.tag == "Quest Board")
                    {
                        Quests.QuestManager.instance.LoadQuests();
                        Cursor.lockState = CursorLockMode.None;
                        Cursor.visible = true;
                    }
                }
                
                if (Physics.SphereCast(transform.position, 1f, transform.forward, out hit, 5f))
                {
                    FetchQuestItem fetchQuest = hit.collider.gameObject.GetComponent<FetchQuestItem>();
                    DroppedItem droppedItem = hit.collider.gameObject.GetComponent<DroppedItem>();
                    if (droppedItem != null)
                    {
                        if (fetchQuest != null)
                        {
                            fetchQuest.UpdateQuest();
                        }
                        pickUpText.text = "Picked up a " + droppedItem.name.Substring(0, droppedItem.name.Length - 7); // Can I trim the (Clone) off the end of the name?
                        inventory.AddItem(droppedItem.item);
                        Destroy(hit.collider.gameObject);

                    }
                }
            }
        }
    }
}
