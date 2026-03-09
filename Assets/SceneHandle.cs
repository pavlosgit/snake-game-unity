/**
 * File: SceneHandle.cs
 *
 * This file handles the switching of scenes of the whole game.
 *   essentially, Allows unity buttons to be assigned these functions which change the scene.
 *
 * Version 1
 * Authors: Lawend Mardini, Filip Eriksson, Pavlos Papadopoulos
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandle : MonoBehaviour
{
    // Assigned to the button in the main menu and the tutorial (Through unity inspector). 
    // Once pressed the scene will be changed to Game, and the game will begin.
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Game");
    }

    // Assigned to the button in the tutorial (Through unity inspector). 
    // Once pressed the scene will be changed to the main menu.
    public void MainMenu()
    {
        SceneManager.LoadSceneAsync("Main menu");
    }

    // Assigned to the button in the main menu (Through unity inspector). 
    // Once pressed the scene will be changed to the tutorial.
    public void Tutorial()
    {
        SceneManager.LoadSceneAsync("Tutorial");
    }

    // Assigned to the button in the main menu (Through unity inspector). 
    // Once pressed the application will quit.
    public void QuitGame()
    {
        Application.Quit();
    }
}
