using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    [SerializeField] private List<Sprite> treeSprites;
    [SerializeField] private int treeCount;
    [SerializeField] private float forestWidth;
    [SerializeField] private float minY;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private int orderInLayer;

    private void Start()
    {
        GenerateForest();
    }

    private void GenerateForest()
    {
        Vector2 prevPosition = new Vector2( transform.position.x -forestWidth / 2, minY);
        print($"Position: {prevPosition}");
        
        for (int i = 0; i < treeCount; i++)
        {
            Sprite treeSprite = treeSprites[Random.Range(0, treeSprites.Count)];

            Vector2 position = new Vector2(prevPosition.x + Random.Range(minDistance, maxDistance), minY + treeSprite.bounds.size.y/2);
            
            //Do not allow the forest to go above its defined width
            if(position.x > transform.position.x + forestWidth/2) return;
            
            GameObject treePrefab = new GameObject("TreePrefab");
            SpriteRenderer spriteRenderer = treePrefab.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = treeSprite;
            spriteRenderer.sortingOrder = orderInLayer;

            GameObject tree = Instantiate(treePrefab, position, Quaternion.identity);
            tree.transform.SetParent(transform);
            tree.name = $"Tree_{i}";

            prevPosition = position;
            Destroy(treePrefab);
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        var x = transform.position.x;
        Vector3 topLeft = new Vector3(x - forestWidth / 2, 10, 0);
        Vector3 topRight = new Vector3(x + forestWidth / 2, 10, 0);
        Vector3 bottomLeft = new Vector3(x - forestWidth / 2, -10,0);
        Vector3 bottomRight = new Vector3(x + forestWidth / 2, -10, 0);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        
    }
}