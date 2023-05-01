using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public itemType itemType;
    public Sprite itemSprite;

    public SpriteRenderer itemRenderer;
    public GameObject itemPrefab;

    public Slot slot;

    bool holding;
    // Start is called before the first frame update
    void Start()
    {
        itemRenderer = GameObject.FindGameObjectWithTag("Item").GetComponent<SpriteRenderer>();
        slot = GetComponentInParent<Slot>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void selectOrDeselect()
    {
        
            if (holding && PlayerMechanics.instance.holdingAnItem == true)
            {
            PlayerMechanics.instance.itemHolding = null;
            PlayerMechanics.instance.currentSlot = null;

            itemRenderer.sprite = null;
                PlayerMechanics.instance.holdingAnItem = false;
                holding = false;
            }
            else if(PlayerMechanics.instance.holdingAnItem == false)
            {
            PlayerMechanics.instance.itemHolding = itemPrefab;
            PlayerMechanics.instance.currentSlot = slot;

            itemRenderer.sprite = itemSprite;
                PlayerMechanics.instance.holdingAnItem = true;
                holding = true;
            }
      
    }
}
