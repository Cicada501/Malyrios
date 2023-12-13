using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float destroyedAfterSeconds;
    private Rigidbody2D rb;
    [HideInInspector]
    public Enemy parentEnemy;
    private Vector2 initialVelocityDirection;
    private float lifeTime;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity =  (parentEnemy.facingRight ? Vector2.right : Vector2.left)*speed;
        transform.parent = null;
    }

    private void Update()
    {
        lifeTime += Time.deltaTime;
        if (lifeTime > destroyedAfterSeconds)
        {
            Destroy(gameObject);  
        }
    }
    private void OnTriggerEnter2D(Collider2D hitInfo)
    {
        
        PlayerHealth playerHealth = hitInfo.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
            Destroy(gameObject);
        }

    }
}