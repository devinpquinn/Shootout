using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public List<string> lines; //the dialog text of this encounter
    private int lineIndex = 0;
    public List<Hunch> hunches;

    [HideInInspector] public Hunch currentHunch = null;

    public void Begin()
    {
        //select hunch
        currentHunch = hunches[Random.Range(0, hunches.Count)];

        //set hunch text
        ShootoutManager.instance.hunchText.SetText(currentHunch.hint);

        //maybe wait?

        //start playing lines of dialog
        lineIndex = 0;
        ShootoutManager.instance.dialogText.NewLine(lines[0]);
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
        }
    }
}

[System.Serializable]
public class Hunch
{
    public bool decoy = false; //whether or not this hunch actually has a trigger within the encounter's dialog
    public string keyword; //the text sought within the encounter lines that triggers the shootout (if this is a decoy, this keyword will not be present)
    public string hint; //the text of the hunch displayed to the player
}
