using System.Collections;
using System.Collections.Generic;
using Malyrios.Items;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField] private GameObject initPhysicItem = null;
    public static GameObject PhysicItem;
    private static Transform playerTransform;
    private static float dropRandomnes = 0.3f;
    private static float forceY = 1f;

    static GameObject spawnedItem;

    void Start()
    {
        PhysicItem = initPhysicItem;
        playerTransform = ReferencesManager.Instance.player.transform;
    }

    // public static void Spawn(Item item, Vector3 position, float dropRandomnes = 0.3f, float forceX = 1f, float forceY = 1f)
    // {
    //     PhysicItem.GetComponent<PickUp>().item = item;
    //     spawnedItem = (GameObject) Instantiate(PhysicItem, position, Quaternion.identity);
    //     spawnedItem.GetComponent<Rigidbody2D>()
    //         .AddForce(new Vector2(50f * Random.Range(1 - dropRandomnes, 1 + dropRandomnes) * forceX, 50f * forceY));
    // }

    public static void Spawn(BaseItem item, Vector3 position)
    {
        GameObject it = Instantiate(PhysicItem, position, Quaternion.identity);
        it.GetComponent<PickUp>().BaseItem = item;
        it.GetComponent<Rigidbody2D>().AddForce(new Vector2(50f * Random.Range(1 - dropRandomnes, 1 + dropRandomnes) * playerTransform.localScale.x, 50f * forceY));
    }
}