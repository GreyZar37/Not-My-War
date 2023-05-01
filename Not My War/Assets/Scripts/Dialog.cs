using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
using UnityEngine.Timeline;
using UnityEngine.Playables;

public class Dialog : MonoBehaviour
{
    public TextMeshProUGUI textDisplay;

    public string[] sentences;
    public int index;

    public float typingSpeed;

    public string[] LostSentences;


    public GameObject DialogPanel;




    public bool talking;


    public float nextSentenceTimer;



    bool startedTimer;

    public Animator fader;

    bool ended;

    public PlayableDirector StartCutscene;


    // Start is called before the first frame update

    private void Start()
    {
        typingSpeed = 0.05f;

        StartCoroutine(Type());
    }
    // Update is called once per frame
    void Update()
    {

        if(StartCutscene.time <= 0 && GameManager.instance.state == gameState.Paused && !PlayerMechanics.instance.caught)
        {
            GameManager.instance.state = gameState.playing;
        }

        if (PlayerMechanics.instance.caught && !ended)
        {

            sentences = LostSentences;
            index = 0;
            typingSpeed = 0.07f;
            ended = true;

            StartCoroutine(Type());

        }


        if (textDisplay.text == sentences[index] && startedTimer == false)
        {
          StartCoroutine(next());
            
        }
      

    }
    public IEnumerator Type()
    {

        GameManager.instance.state = gameState.Paused;
        

        talking = true;



        foreach (var letter in sentences[index].ToCharArray())
        {
   
            textDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }


    public void nextSentence()
    {


        if (index < sentences.Length - 1)
        {
          

            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else 
        {

            textDisplay.text = "";
            talking = false;
            if (!PlayerMechanics.instance.caught)
            {
                StartCutscene.Play();
                DialogPanel.SetActive(false);
                fader.SetBool("Fade", false);
            }
           

        }
    }

   public IEnumerator next()
    {
        startedTimer = true;
      yield  return new WaitForSeconds(nextSentenceTimer);
        nextSentence();
        startedTimer = false;
    }

  
}
