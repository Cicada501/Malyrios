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
        SoundHolder.Instance.openGateSound.Play();
        gateCollider.enabled = false;
    }

    public void CloseGate()
    {
        animator.Play("Close");
        SoundHolder.Instance.closeGateSound.Play();
        gateCollider.enabled = true;
    }
}