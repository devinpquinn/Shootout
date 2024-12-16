using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public SpriteRenderer background;
    private Color backgroundColorDefault;
    public Color backgroundColorDark;

    private void Awake()
    {
        backgroundColorDefault = background.color;
    }

    public void MuzzleFlash_Darken()
    {
        background.color = backgroundColorDark;
    }

    public void MuzzleFlash_Restore()
    {
        background.color = backgroundColorDefault;
    }
}
