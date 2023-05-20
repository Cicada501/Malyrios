using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnimationRandom : MonoBehaviour
{
    public Animator animator;
    public float minTime = 3f;
    public float maxTime = 10f;

    private void Start()
    {
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        StartCoroutine(PlayRandomLookaround());
    }

    IEnumerator PlayRandomLookaround()
    {
        while (true)
        {
            float waitTime = Random.Range(minTime, maxTime);
            yield return new WaitForSeconds(waitTime);
            animator.SetTrigger("Lookaround");
        }
    }
}