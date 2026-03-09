/**
 * File: Item.cs
 *
 * This file contains the script for the item object in c#
 *
 * Version 1
 * Authors: Lawend Mardini, Filip Eriksson, Pavlos Papadopoulos
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // An enum containing the different kinds of items
    public enum ItemType
    {
        speed_up,
    }

    // Connects to game object grid area by Unity'
    //Grid area is the area where the game occurs and is defined as an object in Unity
    public BoxCollider2D gridArea;
    
    // Connects to sprite renderer that allows the item object to take on different sprites
    public SpriteRenderer spriteRenderer;

    // Creates a variable that later connects to the snake script
    //Isnt used
    private Snake snake;

    // Creates a variable that later connects to the time manager script
    private TimeManager timeManager;

    //Variables for tracking the state of the item
    //The itemType can be speed up clock that makes the snake move faster
    [HideInInspector] //HideInInspector hides public variables (below) from the unity inspector
    public ItemType itemType;

    // Runs before the first frame
    private void Start()
    {
        // Connects the Snake class to the empty variable snake (Does nothing currently)
        snake = FindObjectOfType<Snake>();

        // Connects the TimeManager class to the empty variable timeManager
        timeManager = FindObjectOfType<TimeManager>();

        // Randomize itemType and position
        itemTypeRandomizer();

        RandomizePosition();
    }

    //Manages the sprite for the item based on the current item type. 
    private void SetSprite()
    {
        //Start with resetting the sprite
        spriteRenderer.sprite = null;

        // Switch case depending on itemType (determined in itemTypeRandomizer)
        switch (itemType)
        {
            case ItemType.speed_up:
                // Set sprite for speedup 
                spriteRenderer.sprite = Resources.Load<Sprite>("speedup"); // Replace with sprite path
                break;

            default:
            //Log if something unexpected occurs
                Debug.Log("Error, item type was not found");
                break;
        }
    }

    //Randomizes the position of the item within the previously defined grid
    public void RandomizePosition()
    {
        // Creates bounds based on the gridArea provided through the unity inspector
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

         //Creates new position based on the randomized 2D values
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    //Randomizes the itemType based on a defined set of probabilities. 
    private void itemTypeRandomizer()
    {
        // A random number generator from 0 to 99
        int randomNumber = Random.Range(0, 100); 

        if (randomNumber < 20)
        {
            //Should be replaced in the future with other items
            itemType = ItemType.speed_up;
        }
        else if (randomNumber > 89)
        {
            //Should be replaced in the future with other items
            itemType = ItemType.speed_up;
        }
        else
        {
            itemType = ItemType.speed_up;
        }

        //Set the sprite according to randomization
        SetSprite();
    }

// Runs on collision
private void OnTriggerEnter2D(Collider2D other)
{
    // Check if the colliding object is the player
    if (other.tag == "Player")
    {
        // Check the item type and handle accordingly
        switch (itemType)
        {
            // If speed up: Call watermelonTime function in timeManager 
            case ItemType.speed_up:
                // (This function essentially speeds up the rate of the game)
                timeManager.watermelonTime();
                break;

            default:
                break;
        }
        Destroy(gameObject); // Destroy the item after handling
    }
}
}
