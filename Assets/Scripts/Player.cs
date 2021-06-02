using System;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class Player : MonoBehaviour
    {
        public enum EquipmentSlot
        {
            Helmet,
            Chestplate,
            Plantaloons,
            Booties,
            StabbyThingies,
            ProtectyThingies
        }

        private Dictionary<EquipmentSlot, EquipmentItem> slots = new Dictionary<EquipmentSlot, EquipmentItem>();
        
        // Start is called before the first frame update
        void Start()
        {
            foreach (EquipmentSlot slot in Enum.GetValues(typeof(EquipmentSlot)))
            {
                slots.Add(slot,null);
            }
        }

        public EquipmentItem EquipItem(EquipmentItem _toEquip)
        {
            if (_toEquip == null)
            {
                Debug.LogError("Why.. just why");
                return null;
            }
            // Attempt to get ANYTHING out of the slot, be it null or not
            if (slots.TryGetValue(_toEquip.slot, out EquipmentItem item))
            {
                // Create a copy of the original, set the slot item to the to the passed value
                EquipmentItem original = item;
                slots[_toEquip.slot] = _toEquip;
                // Return what was originally in the slot to prevent loosing items when equipping
                return original;
            }
            
            // SOMEHOW the slot didn't exist, so let's create it and return null as no item
            // would be in the slot anyways
            slots.Add(_toEquip.slot, _toEquip);
            return null;
        }
        
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}