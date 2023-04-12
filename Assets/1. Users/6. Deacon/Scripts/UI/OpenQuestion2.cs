using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenQuestion2 : MonoBehaviour
{
    public GameObject question;
    public float delayTime = 5f;

    public void OQuestion()
    {
        question.SetActive(true);
        Invoke("DeactivateQuestion", delayTime);
    }

    private void DeactivateQuestion()
    {
        question.SetActive(false);
    }
}
