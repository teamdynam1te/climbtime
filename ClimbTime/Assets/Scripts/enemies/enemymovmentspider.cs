﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymovmentspider : MonoBehaviour
{
    public float maxJumpHeight = 4f; //max jump height
    public float minJumpHeight = 1f; //min jump height
    public float timeToApex = .4f; //time to reach jump height
    float accelTimeAir = .2f; //acceleration speed in the air
    float accelTimeGround = .1f; //acceleration speed on the ground
    public float moveSpeed = 12f;
    [Range(-1.0f, 1.0f)]
    public float moveX = 0f;
    public float Timer = 2f;
    private float actualTimer;




    //custom velocity and gravity settings
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float gravity;
    float velocitySmoothing;



    Controller2D controller; //reference to Controller2D Script

    void Start()
    {

        controller = GetComponent<Controller2D>();

        //equation for turning movement in to velocity for movement
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        actualTimer = Timer;
    }

    // Update is called once per frame
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





        Vector2 input = new Vector2(moveX, 0);              // -1 left +1 right

        actualTimer -= Time.deltaTime;



        if (actualTimer <= 0 && controller.collisions.below)
        {
           Debug.Log("is jumping");
           velocity.y = maxJumpVelocity;
            actualTimer = Timer;
        }




        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocitySmoothing, (controller.collisions.below) ? accelTimeGround : accelTimeAir);
        velocity.y += gravity * Time.deltaTime;

        //movement and acceleration
        controller.Move(velocity * Time.deltaTime);
    }
}
