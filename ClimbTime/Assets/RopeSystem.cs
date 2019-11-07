using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RopeSystem : MonoBehaviour
{
    public GameObject ropeAnchor;
    public DistanceJoint2D ropeJoint;
    public Transform crossHair;
    public SpriteRenderer crossHairSprite;
    public Player player;
    private bool ropeAttach;
    private Vector2 playerPos;
    private Rigidbody2D ropeAnchorRigidbody;
    private SpriteRenderer ropeAnchorSprite;
    private bool distanceSet;
    public LineRenderer ropeRender; //throws our render for the rope
    public LayerMask ropeLayerMask; //defines layer
    public float ropeMaxDistance = 20f; //defines rope reach
    private List<Vector2> ropePositions = new List<Vector2>(); //gives me the list of the rope positions
    public float timeToTravel = 2f;

    private void Awake()
    {
        ropeJoint.enabled = false;
        playerPos = transform.position;
        ropeAnchorRigidbody = ropeAnchor.GetComponent<Rigidbody2D>();
        ropeAnchorSprite = ropeAnchor.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        var worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDir = worldMousePos - transform.position;
        var aimAngle = Mathf.Atan2(facingDir.y, facingDir.x);

        if (aimAngle < 0)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        var aimDir = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        playerPos = transform.position;

        if(!ropeAttach)
        {
            SetCrosshairPos(aimAngle);
        }
        else
        {
            crossHairSprite.enabled = false;
        }

        HandleInput(aimDir);
        UpdateRopePositions();
    }

    private void SetCrosshairPos(float aimAngle) //sets aim cross hair
    {
        if(!crossHairSprite.enabled)
        {
            crossHairSprite.enabled = true;
        }

        var x = transform.position.x + 2f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 2f * Mathf.Sin(aimAngle);

        var crossHairPos = new Vector3(x, y, 0);
        crossHair.transform.position = crossHairPos;
    }

    private void HandleInput(Vector2 aimDirection)
    {
        if(Input.GetMouseButton(0))
        {
            if (ropeAttach) return;
            ropeRender.enabled = true;

            var hit = Physics2D.Raycast(playerPos, aimDirection, ropeMaxDistance, ropeLayerMask);

            if(hit.collider != null) // checks to see if hit anything
            {
                ropeAttach = true;
                if(!ropePositions.Contains(hit.point))
                {
                    transform.GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, 2f), ForceMode2D.Impulse);
                    ropePositions.Add(hit.point);
                    ropeJoint.distance = Vector2.Distance(playerPos, hit.point);
                    ropeJoint.enabled = true;
                    ropeAnchorSprite.enabled = true;
                }
            }
            else
            {
                ropeRender.enabled = false;
                ropeAttach = false;
                ropeJoint.enabled = false;
            }

        }
        if(Input.GetMouseButton(1))
        {
            ResetRope();
        }

    }

    private void ResetRope() //resets values to false or off
    {
        ropeJoint.enabled = false;
        ropeAttach = false;
        //player is swinging needs to go here
        ropeRender.positionCount = 2;
        ropeRender.SetPosition(0, transform.position);
        ropeRender.SetPosition(0, transform.position);
        ropePositions.Clear();
        ropeAnchorSprite.enabled = false;
    }

    private void UpdateRopePositions()
    {
        if(!ropeAttach) //returns method if rope is not attached
        {
            return;
        }

        ropeRender.positionCount = ropePositions.Count + 1; //sets rop positions in the line renderer

        for (var i = ropeRender.positionCount - 1; i >= 0; i--)
        {
            if(i != ropeRender.positionCount - 1)//if the last point of the line render is not set
            {
                ropeRender.SetPosition(i, ropePositions[i]);

                if(i == ropePositions.Count - 1 || ropePositions.Count == 1) //loops back through checking 
                {
                    var ropePosition = ropePositions[ropePositions.Count - 1];

                    if(ropePositions.Count == 1)
                    {
                        ropeAnchorRigidbody.transform.position = ropePosition;

                        if(!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);

                            distanceSet = true;
                        }
                    }
                    else
                    {
                        ropeAnchorRigidbody.transform.position = ropePosition;
                        
                        if(!distanceSet)
                        {
                            ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);

                            distanceSet = true;
                        }
                    }
                }

                else if (i - 1 == ropePositions.IndexOf(ropePositions.Last()))
                {
                    var ropePosition = ropePositions.Last();
                    ropeAnchorRigidbody.transform.position = ropePosition;
                    if(!distanceSet)
                    {
                        ropeJoint.distance = Vector2.Distance(transform.position, ropePosition);

                        distanceSet = true;
                    }
                }

            }

            else
            {
                ropeRender.SetPosition(i, transform.position);
            }

        }

    }

}
