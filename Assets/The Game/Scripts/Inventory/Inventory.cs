using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Item> inventory = new List<Item>();
        [SerializeField] private bool showIMGUIInventory = true;
        [NonSerialized] public Item selectedItem; // mostly for the assessment for IMGUI stuff.

        #region Canvas Inventory
        [SerializeField] private Button buttonPrefab;
        [SerializeField] private GameObject inventoryGameObject;
        [SerializeField] private GameObject inventoryContent;
        [SerializeField] private GameObject filterContent;

        [Header("Selected Item Display")] 
        [SerializeField] private RawImage itemImage;
        [SerializeField] private Text itemName;
        [SerializeField] private Text itemDescription;
        [SerializeField] private Button useButton;

        [Header("Equipped Item UI")] 
        [SerializeField] private RawImage primaryImage;
        [SerializeField] private RawImage secondaryImage;
        [SerializeField] private RawImage defenceImage;
        [SerializeField] private Text primaryText;
        [SerializeField] private Text secondaryText;
        [SerializeField] private Text defenceText;
        #endregion

        #region Display Inventory ONGUI

        private Vector2 scrollposition;
        private string sortType = "All";

        #endregion


        //Updates image and description of item equip slots

        #region Update Slots

        private void UpdatePrimarySlot(Item _item)
        {
            primaryImage.texture = _item.Icon;
            primaryText.text =
                $"{_item.Name} \n Desciption: {_item.Description} \n Damage: {_item.Damage} \n Value: {_item.Value}";
        }

        private void UpdateSecondarySlot(Item _item)
        {
            secondaryImage.texture = _item.Icon;
            secondaryText.text =
                $"{_item.Name} \n Desciption: {_item.Description} \n Damage: {_item.Damage} \n Value: {_item.Value}";
        }

        private void UpdateDefenceSlot(Item _item)
        {
            defenceImage.texture = _item.Icon;
            defenceText.text =
                $"{_item.Name} \n Desciption: {_item.Description} \n Armour: {_item.Armour} \n Value: {_item.Value}";
        }

        #endregion

        // Functions for equipping the selected item into each slot

        #region Equip Item Functions

        public void EquipSelectedItemPrimary()
        {
            //Check if the selected item is a weapon
            if (selectedItem.Type == Item.ItemType.Weapon)
            {
                if (Equipment.TheEquipment.primary.EquippedItem != null)
                {
                    AddItem(Equipment.TheEquipment.primary.EquippedItem, 1);
                }

                // Set selected item and equip into slot
                Equipment.TheEquipment.primary.EquippedItem = selectedItem;


                UpdatePrimarySlot(selectedItem);
                selectedItem.Amount--;
                // If item amount reaches zero, remove from inventory.
                if (selectedItem.Amount <= 0)
                {
                    RemoveItem(selectedItem);
                    selectedItem = null;
                }

                DisplayItemsCanvas();
                DisplaySelectedItemOnCanvas(selectedItem);
            }
            else
            {
                Debug.Log("Can only equip a weapon in this slot");
            }
        }

        public void EquipSelectedItemSecondary()
        {
            //Check if the selected item is a weapon
            if (selectedItem.Type == Item.ItemType.Weapon)
            {
                // if there is a weapon in the slot already then add it back into the inventory
                if (Equipment.TheEquipment.secondary.EquippedItem != null)
                {
                    AddItem(Equipment.TheEquipment.secondary.EquippedItem, 1);
                }

                // Set selected item and equip into slot
                Equipment.TheEquipment.secondary.EquippedItem = selectedItem;


                UpdateSecondarySlot(selectedItem);
                selectedItem.Amount--;
                // If item amount reaches zero, remove from inventory.
                if (selectedItem.Amount <= 0)
                {
                    RemoveItem(selectedItem);
                    selectedItem = null;
                }

                DisplayItemsCanvas();
                DisplaySelectedItemOnCanvas(selectedItem);
            }
            else
            {
                Debug.Log("Can only equip a weapon in this slot");
            }
        }

        public void EquipSelectedItemDefence()
        {
            //Check if the selected item is a weapon
            if (selectedItem.Type == Item.ItemType.Helmet)
            {
                if (Equipment.TheEquipment.defensive.EquippedItem != null)
                {
                    AddItem(Equipment.TheEquipment.defensive.EquippedItem);
                }

                // Set selected item and equip into slot
                Equipment.TheEquipment.defensive.EquippedItem = selectedItem;


                UpdateDefenceSlot(selectedItem);
                selectedItem.Amount--;
                // If item amount reaches zero, remove from inventory.
                if (selectedItem.Amount <= 0)
                {
                    RemoveItem(selectedItem);
                    selectedItem = null;
                }

                DisplayItemsCanvas();
                DisplaySelectedItemOnCanvas(selectedItem);
            }
            else
            {
                Debug.Log("Can only equip a weapon in this slot");
            }
        }

        #endregion

        private void ShowUseItemButton(Item _item)
        {
            switch (_item.Type)
            {
                case Item.ItemType.Food:
                    useButton.gameObject.SetActive(true);
                    useButton.enabled = true;
                    break;
                case Item.ItemType.Potions:
                    useButton.gameObject.SetActive(true);
                    useButton.enabled = true;
                    break;
                default:
                    useButton.gameObject.SetActive(false);
                    break;
            }
        }

        public void UseItem()
        {
            if (selectedItem.Type == Item.ItemType.Food)
            {
                PlayerStats.CoolPlayerStats.health += selectedItem.Heal;
                // Removes 1 from the item amount
                selectedItem.Amount--;
                // If item amount drops to 0 remove from the inventory.
                if (selectedItem.Amount <= 0)
                {
                    RemoveItem(selectedItem);
                    selectedItem = null;
                }

                //Update display
                DisplayItemsCanvas();
                DisplaySelectedItemOnCanvas(selectedItem);
            }
            else if (selectedItem.Type == Item.ItemType.Potions)
            {
                PlayerStats.CoolPlayerStats.health += selectedItem.Heal;
                selectedItem.Amount--;
                if (selectedItem.Amount <= 0)
                {
                    RemoveItem(selectedItem);
                    selectedItem = null;
                }

                DisplayItemsCanvas();
                DisplaySelectedItemOnCanvas(selectedItem);
            }
            else
            {
                Debug.Log("Can't use that Item");
            }
        }

        private void Start()
        {
            inventoryGameObject.SetActive(false);
            DisplayFilterCanvas();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryGameObject.activeSelf)
                {
                    inventoryGameObject.SetActive(false);
                }
                else
                {
                    inventoryGameObject.SetActive(true);
                    DisplayItemsCanvas();
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }


        private void DisplayFilterCanvas()
        {
            List<string> itemType = new List<string>(Enum.GetNames(typeof(Item.ItemType)));
            itemType.Insert(0, "All");

            for (int i = 0; i < itemType.Count; i++)
            {
                Button buttonGo = Instantiate<Button>(buttonPrefab, filterContent.transform);
                Text buttonText = buttonGo.GetComponentInChildren<Text>();
                buttonGo.name = itemType[i] + " filter";
                buttonText.text = itemType[i];

                int x = i;
                buttonGo.onClick.AddListener(() => { sortType = itemType[x]; });
                buttonGo.onClick.AddListener(delegate { ChangeFilter(itemType[x]); });
            }
        }

        private void ChangeFilter(string itemType)
        {
            sortType = itemType;
            DisplayItemsCanvas();
        }

        void DestroyAllChildren(Transform parent)
        {
            foreach (Transform child in parent)
            {
                Destroy(child.gameObject);
            }
        }

        public void AddItem(Item _item)
        {
            AddItem(_item, _item.Amount);
        }


        public void AddItem(Item _item, int count)
        {
            Item foundItem = inventory.Find((x) => x.Name == _item.Name);

            if (foundItem == null)
            {
                _item.Amount = 1;
                inventory.Add(_item);
            }
            else
            {
                foundItem.Amount += count;
            }

            DisplayItemsCanvas();
            DisplaySelectedItemOnCanvas(selectedItem);
        }

        public void RemoveItem(Item _item)
        {
            if (inventory.Contains(_item))
                inventory.Remove(_item);
            DisplayItemsCanvas();
            DisplaySelectedItemOnCanvas(selectedItem);
        }

        private void DisplayItemsCanvas()
        {
            DestroyAllChildren(inventoryContent.transform);
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Type.ToString() == sortType || sortType == "All")
                {
                    Button buttonGo = Instantiate<Button>(buttonPrefab, inventoryContent.transform);
                    Text buttonText = buttonGo.GetComponentInChildren<Text>();
                    buttonGo.name = inventory[i].Name + " button";
                    buttonText.text = inventory[i].Name;

                    Item item = inventory[i];
                    buttonGo.onClick.AddListener(delegate { DisplaySelectedItemOnCanvas(item); });
                }
            }

            UpdatePrimarySlot(Equipment.TheEquipment.primary.item);
            UpdateSecondarySlot(Equipment.TheEquipment.secondary.item);
            UpdateDefenceSlot(Equipment.TheEquipment.defensive.item);
        }

        public void DisplaySelectedItemOnCanvas(Item _item)
        {
            selectedItem = _item;

            if (_item == null)
            {
                itemImage.texture = null;
                itemName.text = "";
                itemDescription.text = "";
            }
            else
            {
                itemImage.texture = selectedItem.Icon;
                itemName.text = selectedItem.Name;
                itemDescription.text =
                    $" {selectedItem.Description} \n Cost: {selectedItem.Value}  \n Damage: {selectedItem.Damage} " +
                    $"\n Armour: {selectedItem.Armour} \n Heal Amount: {selectedItem.Heal} \n Amount: {selectedItem.Amount}";

                ShowUseItemButton(selectedItem);
            }
        }

        #region On GUI

        private void OnGUI()
        {
            if (showIMGUIInventory)
            {
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

                List<string> itemTypes = new List<string>(Enum.GetNames(typeof(Item.ItemType)));
                itemTypes.Insert(0, "All");

                for (int i = 0; i < itemTypes.Count; i++)
                {
                    if (GUI.Button(
                        new Rect((Screen.width / itemTypes.Count) * i, 10, Screen.width / itemTypes.Count, 20),
                        itemTypes[i]))
                    {
                        sortType = itemTypes[i];
                    }
                }

                Display();
                if (selectedItem != null)
                {
                    DisplaySelectedItem();
                }
            }
        }


        private void DisplaySelectedItem()
        {
            GUI.Box(new Rect(Screen.width / 4, Screen.height / 3, Screen.width / 5, Screen.height / 5),
                selectedItem.Icon);
            GUI.Box(
                new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 5), Screen.width / 5,
                    Screen.height / 15), selectedItem.Name);
            GUI.Box(
                new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 3), Screen.width / 5,
                    Screen.height / 5), selectedItem.Description +
                                        "\nValue: " + selectedItem.Value + "\nAmount: " + selectedItem.Amount);
        }

        private void Display()
        {
            scrollposition = GUI.BeginScrollView(new Rect(0, 40, Screen.width, Screen.height - 40), scrollposition,
                new Rect(0, 0, 0, inventory.Count * 30), false, true);
            int count = 0;
            for (int i = 0; i < inventory.Count; i++)
            {
                if (inventory[i].Type.ToString() == sortType || sortType == "All")
                {
                    if (GUI.Button(new Rect(30, 0 + (count * 30), 200, 30), inventory[i].Name))
                    {
                        selectedItem = inventory[i];
                        selectedItem.OnClicked();
                    }
                    count++;
                }
            }

            GUI.EndScrollView();
        }

        public void ShowIMGUI()
        {
            showIMGUIInventory = true;
        }
        #endregion
    }
}