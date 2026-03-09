/**
 * File: Food.cs
 *
 * This file contains the script for the food object in c#
 *
 * Version 1
 * Authors: Lawend Mardini, Filip Eriksson, Pavlos Papadopoulos
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    // An enum containing the different kinds of food
    public enum FoodType
    {
        base_apple,
        double_banana,
        speedy_watermelon
    }

    // Connects to game object grid area by Unity'
    //Grid area is the area where the game occurs and is defined as an object in Unity
    public BoxCollider2D gridArea;

    // Connects to sprite renderer that allows the food object to take on different sprites
    public SpriteRenderer spriteRenderer;

    // Creates a variable that later connects to the snake script
    private Snake snake;

    // Creates a variable that later connects to the time manager script
    private TimeManager timeManager;

    // A bool that determines if the game is over or not
    public bool gameOver;
    // Variables that connect to audio sources
    public AudioSource soundEffects;
    public AudioClip eating;
    public AudioClip gameOverSound;

    //Variables for tracking the state of the food
    //The Foodtypes can be A basic apple that grows size of snake by one, a banana that grows the size by two and a watermelon grows the size by one
    [HideInInspector] //HideInInspector hides public variables (below) from the unity inspector
    public FoodType foodType;

    public static int foodCount;

    // Variables for items
    public static int itemCount;
    public GameObject itemPrefab;
    private Item itemInstance;
    

    // Runs before the first frame
    private void Start()
    {
        // Connects the Snake class to the empty variable snake
        snake = FindObjectOfType<Snake>();

        // Connects the TimeManager class to the empty variable timeManager
        timeManager = FindObjectOfType<TimeManager>();

        //Initialize the count variables
        foodCount = 0;
        itemCount = 0;

        // Randomize foodType and position
        foodTypeRandomizer();

        RandomizePosition();
    }

    //Manages the sprite for the food based on the current food type. 
    private void SetSprite()
    {
        //Start with resetting the sprite
        spriteRenderer.sprite = null;

        // Switch case depending on foodType (determined in foodTypeRandomizer)
        switch (foodType)
        {
            case FoodType.base_apple:
                // Set sprite for base_apple
                spriteRenderer.sprite = Resources.Load<Sprite>("apple");
                break;

            case FoodType.double_banana:
                // Set sprite for banana
                spriteRenderer.sprite = Resources.Load<Sprite>("banana"); 
                break;

            case FoodType.speedy_watermelon:
                // Set sprite for watermelon
                spriteRenderer.sprite = Resources.Load<Sprite>("watermelon");
                break;

            default:
            //Log if something unexpected occurs
                Debug.Log("Error, foodtype was not found");
                break;
        }
    }
    
    //Randomizes the position for the food within the previously defined grid
    public void RandomizePosition()
    {
        // Creates bounds based on the gridArea provided through the unity inspector
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);
        
        //Creates new position based on the randomized 2D values
        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }
    
    //Randomizes the FoodType based on a defined set of probabilities. 20% for banana 10% for watermelon and 70% for a basic apple
    private void foodTypeRandomizer()
    {
        // A random number generator from 0 to 99
        int randomNumber = Random.Range(0, 100); 

        if (randomNumber < 20)
        {
            foodType = FoodType.double_banana;
        }
        else if (randomNumber > 89)
        {
            foodType = FoodType.speedy_watermelon;
        }
        else
        {
            foodType = FoodType.base_apple;
        }

        //Set the sprite according to randomization
        SetSprite();
    }
    
    
    // Creates an itemObject (Powerup)
    private void itemHandler()
    {
        //Should not get here but if it does, reset the itemInstance
        if (itemInstance != null)
        {
            Destroy(itemInstance.gameObject);
            itemInstance = null;
        }

        //Create the instance of the item
        GameObject itemObject = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
        itemInstance = itemObject.GetComponent<Item>();

        // Error handler for if itemInstance == null
        if (itemInstance == null)
        {
            Debug.LogError("Item component not found on instantiated prefab!");
        }
        else
        {
            //Randomize the position for the item
            itemInstance.gridArea = gridArea; // Ensure the gridArea is set for the item
            itemInstance.RandomizePosition(); // Randomize position after instantiation
        }
    }

    // Handles the logic for when to create an item.
    private void itemSpawn()
    {
        // checks if there have been 7 points since last item and makes sure that there is no current item
        if ((foodCount - itemCount) >= 7 && itemInstance == null)
            {
                // Resets the counter for when to spawn an item and simultaneously spawns an item
                itemCount = foodCount;
                itemHandler();
            }
    }

    // Runs on collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player (snake)
        if (other.tag == "Player")
        {
            // Play the eating sound effect
            soundEffects.PlayOneShot(eating); 

            // Check the food type and handle accordingly
            switch (foodType)
            {
                //If apple: adds score by 1, foodcount by 1 and resets speed
                case FoodType.base_apple:
                    ScoreHandler.AddScore(1);
                    foodCount++; 
                    timeManager.resetTime(); // Resets speed
                    break;

                //If banana: adds score by 2, foodcount by 2 and resets the speed
                // Since the handling of a snake growing in size is handled in the snake script, it is done everytime a foodtype is "eaten". 
                // And Since banana wants to grow the size of the snake by 2, we have to let snake.Grow() be called once in the snake script and then once again call it from this script
                case FoodType.double_banana:
                    ScoreHandler.AddScore(2);
                    foodCount += 2;
                    snake.Grow();
                    timeManager.resetTime(); // Resets speed
                    break;

                //If watermelon: adds score by 1, foodcount by 1 and resets speed
                case FoodType.speedy_watermelon:
                    ScoreHandler.AddScore(1);
                    foodCount++;
                    timeManager.resetTime(); // Resets speed
                    break;

                default:
                    break;
            }

            // Once a foodtype has been collided, it randomizes the position of the foodObject and the type of the food
            RandomizePosition();
            foodTypeRandomizer();

            // Additionally, it attempts to spawn an item
            itemSpawn();
        }
    }

    // Handles game over
    public void itIsGameOver()
    {
        //gameOver bool is a flag that prevents repetition of this function being called
        if(gameOver)
        {
            // Plays a sound effect 
            soundEffects.PlayOneShot(gameOverSound); 
            // Randomizes position of the food for the next run
            RandomizePosition();

            // When game over occurs, the first foodType is set to be apple
            foodType = FoodType.base_apple;
            SetSprite();

            //resets scores
            foodCount = 0;
            itemCount = 0;

            // destroys the item
            if (itemInstance != null)
            {
                Destroy(itemInstance.gameObject);
                itemInstance = null;
            }
        }   

        // Resets the gameOver flag
        gameOver = false;
        
    }
}
