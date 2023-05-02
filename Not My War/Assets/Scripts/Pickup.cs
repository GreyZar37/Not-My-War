using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum itemType
{
    food, money, keysItem
}
public class Pickup : MonoBehaviour
{
    
    public itemType type;

    GameObject player;
    MovementScript playerMovement;


    Inventory inventory;
    Inventory inventoryMoney;

    float distance;

    public GameObject itemButton;

    public GameObject itemPrefab;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerMovement = player.GetComponent<MovementScript>();

        inventory = player.GetComponent<Inventory>();

        inventoryMoney = GameObject.FindGameObjectWithTag("Inventory").GetComponent<Inventory>();
       
    }

    private void Update()
    {
        distance = Vector2.Distance(playerMovement.hands.position, transform.position);

     

    }


    private void OnMouseDown()
    {
        if (distance <= 1 && !PlayerMechanics.instance.holdingAnItem)
        {
            pickUp();
        }

    }

    public void pickUp()
    {
        if(type == itemType.food)
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.slots[i].isFull == false)
                {
                    inventory.slots[i].isFull = true;

                    GameObject uiItem = Instantiate(itemButton, inventory.slots[i].slot.transform, false);
                    Interact itemInfo = uiItem.GetComponent<Interact>();
                    itemInfo.itemSprite = GetComponent<SpriteRenderer>().sprite;
                    itemInfo.itemType = type;
                    itemInfo.itemPrefab = itemPrefab;

                    Destroy(gameObject);
                    break;
                }
            }
        }
        else if (type == itemType.money)
        {
            for (int i = 0; i < inventoryMoney.slots.Length; i++)
            {
                if (inventoryMoney.slots[i].isFull == false)
                {
                    inventoryMoney.slots[i].isFull = true;

                   GameObject uiItem = Instantiate(itemButton, inventoryMoney.slots[i].slot.transform, false);
                    Interact itemInfo = uiItem.GetComponent<Interact>();
                    itemInfo.itemSprite = GetComponent<SpriteRenderer>().sprite;
                    itemInfo.itemType = type;
                    itemInfo.itemPrefab = itemPrefab;
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }
    
}
