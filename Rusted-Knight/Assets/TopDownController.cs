using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class TopDownController : MonoBehaviour
{

    public Rigidbody2D body;

    public SpriteRenderer spriteRend;

    public List<Sprite> upSprites;
    public List<Sprite> downSprites;
    public List<Sprite> rightSprites;
    public List<Sprite> leftSprites;

    public float walkspeed;
    public float runningMultiplier;
    public float framerate;

    float idleTime;
    Vector2 direction;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        WalkingExecution();
    }

    void WalkingExecution()
    {
        //Gets the general walking direction of the player based on input
        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        //Returns the unit vector to ensure movement isn't faster diagonally
        direction = direction.normalized;


        //Stops movement during paused sections such as dialogue and fighting.
        if (PauseController.IsGamePaused)
        {
            body.linearVelocity = Vector2.zero;
        }
        else
        {
            runningCommand();
            body.linearVelocity = direction * walkspeed;

            List<Sprite> directionSprites = getDirectionSprites();

            if (directionSprites != null)
            {
                float walkDuration = Time.time - idleTime;

                int frame = (int)((walkDuration * framerate) % directionSprites.Count);

                spriteRend.sprite = directionSprites[frame];
            }
            else
            {
                idleTime = Time.time;
            }
        }

    }

    List<Sprite> getDirectionSprites()
    {

        List<Sprite> directionSprite = null;

        bool movingUp = direction.y > 0;
        bool movingDown = direction.y < 0;
        bool movingRight = direction.x > 0;
        bool movingLeft = direction.x < 0;

        if (movingUp)
        {
            directionSprite = upSprites;
        }
        else if (movingDown)
        {
            directionSprite = downSprites;
        }
        else if (movingRight)
        {
            directionSprite = rightSprites;
        }
        else if (movingLeft)
        {
            directionSprite = leftSprites;
        }

        return directionSprite;
    }

    void runningCommand()
    {
        if (Input.GetKeyDown("left shift"))
        {
            walkspeed *= runningMultiplier;
            framerate *= runningMultiplier;
        }
        else if (Input.GetKeyUp("left shift"))
        {
            walkspeed /= runningMultiplier;
            framerate /= runningMultiplier;
        }
    }

}
