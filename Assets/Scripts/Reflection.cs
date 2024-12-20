using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reflection : MonoBehaviour
{
    private SpriteRenderer rend;
    public SpriteRenderer targetRenderer;

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(rend.sprite != targetRenderer.sprite)
        {
            rend.sprite = targetRenderer.sprite;
        }
    }
}
