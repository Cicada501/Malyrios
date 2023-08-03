using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicGravityScale : MonoBehaviour
{
    private Vector3 lastPosition;
    [SerializeField] private float maxSlopeAngle;
    private Rigidbody2D rb2d;
    [SerializeField]private float climbingGravityScale;
    [SerializeField]private float fallingGravityScale;
    [SerializeField]private float defaultGravityScale;
    private RaycastHit2D hit;
    [SerializeField] private float distanceThreshold; // Die Entfernung, ab der die Gravitation angepasst wird
    [SerializeField] private bool printDist;
    private int groundLayer;


    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
        groundLayer = LayerMask.GetMask("Ground");
    }
    void Update()
    {
        hit = Physics2D.Raycast(transform.position, -Vector2.up, 10f, groundLayer);
        Debug.DrawRay(transform.position, -Vector2.up, Color.red);
        if (printDist)
        {
            if (hit.collider != null)
            {
                print("hit detected ");
            }
            print($"Der Boden ist {hit.distance} Einheiten entfernt");
        }

        if (hit.collider != null && rb2d != null) {
            if (hit.distance > distanceThreshold)
            {
                rb2d.gravityScale = fallingGravityScale;
            } else {
               
                rb2d.gravityScale = climbingGravityScale;
            }
        }

    }
}
