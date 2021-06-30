using System.Collections;
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
            Pantaloons,
            Booties,
            StabbyThings,
            ProtectyThings
        }
        
        private Dictionary<EquipmentSlot, EquipmentItem> slots = new Dictionary<EquipmentSlot, EquipmentItem>();

        private void Start()
        {
            foreach (EquipmentSlot slot in System.Enum.GetValues(typeof(EquipmentSlot)))
            {
                slots.Add(slot, null);
            }
        }

        public EquipmentItem EquipItem(EquipmentItem _toEquip)
        {
            if (_toEquip == null)
            {
                Debug.LogError("Why you nulled this... never do that!");
                return null;
            }

            if (slots.TryGetValue(_toEquip.slot, out EquipmentItem item))
            {
                // Making copies!
                EquipmentItem original = item;
                slots[_toEquip.slot] = _toEquip;
                // return what was originally in the slot to prevent losing items when equipping
                return original;
            }
            slots.Add(_toEquip.slot, _toEquip);
            return null;
        }
    }
}