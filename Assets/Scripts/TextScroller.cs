using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextScroller : MonoBehaviour
{
    //plain line text
    private string rawText = "";

    //tmp
    [HideInInspector]public TextMeshProUGUI dialogText;

    //current character index in line
    private int index = 0;

    //how much time between displaying standard characters
    private float timePerChar = 0.035f;

    //decreases every update
    private float timer = Mathf.Infinity;

    //audio
    private AudioSource textSource;

    public AudioClip letterSound;
    private string silentChars = ",.-?!;:()[]{}<>*& ";

    [HideInInspector] public bool lockScroll = false;

    //UI
    public RectTransform bubble;
    public Animator bubbleAnim;

    private void Awake()
    {
        dialogText = GetComponent<TextMeshProUGUI>();
        textSource = GetComponent<AudioSource>();
    }

    public void NewLine(string line)
    {
        if(rawText.Length > 0)
        {
            bubbleAnim.Play("SpeechBubble_Pressed");
        }

        rawText = line;
        dialogText.text = "";

        index = 0;
        timer = timePerChar;
    }

    //when a character is advanced, update the text in the UI
    public void AdvanceText()
    {
        if (lockScroll)
        {
            return;
        }

        index++;
        timer = timePerChar;

        if (index < rawText.Length)
        {
            //display split text

            //check next character
            string addedChar = rawText.Substring(index - 1, 1);
            string nextChar = rawText.Substring(index, 1);

            //check for rich text tag
            if (addedChar == "<")
            {
                if (nextChar == "/")
                {
                    index++;
                }
                index += 2;

                //refresh characters
                addedChar = rawText.Substring(index - 1, 1);
                if (index < rawText.Length)
                {
                    nextChar = rawText.Substring(index, 1);
                }
            }

            //check if added character signals a pause
            if ((addedChar == "." && nextChar != ".") || addedChar == "!" || addedChar == "?")
            {
                timer += 0.25f;
            }
            else if (addedChar == ",")
            {
                timer += 0.1f;
            }
            else if (addedChar == ";" || addedChar == ":" || rawText.Substring(0, index).EndsWith("--"))
            {
                timer += 0.2f;
            }

            //check next character
            if (nextChar == "'" || nextChar == "\"" || nextChar == ")" || nextChar == "]")
            {
                timer = timePerChar;
            }

            dialogText.text = rawText.Substring(0, index);

            //letter audio
            if (!silentChars.Contains(addedChar))
            {
                //randomly vary pitch
                float basePitch = 1f;

                float margin = 0.1f;
                float amount = Random.Range(basePitch - margin, basePitch + margin);

                textSource.pitch = amount;
                textSource.PlayOneShot(letterSound);
            }
        }
        else
        {
            //display full text
            dialogText.text = rawText;

            //wait for a few seconds and then advance
            StartCoroutine(WaitAndAdvance());
        }

        if (!ShootoutManager.instance.encounter.currentHunch.decoy && dialogText.text.EndsWith(ShootoutManager.instance.encounter.currentHunch.keyword))
        {
            //start timer for enemy to shoot
            ShootoutManager.instance.PrepareToDraw();
        }
    }

    IEnumerator WaitAndAdvance()
    {
        yield return new WaitForSeconds(2.5f);

        if (!lockScroll)
        {
            ShootoutManager.instance.encounter.Continue();
        }
    }

    private void Update()
    {
        if (index < rawText.Length)
        {
            timer -= Time.deltaTime;

            if (timer <= 0)
            {
                AdvanceText();
            }
        }
    }
}
