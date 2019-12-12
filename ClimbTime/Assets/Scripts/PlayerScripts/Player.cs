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

    [Header("PlayerSettings")]
    float maxJumpVelocity;
    float minJumpVelocity;
    public Vector3 velocity;
    float gravity;
    float velocitySmoothing;
    Vector2 facingDir;
    Vector2 hookDirOnClick;
    Vector3 worldMousePos;

    bool doJump = false;
    bool doHop = false;

    public Transform crossHair;
    public LineRenderer lineRenderer;
    public SpriteRenderer crossHairSprite;
    private Vector2 playerPos;
    public Transform shootPoint;
    public GameObject crossbow;
    public GameObject crosshair;
    public GameManager gm;
    public AudioSource jump;
    public AudioClip dashSFX;
    public float vol = 1f;
    public GameObject jumpParticles;
    public Transform jumpSpawnPoint;

    //enum stateCheck { }

    bool canDash = true; //checks to see if can dash

    Controller2D controller; //reference to Controller2D Script
    Animator anim;

    void Start()
    {
        moveState = movementStates.regMovement;
        controller = GetComponent<Controller2D>();
        anim = GetComponent<Animator>();
        gm = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

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
        FlipSprite();
        
        if (Input.GetKeyDown(KeyCode.Space) && controller.collisions.below)
        {
            //Debug.Log("is jumping");
            doJump = true;
            jump.Play();
        }

        if(Input.GetKeyUp(KeyCode.Space))
        {
            doHop = true;
        }
        if (gm.gameState == GameManager.GameStates.mountain)
        {
            lineRenderer.enabled = false;
            crosshair.SetActive(true);
            crossbow.SetActive(false);
            SetCrosshairPosition();
            CheckCanHook();
            CheckCanDash();
        }

        if (gm.gameState == GameManager.GameStates.arena)
        {
            crossbow.SetActive(true);
            crosshair.SetActive(false);
        }

        if (gm.gameState == GameManager.GameStates.shopping)
        {
            crossbow.SetActive(false);
        }

        

    }

    public void RemoveCrossbow()
    {
        crossbow.SetActive(false);
        crosshair.SetActive(false);
    }

    public void SetCrossbow()
    {
        crossbow.SetActive(true);
        crosshair.SetActive(false);
    }

    private void CheckCanHook()
    {
        if (gm.gameState == GameManager.GameStates.mountain)
        {
            worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
            facingDir = worldMousePos - transform.position;

            LayerMask mask = LayerMask.GetMask("Obstacles");
            RaycastHit2D hit = Physics2D.Raycast(transform.position, facingDir, hookDist, mask);

            lineRenderer.SetPosition(0, transform.position);
            lineRenderer.SetPosition(1, hit.point);

            Debug.DrawRay(transform.position, facingDir, Color.red);

            if (Input.GetMouseButtonDown(0) && hit && gm.GrappleAmmoAmount > 0)
            {
                lineRenderer.enabled = true;
                lineRenderer.SetPosition(0, transform.position);
                lineRenderer.SetPosition(1, hit.point);
                Debug.Log("Button Down");
                hookDirOnClick = facingDir;
                gm.GrappleAmmoAmount -= 1;
                moveState = movementStates.hook;
            }
            if (Input.GetMouseButtonUp(0))
            {
                lineRenderer.enabled = false;
                moveState = movementStates.regMovement;
            }
        }
    }

    private void CheckCanDash()
    {
        if (gm.gameState == GameManager.GameStates.mountain)
        {           
            if (canDash == false && gm.dashPotionAmount >= 1)
            {
                dashCooldown -= Time.deltaTime;

                if (dashCooldown <= 0)
                {
                    canDash = true;
                    dashCooldown = dashCooldownDefault;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Treasure")
        {
            gm.AddToScore(Random.Range(5, 10));
        }
    }

    private void Move()
    {
        if (controller.collisions.above || controller.collisions.below)
        {
            velocity.y = 0; //stops accumulation of gravity
        }

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (gm.gameState == GameManager.GameStates.mountain)
        {
            if (Input.GetKey(KeyCode.LeftShift) && canDash && gm.dashPotionAmount > 0)
            {
                Debug.Log("is dashing");
                AudioSource.PlayClipAtPoint(dashSFX, Camera.main.transform.position, vol);
                moveState = movementStates.dashing;
                dashTime -= Time.deltaTime;

                if (dashTime <= 0)
                {
                    canDash = false;
                    dashTime = dashTimeDefault;
                    gm.dashPotionAmount -= 1;
                    moveState = movementStates.regMovement;
                }
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                Debug.Log("is regMovement");
                moveState = movementStates.regMovement;
            }
        } //dashing only works when mountain

        if (doJump)
        {
            //Debug.Log("is jumping");
            Jump(maxJumpVelocity);
           
        }

        //input for min jump height
        if (doHop)
        {
            if (velocity.y > minJumpVelocity)
            {
                velocity.y = minJumpVelocity;
            }

            doHop = false;
        }

        switch(moveState)
        {
            case movementStates.regMovement:

                float targetVelocityX = input.x * moveSpeed;
                velocity.x = Mathf.SmoothDamp(velocity.x, targetVelocityX, ref velocitySmoothing, (controller.collisions.below) ? accelTimeGround : accelTimeAir);
                velocity.y += gravity * Time.deltaTime;

                bool PlayerHasMovement = Mathf.Abs(input.x) > Mathf.Epsilon;
                
                if(controller.collisions.below )
                {
                    anim.SetBool("Running", PlayerHasMovement);
                }
                else
                {
                    anim.SetBool("Running", false);
                }

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

    public void Jump(float jumpVelocity)
    {
        velocity.y = jumpVelocity;

        Instantiate(jumpParticles, jumpSpawnPoint.position, Quaternion.identity);
        doJump = false;
    }

    public void KnockBack(float knockbackX, float knockbackY)
    {
        velocity.x = knockbackX;
        velocity.y = knockbackY;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "enemy" && doJump == true)
        {
            Jump(25);
        }
    }

    private void FlipSprite()
    {
        bool PlayerHasMovement = Mathf.Abs(velocity.x) > Mathf.Epsilon;

        if(controller.collisions.below)
        {
            anim.SetBool("Running", PlayerHasMovement);
        }
        else
        {
            anim.SetBool("Running", false);
        }

        if(PlayerHasMovement)
        {
            transform.localScale = new Vector2(Mathf.Sign(velocity.x), 1);
            crossbow.transform.localScale = new Vector2(Mathf.Sign(velocity.x), 1);
        }
    }
    public void SetCrosshairPosition() // cross hair aiming
    {
        if (gm.gameState == GameManager.GameStates.mountain)
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
    }
}
