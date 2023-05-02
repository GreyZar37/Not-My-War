using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ending : MonoBehaviour
{
    public GameObject end;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            end.SetActive(true);
            GameManager.instance.state = gameState.Paused;
            player.SetActive(false);
        }
    }

    public void openWebsite()
    {
        Application.OpenURL("https://www.rodekors.dk/stoet/afghanistan?gclid=Cj0KCQjw6cKiBhD5ARIsAKXUdybulND6ai3Txh7Pr4CIMR5PUfmXC4eH8BknVZtxMtBWOIiZHM0h9j8aAghSEALw_wcB");
        Application.Quit();
    }
}
