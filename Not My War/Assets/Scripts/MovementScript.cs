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

    public movementType movementTypes;


    float horizontal;
    float vertical;


    public Vector2 movement;
    Rigidbody2D rb2D;

    Animator anim;

    public AudioClip[] movementSound;

    float volume;
    public float noiseLevel;

    public float  maxEnergy;
    bool outOfBreath;
    float energy;

    bool breathing;

    public AudioClip outOfBreathSound;
    public AudioClip runningSound;
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

                    if(energy < maxEnergy)
                    {
                        energy += Time.deltaTime;
                    }
                  
                    volume = 0.025f;
                    anim.SetBool("Running", false);
                    anim.SetBool("Crouching", false);
                    noiseLevel = 1.5f;
                    speed = walkSpeed;
                    if (breathing)
                    {
                        breathing = false;
                        CancelInvoke("breath");
                    }
                    break;
                case movementType.Crouching:
                    if (energy < maxEnergy)
                    {
                        energy += Time.deltaTime;
                    }

                    volume = 0.01f;
                    noiseLevel = 0.7f;

                    anim.SetBool("Running", false);
                    anim.SetBool("Crouching", true);

                    if (breathing)
                    {
                      
                        breathing = false;
                        CancelInvoke("breath");
                    }
                    

                    speed = crouchSpeed;
                    break;
                case movementType.Running:

                    if(breathing == false)
                    {
                        InvokeRepeating("breath", 0, runningSound.length);
                        breathing = true;
                    }
                   

                    energy -= Time.deltaTime;
                    volume = 0.05f;
                    noiseLevel = 2f;

                    anim.SetBool("Running", true);
                    anim.SetBool("Crouching", false);
                    speed = runSpeed;
                    break;
                default:
                    break;
            }
            movement = new Vector2(horizontal * speed, vertical * speed);
            flip();
            
            controls();
            anim.SetFloat("Velocity", Mathf.Abs(movement.x) + Mathf.Abs(movement.y));

            if (energy > maxEnergy)
            {
                energy = maxEnergy;
            }

            if (energy <= 0 && outOfBreath == false)
            {
                AudioManager.playSoundEffect(outOfBreathSound, 1);
                outOfBreath = true;
            }
            else if (energy > maxEnergy / 2)
            {
                outOfBreath = false;
            }

        }
        else
        {
            movement = Vector2.zero;
        }


        rb2D.velocity = movement;
       
    }

    public void breath()
    {
        AudioManager.playSoundEffect(runningSound, 1);
    }

    void flip()
    {
        if(horizontal > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }
        else if(horizontal < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);

        }
    }

    void controls()
    {
        if (Input.GetKey(KeyCode.LeftShift) && energy > 0 && outOfBreath == false)
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

    public void playSound()
    {
        AudioManager.playSoundEffect(movementSound, volume);
    }
}
