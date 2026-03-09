/**
 * File: Snake.cs
 *
 * This file contains the script for handling the snake. It handles the movement and the rendering of the sprites on the snake. 
 * It also handles collision and growing of the snake lightly.
 *
 * Version 1
 * Authors: Lawend Mardini, Filip Eriksson, Pavlos Papadopoulos
 */

using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    //Direction is the direction the snake is moving. 
    //It is set to zero at first as it only starts moving once a direction is provided through the arrow keys.
    private Vector2 direction = Vector2.zero;
    
    //A list containt all the prefabs (essentially the tail of the snake)
    private List<Transform> segments;
    //Prefab is a predefined shell of a game object which is an asset in the project
    public Transform segmentPrefab;
    
    //Sets the initial size of the snake
    public int initialSize = 4;

    // Connects to sprite renderer that allows the food object to take on different sprites
    private SpriteRenderer spriteRenderer;

    // Creates a variable that later connects to the time manager script
    private TimeManager timeManager;

    //Creates a variable that later connects to the scene handler script
    private SceneHandle sceneHandle;

    // Creates a variable that later connects to the food script
    private Food food;
    // Variable that checks if the snake is allowed to move
    private bool canMove = true; 

    // Arrays to hold the individual animation frames (Assigned in unity inspector)
    public Sprite[] snakeEatingUp;  
    public Sprite[] snakeEatingDown;  
    public Sprite[] snakeEatingLeft;  
    public Sprite[] snakeEatingRight;  

    // Speed of the animation
    public float framesPerSecond = 10f;  

    // Current frame index of the animation
    private int currentFrameIndex = 0;

    //Grows snake at the start of the game to the initialSize
    private void fillToInitialSize()
    {
        for (int i = 1; i < initialSize; i++)
        {
            //Instantiates the prefab object and adds it to the list of segments
            Transform segment = Instantiate(segmentPrefab);
            //The new segments have a cascading position to the right 
            segment.position = (segments[segments.Count - 1].position - Vector3.left);
            segments.Add(segment);
        }
    }
    
    // Runs before the first frame
    private void Start()
    {
        //Instiaties the list of all the segments connected to the snake
        segments = new List<Transform>();
        segments.Add(this.transform);
        fillToInitialSize();

        //find time manager
        timeManager = FindObjectOfType<TimeManager>();
        //find scene Handler
        sceneHandle = FindObjectOfType<SceneHandle>();
        //find food
        food = FindObjectOfType<Food>();

        // Get the SpriteRenderer component from the first segment
        spriteRenderer = this.GetComponent<SpriteRenderer>();


    }

    //Update runs for every frame of the program
    private void Update()
    {
        // Check if the snake can move this frame
        // Due to a bug that allowed the snake to move multiple times in a frame (Which would result in game over), we had to limit movements to once a frame, which is done through canMove
        if (canMove) 
        {

            //This section checks keypresses, and changes direction based on keypresses, as long as it is not the new direction is not opposite of the current direction
            // This is because a snake cannot turn towards the direction it is currently coming from in one move due to the tail

            // In each of the if cases, the direction is changed, the food.gameOver is changed to true to essentially start the game and allow for game over, and it also disables the movement for the current frame
    
            if (Input.GetKeyDown(KeyCode.UpArrow) && direction != Vector2.down)
            {
                direction = Vector2.up; //Changes direction
                food.gameOver = true; //Allows for loss
                canMove = false; // Disable movement for this frame
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow) && direction != Vector2.up)
            {
                direction = Vector2.down;
                food.gameOver = true;
                canMove = false; // Disable movement for this frame
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) && direction != Vector2.left)
            {
                direction = Vector2.right;
                //This should be disabled, as moving right at the start is not possible and does nothing
                //food.gameOver = true;
                canMove = false; // Disable movement for this frame
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) && direction != Vector2.right)
            {
                direction = Vector2.left;
                food.gameOver = true;
                canMove = false; // Disable movement for this frame
            }
            // This checks for escape keypresses when game is not active. This allows the user to go back to the main menu.
            else if (Input.GetKeyDown(KeyCode.Escape) && (food.gameOver == false))
            {
                // Through the sceneHandle, call the function MainMenu, which loads the main menu scene
                sceneHandle.MainMenu();
            }
        }
    }

    //Runs for a designated time interval, defined in unity. Is used for game physics
    private void FixedUpdate()
    {
        // Moves the snake forward in the direction of the head
        for (int i = segments.Count - 1; i > 0; i--)
        {
            segments[i].position = segments[i - 1].position;
        }

        // Move the head
        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + direction.x,
            Mathf.Round(this.transform.position.y) + direction.y,
            0.0f
        );

        // Update the sprite of the head
        UpdateSpriteDirection();
        // Update the sprite of the tail segments
        UpdateTailSprites();
        UpdateLastTailSprite();

        canMove = true; // Enable movement for the next frame
            
    }


    //This segment of functions are conneted to updating sprites on runtime. 
    //This is made by using the current direction and also the previous direction to determine if the snake has turned.

    //This function in particular determines what sprite should be generated at each part of the tail (segments list) 
    private void UpdateTailSprites()
    {
        for (int i = 1; i < segments.Count; i++)
        {
            Transform currentSegment = segments[i];
            Transform nextSegment = segments[i - 1];

            Vector2 relativeDirection = currentSegment.position - nextSegment.position;

            // Check for direction changes to determine the sprite
            Vector2 nextDirection = i < segments.Count - 1 ? segments[i + 1].position - currentSegment.position : direction;

            if (relativeDirection == Vector2.up)
            {
                if (nextDirection == Vector2.left)
                {
                    currentSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_corner_left_down"); 
                }
                else if (nextDirection == Vector2.right)
                {
                    currentSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_corner_right_down"); 
                }
                else
                {
                    currentSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_vertical");
                }
            }
            else if (relativeDirection == Vector2.down)
            {
                if (nextDirection == Vector2.left)
                {
                    currentSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_corner_left_up"); 
                }
                else if (nextDirection == Vector2.right)
                {
                    currentSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_corner_right_up"); 
                }
                else
                {
                    currentSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_vertical");
                }
            }
            else if (relativeDirection == Vector2.left)
            {
                if (nextDirection == Vector2.up)
                {
                    currentSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_corner_right_up"); 
                }
                else if (nextDirection == Vector2.down)
                {
                    currentSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_corner_right_down"); 
                }
                else
                {
                    currentSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_horizontal");
                }
            }
            else if (relativeDirection == Vector2.right)
            {
                if (nextDirection == Vector2.up)
                {
                    currentSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_corner_left_up"); 
                }
                else if (nextDirection == Vector2.down)
                {
                    currentSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_corner_left_down"); 
                }
                else
                {
                    currentSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_horizontal");
                }
            }
        }
    }

    //This function renders a snake tail sprite over the last segment depending on the direction the snake was moving the time the now last segment was generated
    private void UpdateLastTailSprite()
    {
        //this is the last tail block
        Transform lastSegment = segments[segments.Count - 1];
        //this is the tail block before the last
        Transform beforeLastSegment = segments[segments.Count - 2];

        //We get the direction by comparing the position of the lastsegment with the block before
        Vector2 lastDirection = lastSegment.position - beforeLastSegment.position;

        
        if (lastDirection == Vector2.up)
        {
            lastSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_tail_down"); 
        }
        else if (lastDirection == Vector2.down)
        {
            lastSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_tail_up"); 
        }
        else if (lastDirection == Vector2.left)
        {
            lastSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_tail_right"); 
        }
        else if (lastDirection == Vector2.right)
        {
            lastSegment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_tail_left");
        }
    }

    // Update the sprite of the new segment based on its relative direction to its previous segment
    private void UpdateSegmentSprite(Transform segment, Transform nextSegment)
    {
        Vector2 relativeDirection = nextSegment.position - segment.position;

        if (relativeDirection == Vector2.up)
        {
            segment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_vertical");
        }
        else if (relativeDirection == Vector2.down)
        {
            segment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_vertical");
        }
        else if (relativeDirection == Vector2.left)
        {
            segment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_horizontal");
        }
        else if (relativeDirection == Vector2.right)
        {
            segment.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snake_horizontal");
        }
    }

    // Updates the head based on the current direction
    private void UpdateSpriteDirection()
    {
        if (direction == Vector2.up)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("snake_up");
        }
        else if (direction == Vector2.down)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("snake_down");
        }
        else if (direction == Vector2.left)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("snake_left");
        }
        else if (direction == Vector2.right)
        {
            spriteRenderer.sprite = Resources.Load<Sprite>("snake_right");
        }
    }

    // This function handles the functionality for when the snake grows by a segment in size.
    public void Grow()
    {
        Transform newSegment = Instantiate(this.segmentPrefab);

        // Set the position of the new segment based on the direction of the last segment
        Vector2 newPosition = segments[segments.Count - 1].position;

        if (direction == Vector2.up)
        {
            newPosition += Vector2.down;
        }
        else if (direction == Vector2.down)
        {
            newPosition += Vector2.up;
        }
        else if (direction == Vector2.left)
        {
            newPosition += Vector2.right;
        }
        else if (direction == Vector2.right)
        {
            newPosition += Vector2.left;
        }

        newSegment.position = newPosition;

        segments.Add(newSegment);

        // Update the sprite of the new segment based on its relative direction to its previous segment
        UpdateSegmentSprite(newSegment, segments[segments.Count - 2]);
    }
    
    // Handles game over
    private void GameOver()
    {
        //Resets score
        ScoreHandler.ResetScore();

        //Destroys the snake and it's segments, then resets the snake to it's initial size and position
        for (int i = 1; i < segments.Count; i++)
        {
            Destroy(segments[i].gameObject);
        }
        //Resets the snake to the initial state
        segments.Clear();
        segments.Add(this.transform);
        this.transform.position = Vector3.zero;
        fillToInitialSize();
        direction = Vector2.zero;
        
        //resets the sprite back to normal
        spriteRenderer.sprite = Resources.Load<Sprite>("snake_left");
        timeManager.resetTime();

    }

    //Runs on collision
    private void OnTriggerEnter2D(Collider2D other)
    {
        //If snake collides with an object tagged as food it will grow by one segement, additionally, it will invoke a function to run an animation
        if (other.tag == "Food")
        {
            Grow();
            currentFrameIndex = 0;
            Invoke("EatingAnimation", 0); 
        }

        //If snake collides with an object tagged as an obstacle the game will be set in the state of game over.
        else if (other.tag == "Obstacle")
        {
            GameOver();

            food.itIsGameOver();
        }
    }


    //This will run the animation after eating a food object.
    void EatingAnimation()
    {
       // Load sprites direction of head depending on current direction
        if (direction == Vector2.up)
        {
            spriteRenderer.sprite = snakeEatingUp[currentFrameIndex];
        }
        else if (direction == Vector2.down)
        {
            spriteRenderer.sprite = snakeEatingDown[currentFrameIndex];
        }
        else if (direction == Vector2.left)
        {
            spriteRenderer.sprite = snakeEatingLeft[currentFrameIndex];
        }
        else if (direction == Vector2.right)
        {
            spriteRenderer.sprite = snakeEatingRight[currentFrameIndex];
        }

        // Move to the next frame
        currentFrameIndex++;

        // Check if animation is finished
        if (currentFrameIndex < snakeEatingUp.Length)
        {
            // Call NextFrame again if there are more frames
            Invoke("EatingAnimation", 1 / framesPerSecond);
        }
    }

}
