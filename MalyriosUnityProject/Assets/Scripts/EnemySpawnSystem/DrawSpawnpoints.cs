using UnityEngine;

public class DrawSpawnpoints : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private Color color;
    
    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        Gizmos.DrawSphere(transform.position, this.radius);
    }
}
