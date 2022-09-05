using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawSpawnpoints : MonoBehaviour
{
    [SerializeField] private float radius;

    [SerializeField] private Color color;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, this.radius);
    }
}
