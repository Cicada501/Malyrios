using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] Transform fireBallSpawn = null;
    [SerializeField] GameObject fireball = null;
    [SerializeField] float fireballCooldownTime;
    private float fireballCooldown = 0f;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frasme
    void Update()
    {
        //Spawn Fireball
        if(ButtonScript.receivedAbility1_input&& fireballCooldown==0.0f){
            Instantiate(fireball,fireBallSpawn.transform.position,fireBallSpawn.rotation );
            fireballCooldown = fireballCooldownTime;
        }

        //When fireball on Cooldown reduce remaining cooldown
        if(fireballCooldown>0){
            fireballCooldown-=0.01f;
        }
        if (fireballCooldown<0) //cause unity says 0.08 - 0.01 is 0.69999
        {
            fireballCooldown = 0f;
        }
    }
}
