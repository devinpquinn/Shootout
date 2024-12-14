using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public List<string> lines; //the dialog text of this encounter
    public List<Hunch> hunches;

    public Hunch currentHunch = null;

    public void Begin()
    {
        //select hunch

        //set hunch text
        //maybe wait?
        //start playing lines of dialog
    }
}

[System.Serializable]
public class Hunch
{
    public bool decoy = false; //whether or not this hunch actually has a trigger within the encounter's dialog
    public string keyword; //the text sought within the encounter lines that triggers the shootout (if this is a decoy, this keyword will not be present)
    public string hint; //the text of the hunch displayed to the player
}
