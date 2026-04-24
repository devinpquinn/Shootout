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
    public GameObject hunchBubbleObj;
    public TextMeshProUGUI resultText;
    public GameObject clickToContinuePrompt;

    public Animator playerAnim;
    public Animator enemyAnim;

    private float drawTime = 0.5f;
    private Coroutine enemyDraw;

    public float hunchTextDelay = 1f;
    public float continuePromptDelay = 1.5f;

    private bool playerDrew = false;
    private bool enemyDrew = false;

    public Animator fadeAnim;
    public GameObject hunchPrompt;

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
            "This crimson blood condemns your act of haste.\n\nA human life laid low in senseless waste.",
            "A leaden judgment paid the devil due.\n\nYour gut spoke sense, your aim has proved it true.",
            "Your blood upon the dust spells out in red\n\nThe oldest law— too slow, now you are dead.",
            "Suspicion fades; no shot disturbs the air.\n\nTwo strangers pass, and leave the moment there."
        };
        bool win = resultState == 1 || resultState == 3;

        TriggerSkyLerp();

        yield return new WaitForSeconds(4.5f);
        
        if(resultState == 0)
        {
            playerAnim.SetTrigger("ShotHuman");
        }
        else if(resultState == 1)
        {
            playerAnim.SetTrigger("ShotAlien");
        }
        else if(resultState == 2)
        {
            enemyAnim.SetTrigger("ShotHuman");
        }

        GetComponent<AudioSource>().PlayOneShot(win ? winSound : loseSound);

        resultText.gameObject.SetActive(true);
        resultText.text = texts[resultState];

        yield return new WaitForSeconds(continuePromptDelay);

        clickToContinuePrompt.SetActive(true);
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
