/**
 * File: TimeManager.cs
 *
 * Edits the speed the game is ran at
 *
 * Version 1
 * Authors: Lawend Mardini, Filip Eriksson, Pavlos Papadopoulos
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

    // Base speed of the game
    //time.fixedDeltaTime changes how often fixedUpdate is called throughout the application
    void Start()
    {
        Time.fixedDeltaTime = 0.06f;
    }

    //Resets game speed to original speed
    public void resetTime()
    {
        Time.fixedDeltaTime = 0.06f;
    }
    
    //Speeds the game speed to double the original speed
    public void watermelonTime()
    {
        Time.fixedDeltaTime = 0.03f;
    }
}
