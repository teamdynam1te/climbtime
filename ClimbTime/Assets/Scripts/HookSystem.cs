using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSystem : MonoBehaviour
{
    public Transform crossHair;
    public SpriteRenderer crossHairSprite;
    private Vector2 playerPos;

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
    }
}
