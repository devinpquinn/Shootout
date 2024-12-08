using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTester : MonoBehaviour
{
    public Animator anim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            anim.Play("Player_Idle");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            anim.Play("Player_Draw");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            anim.Play("Player_Death");
        }
    }
}
