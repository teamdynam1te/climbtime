using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour
{
    public LayerMask[] collisionMask; //reference to layermask

    const float skinWidth = .015f; //safety zone outside of sprite
    public int horizontalRayCount = 4; //number of rays horizontal
    public int verticalRayCount = 4; //number of rays vertical

    //number value of spacing
    float horizontalRaySpacing;
    float verticalRaySpacing;

    BoxCollider2D collider;
    ScoreManager scoreManager;
    RaycastOrigins raycastOrigins;
    public CollisionInfo collisions;

    void Start()
    {
        scoreManager = FindObjectOfType<ScoreManager>().GetComponent<ScoreManager>();
        collider = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    public void Move(Vector3 velocity) //movement script
    {
        UpdateRaycastOrigins();
        collisions.Reset();

        if (velocity.x != 0)
        {
            HorizontalCollisions(ref velocity);
        }
        if (velocity.y != 0)
        {
            VerticalCollisions(ref velocity);
        }
        transform.Translate(velocity);
    }

    void VerticalCollisions(ref Vector3 velocity) //detects vertical collisions
    {
        float directionY = Mathf.Sign(velocity.y);
        float rayLength = Mathf.Abs(velocity.y) + skinWidth;

        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 rayOrigin = (directionY == -1) ? raycastOrigins.bottomLeft : raycastOrigins.topLeft;
            rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);

            foreach (LayerMask mask in collisionMask)
            {
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.up * directionY, rayLength, mask);

                if (hit)
                {
                    if(CheckItemCollision(hit))
                    {
                        continue;
                    }

                    velocity.y = (hit.distance - skinWidth) * directionY;
                    rayLength = hit.distance; //changes ray distance to avoid falling through platforms

                    collisions.below = directionY == -1; //if hitting something equals true
                    collisions.above = directionY == 1;
                }
            }
        }
    }

    void HorizontalCollisions(ref Vector3 velocity) //detects vertical collisions
    {
        float directionX = Mathf.Sign(velocity.x);
        float rayLength = Mathf.Abs(velocity.x) + skinWidth;

        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 rayOrigin = (directionX == -1) ? raycastOrigins.bottomLeft : raycastOrigins.bottomRight;
            rayOrigin += Vector2.up * (horizontalRaySpacing * i);

            foreach (LayerMask mask in collisionMask)
            {
                RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.right * directionX, rayLength, mask);

                if (hit)
                {
                    if (CheckItemCollision(hit))
                    {
                        continue;
                    }

                    velocity.x = (hit.distance - skinWidth) * directionX;
                    rayLength = hit.distance; //changes ray distance to avoid falling through platforms

                    collisions.left = directionX == -1; //if hitting something equals true
                    collisions.right = directionX == 1;
                }
            }
        }
    }

    bool CheckItemCollision(RaycastHit2D hit)
    {
        int coinValue = 1;

        if (hit.collider.tag == "Coin")
        {
            Destroy(hit.collider.gameObject);
            //audio clip
            scoreManager.AddToScore(coinValue);
            Debug.Log("Coin Hit");
            return true;
        }
        return false;
    }

    void UpdateRaycastOrigins() //origin of raycast
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        raycastOrigins.bottomLeft = new Vector2(bounds.min.x, bounds.min.y);
        raycastOrigins.bottomRight = new Vector2(bounds.max.x, bounds.min.y);
        raycastOrigins.topLeft = new Vector2(bounds.min.x, bounds.max.y);
        raycastOrigins.topRight = new Vector2(bounds.max.x, bounds.max.y);
    }

    void CalculateRaySpacing() //detects ray spacing
    {
        Bounds bounds = collider.bounds;
        bounds.Expand(skinWidth * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount, 2, int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount, 2, int.MaxValue);

        horizontalRaySpacing = bounds.size.y / (horizontalRayCount - 1);
        verticalRaySpacing = bounds.size.x / (verticalRayCount - 1);
    }

    struct RaycastOrigins //determins where from the sprite the rays come from
    {
        public Vector2 topLeft, topRight;
        public Vector2 bottomLeft, bottomRight;
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public void Reset()
        {
            above = below = false;
            left = right = false;
        }
    }
}
