using System.Collections;
using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventory : MonoBehaviour
{
   [SerializeField] private List<Items> inventory = new List<Items>();
    [SerializeField] private bool showIMGUIInventory = true;
    private Items selectedItem = null;

    #region Canvas Inventory
    [SerializeField] private Button ButtonPrefab;
    [SerializeField] private GameObject InventoryGameObject;
    [SerializeField] private GameObject InventoryContent;
    [SerializeField] private GameObject FilteContent;
    #endregion

    #region Display Inventory
    private Vector2 scrollPosition;
    private string sortType = "All";
    #endregion

    private void OnGUI()
    {
        if (showIMGUIInventory)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

            List<string> itemType = new List<string>(Enum.GetNames(typeof(Items.ItemType)));
            itemType.Insert(0, "All");

            for (int i = 0; i < itemType.Count; i++)
            {
                if (GUI.Button(new Rect(
                     (Screen.width / itemType.Count) * i
                     , 10
                     , Screen.width / itemType.Count
                     , 20), itemType[i]))
                {
                    sortType = itemType[i];
                }
            }
            Display();
            if (selectedItem != null)
            {
                DisplaySelectedItem();
            }
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            InventoryGameObject.SetActive(true);
            DisplayItemsCanvas();
        }
    }

    private void DisplayFiltersCanvas()
    {
        List<string> itemType = new List<string>(Enum.GetNames(typeof(Items.ItemType)));
        itemType.Insert(0, "All");

        for (int i = 0; i < itemType.Count; i++)
        {
            Button buttonGO = Instantiate<Button>(ButtonPrefab, InventoryContent.transform);
            Text buttonText = buttonGO.GetComponentInChildren<Text>();
            buttonGO.name = itemType[i] + " Filter";
            buttonText.text = itemType[i];

            buttonGO.onClick.AddListener(delegate { ChangeFilter(itemType[i]); });
        }
    }

    private void ChangeFilter(string itemType)
    {
        sortType = itemType;
        DisplayItemsCanvas();
    }

    void DestroyAllChildren(Transform parent)
    {
        foreach(Transform child in parent)
        {
            Destroy(child); // DESTORY THE CHILD!
        }
    }

    private void DisplayItemsCanvas()
    {
        for(int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Type.ToString() == sortType || sortType == "All")
            {
                Button buttonGO = Instantiate<Button>(ButtonPrefab, InventoryContent.transform);
                Text buttonText = buttonGO.GetComponentInChildren<Text>();
                buttonGO.name = inventory[i].Name + " Button";
                buttonText.text = inventory[i].Name;
            }
        }
    }    

    private void DisplaySelectedItem()
    {
        GUI.Box(new Rect(Screen.width / 4, Screen.height / 3,
            Screen.width / 5, Screen.height / 5),
            selectedItem.Icon);

        GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 5),
            Screen.width / 7, Screen.height / 15),
            selectedItem.Name);

        GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 3),
           Screen.width / 5, Screen.height / 5), selectedItem.Description +
           "\nValue: " + selectedItem.Value +
           "\nAmount: " + selectedItem.Amount);
    }

    private void Display()
    {
        scrollPosition = GUI.BeginScrollView(new Rect(0, 40, Screen.width, Screen.height - 40),
            scrollPosition,
            new Rect(0, 0, 0, inventory.Count * 30),
            false, true);
        int count = 0;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Type.ToString() == sortType || sortType == "All")
            {
                if (GUI.Button(new Rect(30, 0 + (count * 30), 200, 30), inventory[i].Name))
                {
                    selectedItem = inventory[i];
                }
                count++;
            }
        }
        GUI.EndScrollView();
    }

    public void AddItem(Items _item)
    {
        inventory.Add(_item);
    }

    public void RemoveItem(Items _items)
    {
        if (inventory.Contains(_items))
            inventory.Remove(_items);
    }
}
