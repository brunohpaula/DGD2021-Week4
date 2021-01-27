using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class GameStateController : MonoBehaviour
{

    //some variables and behaviours were moved from the Player to this class to make
    //the game more organised
    
    //duration of the level in seconds
    public static float levelDuration;

    //controls whether the game is running (true) or paused/over (false)
    public static bool gameRunning;

    //points that the player has
    public static int myPoints;

    //lives that the player has
    public static int lives;

    //reference to the DialogueSystem (another component in the same GameObject)
    private DialogueSystem dialogueSys;
    //reference to the UIController (another component in the same GameObject)
    private UIController ui;

    //true if the game is stopped at an event (e.g. cutscene), false if not
    private static bool isInEvent;

    //holds the current (or the last event) that happened in game
    private DialogueLine currentEvent;

    //reference to allObstacles - this is used to make the player temporarily invincible after
    //hitting an obstacle, so the game does not stop/break
    public GameObject allObstacles;

    // Start is called before the first frame update
    void Start()
    {
        myPoints = 0;

        lives = 4;

        levelDuration = 120;

        dialogueSys = GetComponent<DialogueSystem>();
        ui = GetComponent<UIController>();

        isInEvent = false;

        gameRunning = true;
    }

    // Update is called once per frame
    void Update()
    {
        //you already know this first part of the code
        //it was in the PlayerBehaviour

        //checks the conditions to make sure the game keeps running and stops
        //if the player died or time ran out
        if (gameRunning)
        {
            levelDuration -= Time.deltaTime;

            if ((lives <= 0) || (levelDuration <= 0))
            {
                GameOver();
            }
        }
        //this is a new part that controls events ("cutscenes")
        //we just make sure that if we are stopped at a cutscene and the player clicks
        //the mouse, we move to the next event (or finish, if it is the final event in a sequence...)
        else
        {
            if (isInEvent)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    MoveToNextEvent();
                }
            }
        }
    }

    void GameOver()
    {
        Pause();
        ui.DisplayGameOver();
    }


    void Pause()
    {
        gameRunning = false;
        Time.timeScale = 0f;
    }

    void Unpause()
    {
        gameRunning = true;
        Time.timeScale = 1f;
        
    }






    public void StartEvent(int eventID)
    {
        Pause();

        isInEvent = true;

        currentEvent = dialogueSys.GetEvent(eventID);

        ui.DisplayEvent(currentEvent);        

    }

    private void MoveToNextEvent()
    {
        if (currentEvent.lastEventInSequence)
        {
            Unpause();
            ui.HideDialogue();
            isInEvent = false;
        }
        else
        {
            StartEvent(currentEvent.nextEvent);
        }
    }











    public void MakeInvincible()
    {
        foreach (Collider c in allObstacles.GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }
    }

    public void MakeVulnerable()
    {
        foreach (Collider c in allObstacles.GetComponentsInChildren<Collider>())
        {
            c.enabled = true;
        }
    }
}
