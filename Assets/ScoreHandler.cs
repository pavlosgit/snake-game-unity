/**
 * File: ScoreHandler.cs
 *
 * This file contains the script for handling the score. A vital part of this code is the PlayerPrefs class. 
 * It's a class that helps with storing non sensitive information about the players game between sessions. This was used to save high score even if the game is shut down.
 *
 * Version 1
 * Authors: Lawend Mardini, Filip Eriksson, Pavlos Papadopoulos
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreHandler : MonoBehaviour
{   
    //Local variables for tracking the score
    private static int scoreValue = 0;
    private static int highest = 0;
    //Creates a handle in Unity for the UI text object for the score
    public TMP_Text score;
    //Creates a handle in Unity for the UI text object for the high score
    public TMP_Text h_score;


    //Runs in the beginning
    private void Start()
    {
        //Acquire the saved high score if possible
        if (PlayerPrefs.HasKey("h_score"))
        {
            highest = PlayerPrefs.GetInt("h_score");

        }
    }
    //Updates the text objects at every frame of the game
    private void Update()
    {
        score.SetText("Score: " + scoreValue);
        h_score.SetText("High Score:" + highest);
    }

    //These three functions are given as public to allow the other scripts to manage the score based on the events that occur.
    //Add a given amount to the score, the argument amount should only be 1 or 2
    public static void AddScore(int amount)
    {
        scoreValue += amount;

    }
    // Returns the scoreValue
    public static int GetScore()
    {
        return scoreValue;
    }
    //Should run when snake has died and then it's checked if the high score should be updated. This is also then saved in Playerprefs.
    public static void ResetScore()
    {
        if (scoreValue > highest)
        {
            highest = scoreValue;
            PlayerPrefs.SetInt("h_score", highest);
        }


        scoreValue = 0;
    }
}
