using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField]
    private GameObject initPhysicItem;
    public static GameObject PhysicItem;

    static GameObject spawnedItem;
    /* static float dropRandomnes;

    public float maxDropForce = 1.1f;
    public float minDropForce = 0.9f; */

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        PhysicItem = initPhysicItem;
    }
    void FixedUpdate()
    {
        //dropRandomnes = Random.Range(minDropForce,maxDropForce);
    }
    public static void Spawn(Item item, Vector3 position, float dropRandomnes = 0.3f, float forceX = 1f,float forceY = 1f)
    {

        PhysicItem.GetComponent<PickUp>().item = item;
        spawnedItem = (GameObject)Instantiate(PhysicItem, position, Quaternion.identity);
        spawnedItem.GetComponent<Rigidbody2D>().AddForce(new Vector2(50f * Random.Range(1 - dropRandomnes, 1 + dropRandomnes) * forceX, 50f* forceY));
    }
}
