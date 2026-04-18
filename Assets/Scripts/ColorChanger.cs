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
    public Image hunchBubble;
    private Sprite bubbleDefault;
    public Sprite bubbleDark;
    public Sprite bubbleAfter;

    public Animator bubbleAnim;
    public Animator hunchBubbleAnim;

    public TextMeshProUGUI tmp;
    private Color tmpColorDefault;
    private Color tmpColorDark = Color.white;

    public float shakeMagnitude = 0.15f;
    public float shakeDuration = 0.4f;

    private void Awake()
    {
        backgroundColorDefault = background.color;
        bubbleDefault = bubble.sprite;
        tmpColorDefault = tmp.color;
    }

    public void MuzzleFlash_Darken()
    {
        background.color = backgroundColorDark;
        StartCoroutine(LerpBackgroundColor());
        
        bubble.sprite = bubbleDark;
        tmp.color = tmpColorDark;
        
        hunchBubble.sprite = bubbleDark;

        bubbleAnim.Play("SpeechBubble_Float");
        hunchBubbleAnim.Play("HunchBubble_Float");

        StartCoroutine(ShakeCamera());
    }

    private IEnumerator ShakeCamera()
    {
        Camera cam = Camera.main;
        Vector3 originalPos = cam.transform.localPosition;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float strength = shakeMagnitude * (1f - elapsed / shakeDuration);
            cam.transform.localPosition = originalPos + (Vector3)Random.insideUnitCircle * strength;
            elapsed += Time.deltaTime;
            yield return null;
        }

        cam.transform.localPosition = originalPos;
    }

    public void MuzzleFlash_Restore()
    {
        bubble.sprite = bubbleAfter;
        tmp.color = tmpColorDefault;
    }

    private IEnumerator LerpBackgroundColor()
    {
        yield return new WaitForSeconds(0.5f);
    
        float elapsed = 0f;
        float backgroundRestoreDuration = 0.1f;
        Color startColor = background.color;
        while (elapsed < backgroundRestoreDuration)
        {
            elapsed += Time.deltaTime;
            background.color = Color.Lerp(startColor, backgroundColorDefault, elapsed / backgroundRestoreDuration);
            yield return null;
        }
        background.color = backgroundColorDefault;
    }
}
