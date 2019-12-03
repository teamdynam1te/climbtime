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
    
    Vector3 velocity;
    public float gravity;
    Controller2D controller; //reference to Controller2D Script

    public float moveXDir;
    public float minSpeed;
    public float maxSpeed;
    public float maxJump;


    void Start()
    {
        controller = GetComponent<Controller2D>();

        //equation for turning movement in to velocity for movement
        // gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);

        while (moveXDir == 0)
        {
            moveXDir = Random.Range(-1f, 1f);
        }

        moveSpeed = Random.Range(minSpeed, maxSpeed);
        moveX = moveXDir * moveSpeed;
        velocity.y = Random.Range(0f, maxJump);

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



        if (jumpHeight <= 0.832f)
        {
            jumpHeight = 0;
            moveX = 0;
        }

        if (controller.collisions.left || controller.collisions.right)
        {
            moveX = -moveX;
        }


        //movement and acceleration
        velocity.x = moveX;
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    public void Jump(float jumpVelocity)
    {
        velocity.y = jumpVelocity;
    }


}
