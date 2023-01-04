using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{

    [SerializeField] float speed;
    float horizontal;
    float vertical;


    Vector2 movement;
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        movement = new Vector2(horizontal*speed, vertical * speed);

        rb2D.velocity = movement;

        flip();
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
}
