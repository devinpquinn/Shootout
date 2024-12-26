using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ColorChanger : MonoBehaviour
{
    public SpriteRenderer background;
    private Color backgroundColorDefault;
    public Color backgroundColorDark;

    public Image bubble;
    private Sprite bubbleDefault;
    public Sprite bubbleDark;
    public Sprite bubbleAfter;

    public Animator bubbleAnim;
    public Animator hunchAnim;

    public TextMeshProUGUI tmp;
    private Color tmpColorDefault;
    private Color tmpColorDark = Color.white;

    private void Awake()
    {
        backgroundColorDefault = background.color;
        bubbleDefault = bubble.sprite;
        tmpColorDefault = tmp.color;
    }

    public void MuzzleFlash_Darken()
    {
        background.color = backgroundColorDark;
        bubble.sprite = bubbleDark;
        tmp.color = tmpColorDark;

        bubbleAnim.Play("SpeechBubble_Float");
        hunchAnim.Play("TextFade_Out");
    }

    public void MuzzleFlash_Restore()
    {
        background.color = backgroundColorDefault;
        bubble.sprite = bubbleAfter;
        tmp.color = tmpColorDefault;
    }
}
