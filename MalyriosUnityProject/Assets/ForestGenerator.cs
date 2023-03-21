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
        float treeSpacing = forestWidth / (treeCount - 1);
        
        Vector2 prevPosition = new Vector2(-forestWidth / 2, minY);
        
        for (int i = 0; i < treeCount; i++)
        {
            Sprite treeSprite = treeSprites[Random.Range(0, treeSprites.Count)];

            Vector2 position = new Vector2(prevPosition.x + Random.Range(minDistance, maxDistance), minY + treeSprite.bounds.size.y/2);
            print($"Position: {position}");

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
}