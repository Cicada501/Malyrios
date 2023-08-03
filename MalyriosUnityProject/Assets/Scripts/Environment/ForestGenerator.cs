using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForestGenerator : MonoBehaviour
{
    [SerializeField] private List<Sprite> treeSprites;
    [SerializeField] private float forestWidth;
    [SerializeField] private float widthMultiplier;
    [SerializeField] private float minY;
    [SerializeField] private float minDistance;
    [SerializeField] private float maxDistance;
    [SerializeField] private int seed;
    [SerializeField] private int initSortingLayer;

    private void Start()
    {
        GenerateForest();
    }

    private void GenerateForest()
    {
        Random.InitState(seed);
        forestWidth = forestWidth * widthMultiplier;
        Vector2 prevPosition = new Vector2( transform.position.x -forestWidth / 2, minY);
        var lastTree = false;
        var i = 0;
        while(!lastTree)
        {
            Sprite treeSprite = treeSprites[Random.Range(0, treeSprites.Count)];

            Vector2 position = new Vector2(prevPosition.x + Random.Range(minDistance, maxDistance), minY + treeSprite.bounds.size.y/2);
            
            //Do not allow the forest to go above its defined width
            if(position.x >= transform.position.x + forestWidth/2)
            {
                lastTree = true;
            }
            
            GameObject treePrefab = new GameObject("TreePrefab");
            SpriteRenderer spriteRenderer = treePrefab.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = treeSprite;
            spriteRenderer.sortingOrder = i+ initSortingLayer;
            spriteRenderer.sortingLayerName = "Background";


            GameObject tree = Instantiate(treePrefab, position, Quaternion.identity);
            tree.transform.SetParent(transform);
            tree.name = $"Tree_{i}";

            prevPosition = position;
            Destroy(treePrefab);
            i++;
        }
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;

        var x = transform.position.x;
        Vector3 topLeft = new Vector3(x - forestWidth*widthMultiplier / 2, 10, 0);
        Vector3 topRight = new Vector3(x + forestWidth*widthMultiplier / 2, 10, 0);
        Vector3 bottomLeft = new Vector3(x - forestWidth*widthMultiplier / 2, -10,0);
        Vector3 bottomRight = new Vector3(x + forestWidth*widthMultiplier / 2, -10, 0);
        Gizmos.DrawLine(topRight, bottomRight);
        Gizmos.DrawLine(bottomLeft, topLeft);
        
    }
}