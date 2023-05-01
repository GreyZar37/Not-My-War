using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public enum aiState
{
    patrolling, Idle, chasing
}
public class AiController : MonoBehaviour
{
    public Transform sightPos;
    AIDestinationSetter destenation;
    AIPath aiControl;

    Animator animator;
    public Animator fade;

    public float spotRadius;

    public Transform[] patrolDestinations;

    public aiState state;
    public LayerMask playerLayer;

    public MovementScript movementScript;

    public AudioClip found;
    public AudioClip caughtSound;
    public AudioClip GirlScream;

    public GameObject dialog;
    // Start is called before the first frame update
    void Start()
    {
        destenation = GetComponent<AIDestinationSetter>();
        aiControl = GetComponent<AIPath>();

        movementScript = GameObject.FindObjectOfType<MovementScript>();
        destenation.target = patrolDestinations[Random.Range(0, patrolDestinations.Length)];

        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case aiState.patrolling:
                patrollingState();
                break;
            case aiState.Idle:
                break;
            case aiState.chasing:
                chasingState();
                break;
            default:
                break;
        }

        if(aiControl.desiredVelocity.x >= 0.01f)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }

    }


    public void idleState()
    {

    }
    public void chasingState()
    {
        animator.SetBool("Running", true);
        PlayerMechanics.instance.spottet = true;

        aiControl.maxSpeed = 3.5f;
        if (aiControl.reachedDestination && PlayerMechanics.instance.caught == false)
        {
            PlayerMechanics.instance.caught = true;

          movementScript.CancelInvoke("breath");
            dialog.SetActive(true);
            animator.SetTrigger("Caught");
            destenation.target.gameObject.SetActive(false);
            AudioManager.playSoundEffect(caughtSound, 1f);
            AudioManager.playSoundEffect(GirlScream, 0.5f);
            fade.SetBool("Fade", true);
            
        }
        
        if(aiControl.remainingDistance > (spotRadius * 2) * 1.20f)
        {
            PlayerMechanics.instance.spottet = false;

            destenation.target = patrolDestinations[Random.Range(0, patrolDestinations.Length)];
            state = aiState.patrolling;

        }
    }
    public void patrollingState()
    {
       
        animator.SetBool("Running", false);
        aiControl.maxSpeed = 1f; 
        if
        (aiControl.reachedDestination)
        {
            destenation.target = patrolDestinations[Random.Range(0, patrolDestinations.Length)];
        }
        Collider2D collider = Physics2D.OverlapCircle(sightPos.position, spotRadius * movementScript.noiseLevel, playerLayer );
       
        if (collider != null && PlayerMechanics.instance.currentState == playerState.visible)
        {
            destenation.target = collider.gameObject.transform;
            state = aiState.chasing;
            AudioManager.playSoundEffect(found, 1f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(sightPos.position, spotRadius * movementScript.noiseLevel);
    }
}
