using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum gameState
{
    playing, Paused
}
public class GameManager : MonoBehaviour
{
    public static  GameManager instance;
    public gameState state;

    private void Awake()
    {
        instance = this;

    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
