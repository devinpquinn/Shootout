using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTester : MonoBehaviour
{
    public Animator playerAnim;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            playerAnim.Play("Player_Idle");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            playerAnim.Play("Player_Draw");
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            playerAnim.Play("Player_Death");
        }
    }
}
