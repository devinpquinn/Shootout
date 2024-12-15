using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public SpriteRenderer sky;
    private Color skyColorDefault;
    public Color skyColorDark;

    public SpriteRenderer ground;
    private Color groundColorDefault;
    public Color groundColorDark;

    private void Awake()
    {
        skyColorDefault = sky.color;
        groundColorDefault = ground.color;
    }

    public void MuzzleFlash_Darken()
    {
        sky.color = skyColorDark;
        ground.color = groundColorDark;
    }

    public void MuzzleFlash_Restore()
    {
        sky.color = skyColorDefault;
        ground.color = groundColorDefault;
    }
}
