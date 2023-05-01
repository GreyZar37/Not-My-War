using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    public Inventory inventory;
    public Inventory inventoryMoney;
    
    public int i;

    public bool moneySlot;
    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
        inventoryMoney = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.childCount <= 0)
        {
            if(!moneySlot)
            {
                inventory.slots[i].isFull = false;
            }
            else
            {
                inventoryMoney.slots[i].isFull = false;
            }
      
        }

        
    }

    public void DropItem()
    {
        foreach(Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
