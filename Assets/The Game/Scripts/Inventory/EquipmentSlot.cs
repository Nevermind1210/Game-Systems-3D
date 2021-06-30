using UnityEngine;

namespace Player
{
    [System.Serializable]
    public struct EquipmentSlot
    {
        [SerializeField] public Item item;

        public Item EquippedItem
        {
            get
            {
                return item;
            }
            set
            {
                item = value;
                itemEquiped.Invoke(this); // change to this
            }
        }
        public Transform visualLocation;
        public Vector3 offset;

        public delegate void ItemEquiped(EquipmentSlot item); // Change to equipment slot
        public event ItemEquiped itemEquiped;
    }
}