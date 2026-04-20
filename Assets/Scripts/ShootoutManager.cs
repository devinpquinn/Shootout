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
    public TextScroller hunchText;
    public TextMeshProUGUI resultText;

    public Animator playerAnim;
    public Animator enemyAnim;

    private float drawTime = 0.5f;
    private Coroutine enemyDraw;

    private bool playerDrew = false;
    private bool enemyDrew = false;

    public Animator fadeAnim;

    public AudioClip winSound;
    public AudioClip loseSound;

    public SpriteRenderer skyRenderer;
    public Color skyEndColor;
    public float skyLerpDuration = 2f;

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
            if(!playerDrew && !enemyDrew && encounter.began && !encounter.ended)
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
        
        StartCoroutine(FlashDelayed(0.15f));

        if (encounter.currentHunch.decoy)
        {
            enemyAnim.SetBool("Alien", false);
            StartCoroutine(SetResultText(0));
        }
        else
        {
            enemyAnim.SetBool("Alien", true);
            StartCoroutine(SetResultText(1));
        }
    }

    public void EnemyDraw()
    {
        enemyDrew = true;
        CutOffDialog();

        playerAnim.Play("Player_Die");
        enemyAnim.Play("Enemy_Draw");
        
        StartCoroutine(FlashDelayed(0.15f));

        StartCoroutine(SetResultText(2));
    }
    
    IEnumerator FlashDelayed(float delay)
    {
        yield return new WaitForSeconds(delay);
        fadeAnim.Play("Fade_FlashWhiteInstant");
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

    // resultState: 0 = shot human, 1 = shot alien, 2 = enemy drew, 3 = both humans
    public IEnumerator SetResultText(int resultState)
    {
        string[] texts = new string[]
        {
            "This red blood your damnation, what have you done?\n\nA human life wasted by a fool with a gun.",
            "A judgement in lead gave the devil its due.\n\nYour gut called it right and your aim made it true.",
            "Your blood paints the ground one last lesson in red.\n\nIt's the oldest of laws— you were slow, now you're dead.",
            "Two humans disarm, stow suspicion away\n\nYour paths both continue— no death on this day"
        };
        bool win = resultState == 1 || resultState == 3;

        TriggerSkyLerp();

        yield return new WaitForSeconds(4.5f);
        
        if(resultState == 1)
        {
            playerAnim.SetTrigger("ShotAlien");
        }

        GetComponent<AudioSource>().PlayOneShot(win ? winSound : loseSound);

        resultText.text = texts[resultState];
    }

    public void TriggerSkyLerp()
    {
        StartCoroutine(LerpSky());
    }

    IEnumerator LerpSky()
    {
        Color startColor = skyRenderer.color;
        float elapsed = 0f;
        while (elapsed < skyLerpDuration)
        {
            elapsed += Time.deltaTime;
            skyRenderer.color = Color.Lerp(startColor, skyEndColor, elapsed / skyLerpDuration);
            yield return null;
        }
        skyRenderer.color = skyEndColor;
    }
}
