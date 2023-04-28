using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum movementType
{
    walking, Crouching, Running
}

public class MovementScript : MonoBehaviour
{

    public Transform hands;

    [SerializeField] float walkSpeed;
    [SerializeField] float crouchSpeed;
    [SerializeField] float runSpeed;

    float speed;

    movementType movementTypes;


    float horizontal;
    float vertical;


    Vector2 movement;
    Rigidbody2D rb2D;

    Animator anim;
   

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        if(GameManager.instance.state == gameState.playing)
        {
            switch (movementTypes)
            {
                case movementType.walking:
                    speed = walkSpeed;
                    break;
                case movementType.Crouching:
                    speed = crouchSpeed;
                    break;
                case movementType.Running:
                    speed = runSpeed;
                    break;
                default:
                    break;
            }
            movement = new Vector2(horizontal * speed, vertical * speed);
            flip();

        }
        else
        {
            movement = Vector2.zero;
        }


        rb2D.velocity = movement;
        controls();
        anim.SetFloat("Velocity", Mathf.Abs(movement.x + movement.y));
    }

    void flip()
    {
        if(horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }
        else if(horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
    }

    void controls()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            movementTypes = movementType.Running;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            movementTypes = movementType.Crouching;
        }
        else
        {
            movementTypes = movementType.walking;
        }

     
    }
}
