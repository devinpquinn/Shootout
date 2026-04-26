using System.Collections;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public EncounterData data;
    private int lineIndex = 0;

    [HideInInspector] public Hunch currentHunch = null;

    [HideInInspector] public bool began = false;
    [HideInInspector] public bool ended = false;

    public void Begin()
    {
        StartCoroutine(DoBegin());
    }

    IEnumerator DoBegin()
    {
        ShootoutManager.instance.dialogText.transform.parent.gameObject.SetActive(false);
        ShootoutManager.instance.hunchPrompt.SetActive(false);
        ShootoutManager.instance.hunchBubbleObj.SetActive(false);

        //select hunch
        currentHunch = data.hunches[Random.Range(0, data.hunches.Count)];

        //set hunch text
        ShootoutManager.instance.hunchPrompt.SetActive(true);
        yield return new WaitForSeconds(2f);
        ShootoutManager.instance.hunchBubbleObj.SetActive(true);
        ShootoutManager.instance.hunchText.NewLine(currentHunch.hint);

        yield return new WaitUntil(() => ShootoutManager.instance.hunchText.isDone);
        yield return new WaitForSeconds(2f);
        ShootoutManager.instance.hunchPrompt.GetComponent<Animator>().Play("HunchPrompt_Out");
        yield return new WaitForSeconds(1f); // wait for the animation to finish
        ShootoutManager.instance.dialogText.transform.parent.gameObject.SetActive(true);

        //start playing lines of dialog
        lineIndex = 0;
        ShootoutManager.instance.dialogText.NewLine(data.lines[0]);

        yield return new WaitForSeconds(0.1f);

        began = true;
    }

    public void Continue()
    {
        lineIndex++;

        if(data.lines.Count > lineIndex)
        {
            //continue to next line
            ShootoutManager.instance.dialogText.NewLine(data.lines[lineIndex]);
        }
        else
        {
            //end encounter
            //you win! hide dialog ui and show result text
            ended = true;
            StartCoroutine(EndEncounter_Humans());
        }
    }

    public void Reset()
    {
        StopAllCoroutines();
        lineIndex = 0;
        began = false;
        ended = false;
        currentHunch = null;
    }

    IEnumerator EndEncounter_Humans()
    {
        ShootoutManager.instance.fadeAnim.Play("Fade_FlashWhite");
        yield return new WaitForSeconds(0.25f);

        ColorChanger cc = ShootoutManager.instance.playerAnim.gameObject.GetComponent<ColorChanger>();
        cc.bubble.sprite = cc.bubbleAfter;
        cc.bubbleAnim.Play("SpeechBubble_Float");
        cc.hunchBubble.sprite = cc.bubbleDark;
        cc.hunchBubbleAnim.Play("HunchBubble_Float");
        
        StartCoroutine(ShootoutManager.instance.SetResultText(3));
    }
}

