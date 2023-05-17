using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dynamicGravityScale : MonoBehaviour
{
    private Vector3 lastPosition;
    [SerializeField] private float maxSlopeAngle;
    private Rigidbody2D rb2d;
    [SerializeField]private float climbingGravityScale;
    [SerializeField]private float fallingGravityScale;
    [SerializeField]private float defaultGravityScale;
    private RaycastHit2D hit;
    
    
    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
    }
    void Update()
    {
        hit = Physics2D.Raycast(transform.position, -Vector2.up);

        if (hit.collider != null) {
            float slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            
            var position = transform.position;
            bool movingUpSlope = position.y > lastPosition.y;
            
            lastPosition = position;
            
            if (slopeAngle > 0 && slopeAngle < maxSlopeAngle) {
                if (movingUpSlope) {
                    rb2d.gravityScale = climbingGravityScale;
                } else {
                    rb2d.gravityScale = fallingGravityScale;
                }
            } else {
                rb2d.gravityScale = defaultGravityScale;
            }
        }

    }
}
