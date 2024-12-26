using System;
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
    public TextMeshProUGUI resultText;

    public Animator playerAnim;
    public Animator enemyAnim;

    private float drawTime = 0.5f;
    private Coroutine enemyDraw;

    private bool playerDrew = false;
    private bool enemyDrew = false;

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
        if (Input.GetMouseButtonDown(0))
        {
            if(!playerDrew && !enemyDrew)
            {
                PlayerDraw();
            }
        }
    }

    public void PrepareToDraw()
    {
        enemyDraw = StartCoroutine(CountdownToDraw());
    }

    IEnumerator CountdownToDraw()
    {
        yield return new WaitForSeconds(drawTime);

        if (!playerDrew)
        {
            EnemyDraw();
        }
    }

    public void ReadyAnims()
    {
        playerAnim.Play("Player_Idle");
        enemyAnim.Play("Enemy_Idle");
    }

    public void PlayerDraw()
    {
        playerDrew = true;
        CutOffDialog();

        playerAnim.Play("Player_Draw");
        enemyAnim.Play("Enemy_Die");

        if (encounter.currentHunch.decoy)
        {
            enemyAnim.SetBool("Alien", false);
            StartCoroutine(SetResultText("Blood runs red. What have you done?"));
        }
        else
        {
            enemyAnim.SetBool("Alien", true);
            StartCoroutine(SetResultText("Your gut was right and your aim was true."));
        }
    }

    public void EnemyDraw()
    {
        enemyDrew = true;
        CutOffDialog();

        playerAnim.Play("Player_Die");
        enemyAnim.Play("Enemy_Draw");

        StartCoroutine(SetResultText("You hesitated a moment too long. Now you're dead."));
    }

    public void CutOffDialog()
    {
        dialogText.lockScroll = true;

        //truncate string so that it ends with a letter
        string input = dialogText.dialogText.text;

        if (input.EndsWith(".") && !input.EndsWith("..."))
        {
            return;
        }

        int length = input.Length;
        while (length > 0 && !Char.IsLetterOrDigit(input[length - 1]))
        {
            length--;
        }

        input = input.Substring(0, length);
        dialogText.dialogText.text = input + "—";
    }

    IEnumerator SetResultText(string input)
    {
        yield return new WaitForSeconds(7f);
        resultText.text = input;
    }
}
