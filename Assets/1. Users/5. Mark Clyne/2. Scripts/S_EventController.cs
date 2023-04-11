using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class S_EventController : MonoBehaviour
{
    private int index;
    private string actualText = "";
    
    public TextMeshProUGUI logTextBox;
    public S_Event actualEvent;
    public PauseInfo pauseInfo;
    public ScrollRect sv;

    public float svValue;


    private void Start()
    {
        ReproduceText();
    }

    private void ReproduceText()
    {
        //if not readied all letters
        if (index < actualEvent.finalText.Length)
        {
            //get one letter
            char letter = actualEvent.finalText[index];

            //Actualize on screen
            logTextBox.text = Write(letter);

            //set to go to the next
            index += 1;
            StartCoroutine(PauseBetweenChars(letter));
        }
    }

    private string Write(char letter)
    {
        actualText += letter;
        return actualText;
    }

    private IEnumerator PauseBetweenChars(char letter)
    {
        switch (letter)
        {
            case '.':
                yield return new WaitForSeconds(pauseInfo.dotPause);
                ReproduceText();
                yield break;
            case ',':
                yield return new WaitForSeconds(pauseInfo.commaPause);
                ReproduceText();
                yield break;
            case ' ':
                yield return new WaitForSeconds(pauseInfo.spacePause);
                ReproduceText();
                yield break;
            default:
                yield return new WaitForSeconds(pauseInfo.normalPause);
                ReproduceText();
                yield break;
        }
    }
}

[Serializable]
public class PauseInfo
{
    public float dotPause;
    public float commaPause;
    public float spacePause;
    public float normalPause;
}