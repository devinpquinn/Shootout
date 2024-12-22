using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BubbleResizer : MonoBehaviour
{
    private RectTransform rect;
    public TextMeshProUGUI text;

    private int lines = -1;
    public int maxLines;

    public float minHeight;
    public float maxHeight;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if(text.textInfo.lineCount != lines)
        {
            //resize
            float numLines = text.textInfo.lineCount;
            if(numLines < 1)
            {
                numLines = 1;
            }
            float bump = (maxHeight - minHeight) / (maxLines - 1);
            float bumps = numLines - 1;
            float totalBumped = bump * bumps;

            rect.sizeDelta = new Vector2(rect.sizeDelta.x, minHeight + totalBumped);

            lines = text.textInfo.lineCount;
        }
    }
}
