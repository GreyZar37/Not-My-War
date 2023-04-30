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
    public AIDestinationSetter destenation;
    public AIPath aiControl;

    public float spotRadius;

    public Transform[] patrolDestinations;

    public aiState state;
    public LayerMask playerLayer;
    // Start is called before the first frame update
    void Start()
    {
        destenation = GetComponent<AIDestinationSetter>();
        aiControl = GetComponent<AIPath>();

        destenation.target = patrolDestinations[Random.Range(0, patrolDestinations.Length)];

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

    }


    public void idleState()
    {

    }
    public void chasingState()
    {
        aiControl.maxSpeed = 2.5f;
        if (aiControl.reachedDestination)
        {
            print("Caugth");
        }
        
        if(aiControl.remainingDistance > spotRadius * 1.20f)
        {
            destenation.target = patrolDestinations[Random.Range(0, patrolDestinations.Length)];
            state = aiState.patrolling;

        }
    }
    public void patrollingState()
    {
        aiControl.maxSpeed = 1;
        if (aiControl.reachedDestination)
        {
            destenation.target = patrolDestinations[Random.Range(0, patrolDestinations.Length)];
        }
        Collider2D collider = Physics2D.OverlapCircle(transform.position, spotRadius, playerLayer );
       
        if (collider != null)
        {
            destenation.target = collider.gameObject.transform;
            state = aiState.chasing;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, spotRadius);
    }
}
