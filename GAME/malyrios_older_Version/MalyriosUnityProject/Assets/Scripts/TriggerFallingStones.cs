using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerFallingStones : MonoBehaviour
{
    [SerializeField]  GameObject fallingStone;
    Rigidbody2D rbStone;

    float stoneStartPos;
    private void Start() {
        rbStone = fallingStone.GetComponent<Rigidbody2D>();
        stoneStartPos = fallingStone.transform.position.x;
    }
    private void Update() {
        fallingStone.transform.position = new Vector3(stoneStartPos,fallingStone.transform.position.y,0f);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Player")){
        
            rbStone.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            rbStone.gravityScale = 5f;
            StaticData.spawnPoint = new Vector3(24.5f,-10.8f,0f);
    
            
        }
    }
}
