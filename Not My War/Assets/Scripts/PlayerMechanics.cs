using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum playerState
{
    visible, invisible
}
public class PlayerMechanics : MonoBehaviour
{
    public playerState currentState;
    public static PlayerMechanics instance;
    public bool nearHidingSpot;
    public bool holdingAnItem;

    public GameObject itemHolding;
    public Slot currentSlot;
    public SpriteRenderer itemSpriteRend;
    public Transform hand;

    MovementScript movementScript;
    Animator animator;

    bool doingSomething;

    public bool spottet;

    public bool caught;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        movementScript = GetComponent<MovementScript>();
        instance = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        

        if(movementScript.movementTypes == movementType.Crouching && nearHidingSpot)
        {
            currentState = playerState.invisible;
        }
        else
        {
            currentState = playerState.visible;
        }

        if(Input.GetMouseButtonDown(0) && doingSomething == false && GameManager.instance.state == gameState.playing && holdingAnItem)
        {
            Throw();
        }
        else if(Input.GetMouseButtonDown(1) && doingSomething == false && GameManager.instance.state == gameState.playing && holdingAnItem)
        {
            Eat();
        }

        
    }

    public void Throw()
    {
        if(Mathf.Abs(movementScript.movement.x) + Mathf.Abs(movementScript.movement.y) <= 0)
        {
         Instantiate(itemHolding, hand.transform.position, Quaternion.identity);

           

         animator.SetTrigger("Throw");
         GameManager.instance.state = gameState.Paused;
         doingSomething = true;
            currentSlot.DropItem();

            currentSlot = null;
            itemHolding = null;
            holdingAnItem = false;
            itemSpriteRend.sprite = null;
        }
    }

    public void Eat()
    {
        if (Mathf.Abs(movementScript.movement.x) + Mathf.Abs(movementScript.movement.y) <= 0)
        {
            animator.SetTrigger("Eat");
            GameManager.instance.state = gameState.Paused;
            doingSomething = true;
            currentSlot.DropItem();

            currentSlot = null;
            itemHolding = null;
            holdingAnItem = false;
            itemSpriteRend.sprite = null;
        }
    }

    public void doingDone()
    {
        doingSomething = false;
        GameManager.instance.state = gameState.playing;

    }
}
