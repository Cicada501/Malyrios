using UnityEngine;

public class PuzzleGate : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D gateCollider;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        gateCollider = GetComponent<BoxCollider2D>();
    }

    public void OpenGate()
    {
        animator.Play("Open");
        
        gateCollider.enabled = false;
    }

    public void CloseGate()
    {
        animator.Play("Close");
        gateCollider.enabled = true;
    }
}