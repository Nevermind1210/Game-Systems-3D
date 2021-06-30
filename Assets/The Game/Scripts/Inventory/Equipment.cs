using System;
using UnityEditor;
using UnityEngine;

namespace Player
{
    public class Equipment : MonoBehaviour
    {
        public static Equipment TheEquipment;

        public EquipmentSlot primary;
        public EquipmentSlot secondary;
        public EquipmentSlot defensive;

        private void Awake()
        {
            if (TheEquipment == null)
            {
                TheEquipment = this;
            }
            else
                Destroy(this);

            //adding this function to the equipmentslot set event.
            primary.itemEquiped += EquipItem;
            secondary.itemEquiped += EquipItem;
            defensive.itemEquiped += EquipItem;
        }

        private void Start()
        {
            EquipItem(primary);
            EquipItem(secondary);
            EquipItem(defensive);
        }
        
        public void EquipItem(EquipmentSlot item)
        {
            if (item.visualLocation == null)
            {
                return;
            }

            foreach (Transform transform in item.visualLocation)
            {
                GameObject.Destroy(transform.gameObject);
            }

            if (item.EquippedItem.Mesh == null)
            {
                return;
            }

            GameObject meshInstance = Instantiate(item.EquippedItem.Mesh, item.visualLocation);
            meshInstance.transform.localPosition = item.offset;
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