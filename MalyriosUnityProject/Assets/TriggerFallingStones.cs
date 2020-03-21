using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFallingStones : MonoBehaviour
{
    public  GameObject fallingStone;
    Rigidbody2D rbStone;
    public Collider2D frictionGround;
    private void Start() {
        rbStone = fallingStone.GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
            print("stoneShouldFall");
            rbStone.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            rbStone.gravityScale = 20;
            //frictionGround.sharedMaterial.friction = 10f;
            
        }
    }
}
