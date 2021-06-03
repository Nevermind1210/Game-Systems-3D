using System;
using UnityEditor;
using UnityEngine;

namespace Inventory
{
    [System.Serializable]
    public struct EquipmentSlot
    {
        [SerializeField] private Items items;

        public Items EquippedItem
        {
            get
            {
                return items;
            }
            set
            {
                items = value;
                itemEquipped.Invoke(this);       
            }
        }

        public Transform visualLocation;
        public Vector3 offset;

        public delegate void ItemEquipped(EquipmentSlot items);
        public event ItemEquipped itemEquipped;
    }

    public class Equipment : MonoBehaviour
    {
        public EquipmentSlot primary;
        public EquipmentSlot secondary;
        public EquipmentSlot defensive;

        private void Awake()
        {
            primary.itemEquipped += EquipItem;
            secondary.itemEquipped += EquipItem;
            defensive.itemEquipped += EquipItem;
        }

        private void Start()
        {
            EquipItem(primary);
            EquipItem(secondary);
            EquipItem(defensive);
        }

        public void EquipItem(EquipmentSlot items)
        {
            if (items.visualLocation == null)
            {
                return;
            }

            foreach (Transform child in items.visualLocation)
            {
                Destroy(child.gameObject);
            }

            if (items.EquippedItem.Mesh == null)
            {
                return;
            }
            GameObject meshInstance = Instantiate(items.EquippedItem.Mesh, items.visualLocation);
            meshInstance.transform.localPosition = items.offset;
            OffsetLocation offset = meshInstance.GetComponent<OffsetLocation>();
            if (offset != null)
            {
                meshInstance.transform.localPosition += offset.positionOffset;
                meshInstance.transform.localRotation = Quaternion.Euler(offset.rotationOffset);
                meshInstance.transform.localScale = offset.scaleOffset;
            }
        }
    }
}