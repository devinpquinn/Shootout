using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStagger : MonoBehaviour
{
    private void Start()
    {
        Animator anim = GetComponent<Animator>();

        anim.Play(0, 0, Random.Range(0f, 1f));
    }
}
