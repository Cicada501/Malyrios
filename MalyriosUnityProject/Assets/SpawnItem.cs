using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField]
    private GameObject initPhysicItem;
    public static GameObject PhysicItem;

    static GameObject spawnedItem;

    void Start()
    {
        PhysicItem = initPhysicItem;
    }
   
    public static void Spawn(Item item, Vector3 position, float dropRandomnes = 0.3f, float forceX = 1f,float forceY = 1f)
    {

        PhysicItem.GetComponent<PickUp>().item = item;
        spawnedItem = (GameObject)Instantiate(PhysicItem, position, Quaternion.identity);
        spawnedItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(50f * Random.Range(1 - dropRandomnes, 1 + dropRandomnes) * forceX, 50f* forceY));
    }
}
