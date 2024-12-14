using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShootoutManager : MonoBehaviour
{
    public static ShootoutManager instance;

    public Encounter encounter;
    public TextScroller dialogText;
    public TextMeshProUGUI hunchText;

    public Animator shootoutAnim;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        encounter.Begin();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Idle();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Shoot_L();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Shoot_R();
        }
    }

    public void Idle()
    {
        shootoutAnim.Play("Shootout_Idle");
    }

    public void Shoot_L()
    {
        shootoutAnim.Play("Shootout_Shoot_L");
    }

    public void Shoot_R()
    {
        shootoutAnim.Play("Shootout_Shoot_R");
    }
}
