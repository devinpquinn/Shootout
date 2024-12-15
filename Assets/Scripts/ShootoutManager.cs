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

    public Animator playerAnim;
    public Animator enemyAnim;

    private float drawTime = 0.5f;
    private Coroutine enemyDraw;

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
            PlayerDraw();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            EnemyDraw();
        }
    }

    public void PrepareToDraw()
    {
        enemyDraw = StartCoroutine(CountdownToDraw());
    }

    IEnumerator CountdownToDraw()
    {
        yield return new WaitForSeconds(drawTime);

        EnemyDraw();
    }

    public void Idle()
    {
        playerAnim.Play("Player_Idle");
        enemyAnim.Play("Enemy_Idle");
    }

    public void PlayerDraw()
    {
        playerAnim.Play("Player_Draw");
        enemyAnim.Play("Enemy_Die");
    }

    public void EnemyDraw()
    {
        playerAnim.Play("Player_Die");
        enemyAnim.Play("Enemy_Draw");
    }
}
