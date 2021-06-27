using Inventory;
using UnityEngine;

namespace Inventory
{
   [System.Serializable]
   public class EquipmentItem : Items
   {
      public Player.EquipmentSlot slot = Player.EquipmentSlot.Booties;
      
      public bool isEquipped = false;

      public override void OnClicked()
      {
         base.OnClicked();

         Player player = GameObject.FindObjectOfType<Player>();
         EquipmentItem oldItem = player.EquipItem(this);
         PlayerInventory inventory = GameObject.FindObjectOfType<PlayerInventory>();
         if (oldItem != null)
         {
            inventory.AddItem(oldItem);
         }
         inventory.RemoveItem(this);
      }
   }
}