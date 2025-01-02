using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public List<string> lines; //the dialog text of this encounter
    private int lineIndex = 0;
    public List<Hunch> hunches;

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

        //select hunch
        currentHunch = hunches[Random.Range(0, hunches.Count)];

        //set hunch text
        ShootoutManager.instance.hunchText.SetText(currentHunch.hint);

        yield return new WaitForSeconds(1);

        ShootoutManager.instance.dialogText.transform.parent.gameObject.SetActive(true);

        //start playing lines of dialog
        lineIndex = 0;
        ShootoutManager.instance.dialogText.NewLine(lines[0]);

        yield return new WaitForSeconds(1f);

        began = true;
    }

    public void Continue()
    {
        lineIndex++;

        if(lines.Count > lineIndex)
        {
            //continue to next line
            ShootoutManager.instance.dialogText.NewLine(lines[lineIndex]);
        }
        else
        {
            //end encounter
            //you win! hide dialog ui and show result text
            ended = true;
            StartCoroutine(EndEncounter_Humans());
        }
    }

    IEnumerator EndEncounter_Humans()
    {
        ShootoutManager.instance.fadeAnim.Play("Fade_FlashWhite");
        yield return new WaitForSeconds(0.25f);

        ColorChanger cc = ShootoutManager.instance.playerAnim.gameObject.GetComponent<ColorChanger>();
        cc.bubble.sprite = cc.bubbleAfter;
        cc.bubbleAnim.Play("SpeechBubble_Float");
        cc.hunchAnim.Play("TextFade_Out");
        yield return new WaitForSeconds(4.3f);

        ShootoutManager.instance.resultText.text = "Two humans disarm, stow suspicion away\n\nYour paths both continue— no death on this day";
        ShootoutManager.instance.gameObject.GetComponent<AudioSource>().PlayOneShot(ShootoutManager.instance.winSound);
    }
}

[System.Serializable]
public class Hunch
{
    public bool decoy = false; //whether or not this hunch actually has a trigger within the encounter's dialog
    public string keyword; //the text sought within the encounter lines that triggers the shootout (if this is a decoy, this keyword will not be present)
    public string hint; //the text of the hunch displayed to the player
}
