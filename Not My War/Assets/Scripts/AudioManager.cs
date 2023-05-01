using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioSource sfxAudioSource;
    public static AudioSource musicAudioSource;


    // Start is called before the first frame update
    void Start()
    {
        sfxAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void playSoundEffect(AudioClip[] clip, float volume)
    {
        sfxAudioSource.PlayOneShot(clip[Random.Range(0, clip.Length)], volume);
    }
    public static void playSoundEffect(AudioClip clip, float volume)
    {
        sfxAudioSource.PlayOneShot(clip, volume);
    }
}
