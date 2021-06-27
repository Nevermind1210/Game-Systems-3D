using System;
using System.Collections.Generic;
using Player;
using UnityEngine;
using UnityEngine.UI;

namespace Plr
{
    public class Inventory : MonoBehaviour
    {
        [SerializeField] private List<Item> inventory = new List<Item>();
        [SerializeField] private bool showIMGUIIventory = true; // Welp its for the assessment I guess
        [NonSerialized] public Item selectedItem = null; // This is what you see inside the game will also update inside this script... oofta...
        
        // As it states the variables the holders the containers for the... you guessed it! The inspector!
        #region Canvas Variables/Inventory
        
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
        private Vector2 scrollpos;
        private string sortType = "All";
        #endregion
        
        // This region holds the values of the items and description and place it into the slot!
        #region Update Slots!
        private void UpdatePrimarySlot(Item _item)
        {
            primaryImage.texture = _item.Icon;
            primaryText.text = $"{_item.Name} \n Desciption: {_item.Description} \n Damage: {_item.Damage} \n Value: {_item.Value}";
        }
        private void UpdateSecondarySlot(Item _item)
        {
            secondaryImage.texture = _item.Icon;
            secondaryText.text = $"{_item.Name} \n Desciption: {_item.Description} \n Damage: {_item.Damage} \n Value: {_item.Value}";
        }
        private void UpdateDefenceSlot(Item _item)
        {
            defenceImage.texture = _item.Icon;
            defenceText.text = $"{_item.Name} \n Desciption: {_item.Description} \n Armour: {_item.Armour} \n Value: {_item.Value}";
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

        private void UseItem()
        {
            if (selectedItem.Type == Item.ItemType.Food)
            {
                PlayerStats
            }
        }

    }
}