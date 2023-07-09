using UnityEngine;
public class DrawLine : MonoBehaviour
{
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        float y = transform.position.y;
        Gizmos.DrawLine(new Vector3(-1000, y, 0), new Vector3(1000, y, 0));
    }
}
