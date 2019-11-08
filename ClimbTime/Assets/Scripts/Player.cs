using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class Player : MonoBehaviour
{
    public float maxJumpHeight = 4f; //max jump height
    public float minJumpHeight = 1f; //min jump height
    public float timeToApex = .4f; //time to reach jump height
    float accelTimeAir = .2f; //acceleration speed in the air
    float accelTimeGround = .1f; //acceleration speed on the ground
    public float moveSpeed = 12f;
    public float dashDist = 5f;
    public float dashTime = 1.5f;

    public enum movementStates {regMovement, dashing, hook };
    movementStates moveState;

    //custom velocity and gravity settings
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float gravity;
    float velocitySmoothing;

    Controller2D controller; //reference to Controller2D Script

    void Start()
    {
        moveState = movementStates.regMovement;
        controller = GetComponent<Controller2D>();

        //equation for turning movement in to velocity for movement
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
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

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            Debug.Log("is dashing");
            moveState = movementStates.dashing;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Debug.Log("is regMovement");
            moveState = movementStates.regMovement;
        }


        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            Debug.Log("is jumping");
            velocity.y = maxJumpVelocity;
        }


        //input for min jump height
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }

        switch(moveState)
        {
            case movementStates.regMovement:

                float targetVelocityX = input.x * moveSpeed;
                velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocitySmoothing, (controller.collisions.below) ? accelTimeGround : accelTimeAir);
                velocity.y += gravity * Time.deltaTime;

                break;

            case movementStates.dashing:

                if (input.x > 0)
                {
                    Vector3 dash = new Vector3(dashDist, 0, 0);
                    //float dashVelocityX = input.x * dashDist;
                    gameObject.transform.Translate(dash);
                }
                else if (input.x < 0)
                {
                    Vector3 dash = new Vector3(-dashDist, 0, 0);
                    //float dashVelocityX = input.x * dashDist;
                    gameObject.transform.Translate(dash);
                }
                break;

            case movementStates.hook:

                //hook

                break;
        }
        //movement and acceleration
        controller.Move(velocity * Time.deltaTime);
    }
}
