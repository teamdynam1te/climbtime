using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSystem : MonoBehaviour
{
    public Transform crossHair;
    public SpriteRenderer crossHairSprite;
    private Vector2 playerPos;
    public Transform shootPoint;

    // Start is called before the first frame update
    void Start()
    {
        playerPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        var worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0f));
        var facingDir = worldMousePos - transform.position;
        var aimAngle = Mathf.Atan2(facingDir.y, facingDir.x);
        if (aimAngle < 0f)
        {
            aimAngle = Mathf.PI * 2 + aimAngle;
        }

        var aimDirection = Quaternion.Euler(0, 0, aimAngle * Mathf.Rad2Deg) * Vector2.right;
        playerPos = transform.position;

        SetCrosshairPosition(aimAngle);
        ShootHook();
    }

    private void SetCrosshairPosition(float aimAngle)
    {
        if (!crossHairSprite.enabled)
        {
            crossHairSprite.enabled = true;
        }

        var x = transform.position.x + 2f * Mathf.Cos(aimAngle);
        var y = transform.position.y + 2f * Mathf.Sin(aimAngle);

        var crossHairPosition = new Vector3(x, y, 0);
        crossHair.transform.position = crossHairPosition;
    } // cross hair aiming

    private void ShootHook() // handles shooting the hook
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(shootPoint.position, shootPoint.right);

            if (hit)
            {
                Debug.Log(hit.transform.name);
            }
        }
    }
}
