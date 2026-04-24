using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEncounter", menuName = "Shootout/Encounter")]
public class EncounterData : ScriptableObject
{
    public List<string> lines;
    public List<Hunch> hunches;
}

[System.Serializable]
public class Hunch
{
    public bool decoy = false; //whether or not this hunch actually has a trigger within the encounter's dialog
    public string keyword; //the text sought within the encounter lines that triggers the shootout (if this is a decoy, this keyword will not be present)
    public string hint; //the text of the hunch displayed to the player
}
