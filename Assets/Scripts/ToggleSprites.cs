using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleSprites : MonoBehaviour
{
    public List<SpriteRenderer> targets;

    public void ToggleOn()
    {
        foreach(SpriteRenderer target in targets)
        {
            target.enabled = true;
        }
    }

    public void ToggleOff()
    {
        foreach (SpriteRenderer target in targets)
        {
            target.enabled = false;
        }
    }
}
