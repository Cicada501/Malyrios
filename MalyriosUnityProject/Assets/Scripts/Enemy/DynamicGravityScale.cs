using UnityEngine;

public class DynamicGravityScale : MonoBehaviour
{

    private Rigidbody2D rb2d;
    [SerializeField]private float climbingGravityScale;
    [SerializeField]private float fallingGravityScale;
    private RaycastHit2D hit;
    [SerializeField] private float distanceThreshold = 0.5f; // Die Entfernung, ab der die Gravitation angepasst wird
    private int groundLayer;


    void Start() {
        rb2d = GetComponent<Rigidbody2D>();
        groundLayer = LayerMask.GetMask("Ground");
    }
    void Update()
    {
        hit = Physics2D.Raycast(transform.position, -Vector2.up, 10f, groundLayer);

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
