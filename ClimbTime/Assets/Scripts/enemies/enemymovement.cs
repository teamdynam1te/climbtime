using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemymovement : MonoBehaviour
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
    public enum Enemytype { spider, skeleton, bat}

    public Enemytype enemytype;
    public Transform target;


    //custom velocity and gravity settings
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float gravity;
    float velocitySmoothing;

    

    public bool MoveRight;

    





    Controller2D controller; //reference to Controller2D Script

    void Start()
    {

        controller = GetComponent<Controller2D>();

        //equation for turning movement in to velocity for movement
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        actualTimer = Timer;

        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();


    }


    // Update is called once per frame
    void FixedUpdate()
    {
        Move();


    }

    private void OnTriggerEnter2D(Collider2D trig)
    {
        if(trig.gameObject.CompareTag("turn"))
        {
            if(MoveRight)
            {
                MoveRight = false;
            }
            else
            {
                MoveRight = true;
            }
        }
    }



    private void Move()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0; //stops accumulation of gravity
        }





        

       

        switch(enemytype)
        {
            case Enemytype.spider:
                // do spider stuff here
                actualTimer -= Time.deltaTime;
                if (actualTimer <= 0 && controller.collisions.below)
                {
                   
                    velocity.y = maxJumpVelocity;
                    actualTimer = Timer;
                }

                Vector3 thispostion = transform.position;
                Vector3 otherpostion = target.position;
                Vector3 direction = otherpostion - thispostion;
                direction.Normalize();
                velocity.x = direction.x * moveSpeed;

                


                break;
            case Enemytype.skeleton:
                // do skeleton stuff here
                // if move right bool is true means he will move to the right
               if(MoveRight)
                {
                    transform.Translate(2 * Time.deltaTime * moveSpeed, 0, 0);
                    transform.localScale = new Vector2(1, 3);
                }
                else
                {
                    transform.Translate(-2 * Time.deltaTime * moveSpeed, 0, 0);
                    transform.localScale = new Vector2(-1, 3);
                }

             





                break;
            case Enemytype.bat:
                // do bat stuff here
                Vector3 enemypostion = transform.position;
                Vector3 playerpostion = target.position;
                Vector3 direction2 = playerpostion - enemypostion;
                direction2.Normalize();
                velocity.x = direction2.x * moveSpeed;

                actualTimer -= Time.deltaTime;
                if (actualTimer <= 0 )
                {
                    Debug.Log("is jumping");
                    velocity.y = maxJumpVelocity;
                    actualTimer = Timer;
                }

                break;
        }

        

        Vector2 input = new Vector2(moveX, 0);              // -1 left +1 right


        float targetVelocityX = input.x * moveSpeed;
        velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocitySmoothing, (controller.collisions.below) ? accelTimeGround : accelTimeAir);
        velocity.y += gravity * Time.deltaTime;

        //movement and acceleration
        controller.Move(velocity * Time.deltaTime);
    }
}
