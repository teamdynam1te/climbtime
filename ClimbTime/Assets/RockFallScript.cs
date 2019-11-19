using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Controller2D))]
public class RockFallScript : MonoBehaviour
{
    public float maxJumpHeight = 4f; //max jump height
    public float minJumpHeight = 1f; //min jump height
    public float timeToApex = .4f; //time to reach jump height
    float accelTimeAir = .2f; //acceleration speed in the air
    float accelTimeGround = .1f; //acceleration speed on the ground
    public float moveSpeed = 12f;
    [Range(-1, 1)]
    public float moveX;

    public float jumpHeight;

    [Header("PlayerSettings")]
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    public float gravity;
    float velocitySmoothing;
    Vector2 facingDir;
    
    Controller2D controller; //reference to Controller2D Script
    

    void Start()
    {
        controller = GetComponent<Controller2D>();

        //equation for turning movement in to velocity for movement
       // gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);
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
        }

        //input for min jump height
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }
        }

        
        //movement and acceleration
        controller.Move(velocity * Time.deltaTime);
        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocitySmoothing, (controller.collisions.below) ? accelTimeGround : accelTimeAir);
        velocity.y += gravity * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }


    public void Jump(float jumpVelocity)
    {
        velocity.y = jumpVelocity;
    }

   
}
