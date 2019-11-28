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
    public Transform batTarget = null;

    //custom velocity and gravity settings
    public float JumpHeight;
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
   public float gravity;
    float velocitySmoothing;
 


    public bool MoveRight;
    public bool canmove;
   public float batGravity;

    Vector3 batTargetPos = Vector3.zero;
    Vector3 batDirection = Vector3.zero;


    CircleCollider2D detectioncollider;
    Controller2D controller; //reference to Controller2D Script

  
    void Start()
    {

        controller = GetComponent<Controller2D>();
        detectioncollider = GetComponentInChildren<CircleCollider2D>();

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
                if(canmove == true)
                {
                Vector3 thispostion = transform.position;
                Vector3 otherpostion = target.position;
                Vector3 direction = otherpostion - thispostion;
                direction.Normalize();
                velocity.x = direction.x * moveSpeed;
                }

                

                


                break;
            case Enemytype.skeleton:
                // do skeleton stuff here
                // if move right bool is true means he will move to the right
               if(MoveRight)
                {
                    moveX = -1;
                }
                else
                {
                    moveX = 1;
                }


                break;
            case Enemytype.bat:
                // do bat stuff here                               

                gravity = batGravity;

                float obstacalDistance = 999999999f;
                float playerDistance = 999999999f;

                foreach(LayerMask mask in controller.collisionMask)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, batDirection, detectioncollider.bounds.extents.x, mask);

                    if (hit)
                    {
                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
                        {
                            Debug.DrawRay(transform.position, batDirection * detectioncollider.bounds.extents.x, Color.red);
                            //canmove = false;   
                            obstacalDistance = hit.distance;

                        }

                        if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                        {
                            Debug.DrawRay(transform.position, batDirection * detectioncollider.bounds.extents.x, Color.red);
                            //canmove = false;   
                            playerDistance = hit.distance;

                        }
                    }
                }

                if(obstacalDistance < playerDistance)
                {
                    canmove = false;
                }

                if (canmove == true)
                {
                    batTargetPos = target.position;
                }
                else if (canmove == false)
                {
                    batTargetPos = batTarget.position;
                }

                batDirection = batTargetPos - transform.position;
                batDirection.Normalize();


                velocity.x = batDirection.x * moveSpeed;
                velocity.y = batDirection.y * moveSpeed;

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
