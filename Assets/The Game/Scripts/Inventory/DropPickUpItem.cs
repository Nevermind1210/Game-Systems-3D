using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Inventory
{
    public class DropPickUpItem : MonoBehaviour
    {
        [SerializeField] private PlayerInventory inventory;
        [SerializeField] private Transform dropPoint;
        [SerializeField] private new Camera camera;

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.F))
            {
                Ray ray = camera.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
                RaycastHit hitInfo;

                if(Physics.Raycast(ray, out hitInfo, 50f))
                {
                    DroppedItem droppedItem = hitInfo.collider.gameObject.GetComponent<DroppedItem>();
                    if(droppedItem != null)
                    {
                        inventory.AddItem(droppedItem.item);
                        Destroy(hitInfo.collider.gameObject);
                    }

                }
            }
        }
        public void DropItem()
        { 
            if(inventory.selectedItem == null)
            {
                return;
            }

            GameObject mesh = inventory.selectedItem.Mesh;
            if(mesh != null)
            {
                GameObject spawnedMesh = Instantiate(mesh, null);
                spawnedMesh.transform.position = dropPoint.position;

                DroppedItem droppedItem = mesh.GetComponent<DroppedItem>();
                if(droppedItem == null)
                {
                    droppedItem = spawnedMesh.AddComponent<DroppedItem>();
                }

                if (droppedItem != null)
                {
                    droppedItem.item = new Items( inventory.selectedItem, 1);
                }
            }

            inventory.selectedItem.Amount--;
            if(inventory.selectedItem.Amount <= 0)
            {
                inventory.RemoveItem(inventory.selectedItem);
                inventory.selectedItem = null;
            }

        }
    }
}