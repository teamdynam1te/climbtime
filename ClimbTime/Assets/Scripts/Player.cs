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
    public float dashSpeed = 48f;
    public float dashTimeDefault = 0.5f;
    public float dashCooldown = 1.5f;
    public float dashCooldownDefault = 1.5f;
    public float dashTime = 0.5f;
    public float hookSpeed = 60f;
    public float hookDist = 5f;

    public enum movementStates {regMovement, dashing, hook};
    movementStates moveState;

    //custom velocity and gravity settings
    float maxJumpVelocity;
    float minJumpVelocity;
    Vector3 velocity;
    float gravity;
    float velocitySmoothing;
    Vector2 facingDir;
    Vector2 hookDirOnClick;
    Vector3 worldMousePos;

    public Transform crossHair;
    public SpriteRenderer crossHairSprite;
    private Vector2 playerPos;
    public Transform shootPoint;

    //look at enum 
    public bool arenaCheck;
    public bool mountainCheck;

    //enum stateCheck { }

    bool canDash = true; //checks to see if can dash

    Controller2D controller; //reference to Controller2D Script

    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    void Start()
    {
        moveState = movementStates.regMovement;
        controller = GetComponent<Controller2D>();

        //equation for turning movement in to velocity for movement
        gravity = -(2 * maxJumpHeight) / Mathf.Pow(timeToApex, 2);
        maxJumpVelocity = Mathf.Abs(gravity) * timeToApex;
        minJumpVelocity = Mathf.Sqrt(2 * Mathf.Abs(gravity) * minJumpHeight);

        playerPos = transform.position;
    }

    void FixedUpdate()
    {
        Move();
        CheckCanDash();
    }

    void Update()
    {
        if (mountainCheck == true)
        {
            SetCrosshairPosition();
            CheckCanHook();
        }
    }

    private void CheckCanHook()
    {
        if (mountainCheck == true)
        {
            worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            facingDir = worldMousePos - transform.position;

            LayerMask mask = LayerMask.GetMask("Obstacles");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDir, hookDist, mask);
            Debug.DrawRay(transform.position, facingDir, Color.red);

            if (Input.GetMouseButtonDown(0) && hit)
            {
                Debug.Log("Button Down");
                hookDirOnClick = facingDir;
                moveState = movementStates.hook;
            }
            if (Input.GetMouseButtonUp(0))
            {
                moveState = movementStates.regMovement;
            }
        }
        if (mountainCheck == false)
        {
            return;
        }
    }

    private void CheckCanDash()
    {
        if (canDash == false)
        {
            dashCooldown -= Time.deltaTime;

            if (dashCooldown <= 0)
            {
                canDash = true;
                dashCooldown = dashCooldownDefault;
            }
        }
    }

    private void Move()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0; //stops accumulation of gravity
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (mountainCheck == true)
        {
            if (Input.GetKey(KeyCode.LeftShift) && canDash)
            {
                Debug.Log("is dashing");
                moveState = movementStates.dashing;
                dashTime -= Time.deltaTime;

                if (dashTime <= 0)
                {
                    canDash = false;
                    dashTime = dashTimeDefault;
                    moveState = movementStates.regMovement;
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Debug.Log("is regMovement");
                moveState = movementStates.regMovement;
            }
        } //dashing only works when mountain

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

                velocity = new Vector3(input.x * dashSpeed, 0); //change to 0 if dont like feel
                break;

            case movementStates.hook:

                velocity = hookDirOnClick * hookSpeed;
                if (controller.collisions.above || controller.collisions.below)
                {
                    velocity.x = 0; //stops accumulation of gravity
                }
                if (controller.collisions.left || controller.collisions.right)
                {
                    velocity.y = 0;
                }
                break;
        }
        //movement and acceleration
        controller.Move(velocity * Time.deltaTime);
    }

    private void SetCrosshairPosition() // cross hair aiming
    {
        if (mountainCheck == true)
        {
            var aimAngle = Mathf.Atan2(facingDir.y, facingDir.x);
            if (aimAngle < 0f)
            {
                aimAngle = Mathf.PI * 2 + aimAngle;
            }

            if (!crossHairSprite.enabled)
            {
                crossHairSprite.enabled = true;
            }

            var x = transform.position.x + 2f * Mathf.Cos(aimAngle);
            var y = transform.position.y + 2f * Mathf.Sin(aimAngle);

            var crossHairPosition = new Vector3(x, y, 0);
            crossHair.transform.position = crossHairPosition;
        }
        if(mountainCheck == false)
        {
            if (crossHairSprite.enabled)
            {
                crossHairSprite.enabled = false;
            }
        }
    } 
}
