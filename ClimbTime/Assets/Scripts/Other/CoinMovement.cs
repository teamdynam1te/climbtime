using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class CoinMovement : MonoBehaviour
{
    public float timeToApex = .4f; //time to reach jump height
    float accelTimeAir = .2f; //acceleration speed in the air
    float accelTimeGround = .1f; //acceleration speed on the ground
    public float moveSpeed = 12f;
    [Range(-1, 1)]
    public float moveX;
    public GameObject particleSys;

    public float jumpHeight;
    

    [Header("PlayerSettings")]
    
    Vector3 velocity;
    public float gravity;
    float velocitySmoothing;
    Vector2 facingDir;

    public GameObject plr;
    public playerHealth plrhealth;
    public int damage;

    Controller2D controller; //reference to Controller2D Script

    public float[] randomMoveXDir;
    public float moveXDir;
    public float[] RandomMoveXSpeed;



    void Start()
    {
        controller = GetComponent<Controller2D>();

        //equation for turning movement in to velocity for movement
        // gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        plr = GameObject.FindGameObjectWithTag("Player");
        plrhealth = plr.GetComponent<playerHealth>();
        randomMoveXDir[0] = -0.35f;
        randomMoveXDir[1] = 0.35f;
        RandomMoveXSpeed[0] = -0.25f;
        RandomMoveXSpeed[1] = -0.35f;
        RandomMoveXSpeed[2] = 0.25f;
        RandomMoveXSpeed[3] = 0.35f;
        

        moveXDir = Random.Range(randomMoveXDir[0], randomMoveXDir[1]);
        if (moveXDir > 0)
        {
            moveX = randomMoveXDir[1] + Random.Range(RandomMoveXSpeed[2], RandomMoveXSpeed[3]);
            
        }
        else
        {
            moveX = randomMoveXDir[0] - Random.Range(RandomMoveXSpeed[0], RandomMoveXSpeed[1]);
        }
        
    }

    void FixedUpdate()
    {
        Move();


    }


    private void Move()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0; //stops accumulation of gravity
        }

        Vector2 input = new Vector2(moveX, Input.GetAxisRaw("Vertical"));

        if (controller.collisions.below)
        {
            Jump(jumpHeight);
            jumpHeight = jumpHeight / 1.25f;
        }

        if(jumpHeight <= 0.832f)
        {
            jumpHeight = 0;
            moveX = 0;
        }
   


        //movement and acceleration
        controller.Move(velocity * Time.deltaTime);
        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocitySmoothing, (controller.collisions.below) ? accelTimeGround : accelTimeAir);
        velocity.y += gravity * Time.deltaTime;
    }


    public void Jump(float jumpVelocity)
    {
        velocity.y = jumpVelocity;
    }


}
