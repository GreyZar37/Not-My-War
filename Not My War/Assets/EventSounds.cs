using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class EventSounds : MonoBehaviour
{
    public AudioClip[] movementSound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playSound()
    {
        AudioManager.playSoundEffect(movementSound, 0.025f);
    }
}
