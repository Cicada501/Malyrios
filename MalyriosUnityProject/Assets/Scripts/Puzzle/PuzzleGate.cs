using UnityEngine;

public class PuzzleGate : MonoBehaviour
{
    private Animator animator;
    private BoxCollider2D gateCollider;
    private bool open;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        gateCollider = GetComponent<BoxCollider2D>();
    }

    public void OpenGate()
    {
        if (!open)
        {
            animator.Play("Open");
            SoundHolder.Instance.openGateSound.Play();
            gateCollider.enabled = false;
            open = true;
        }
    }

    public void CloseGate()
    {
        if (open)
        {
            animator.Play("Close");
            SoundHolder.Instance.closeGateSound.Play();
            gateCollider.enabled = true;
            open = false;
        }
    }
}