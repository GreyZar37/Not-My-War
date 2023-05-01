using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct inventorySlot
{
    public bool isFull;
    public GameObject slot;

}
public class Inventory : MonoBehaviour
{
    public GameObject InventoryCanvas;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && InventoryCanvas != null)
        {
            if (InventoryCanvas.activeInHierarchy)
            {
                InventoryCanvas.SetActive(false);
                GameManager.instance.state = gameState.playing;
            }
            else
            {
                InventoryCanvas.SetActive(true);
                GameManager.instance.state = gameState.Paused;

            }
        }
    }
    public inventorySlot[] slots;
}
