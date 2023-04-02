using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushbackTest : MonoBehaviour
{
    private Rigidbody2D rb;

    [SerializeField] private float force;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(new Vector2(10,10)*force, ForceMode2D.Impulse);
    }
}
