using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_PlayerUI : MonoBehaviour
{
    [SerializeField] private Image affordance, hardware, history, software;
    [SerializeField] private TMP_Text question, op1, op2;
    [SerializeField] private GameObject QuestionUI;
    [SerializeField] private Button re1, re2;
    // Start is called before the first frame update
    void Start()
    {
        affordance.enabled = false;
        hardware.enabled = false;
        history.enabled = false;
        software.enabled = false;
        
        QuestionUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetAffordanceBadge(S_BadgePickup badge)
    {
        if (badge != null && S_GameManager.instance.GetCoinAmount() >= 89.99f)
        {
            affordance.enabled = true;
        }
    }

    public void SetHardwareBadge(S_BadgePickup badge)
    {
        if (badge != null)
        {
            Time.timeScale = 0;
            QuestionUI.SetActive(true);
            question.text = "What model is the CPU used in the Nintendo Entertainment System?";
            op1.text = "2A03";
            op2.text = "2C02";

            re1.onClick.AddListener(() =>
            {
                CorrectAnswer(hardware);
            });
            re2.onClick.AddListener(() =>
            {
                IncorrectAnswer(hardware);
            });
        }
    }

    public void SetHistoryBadge(S_BadgePickup badge)
    {
        history.enabled = true;
    }

    public void SetSoftwareBadge(S_BadgePickup badge)
    {
        software.enabled = true;
    }

    private void CorrectAnswer(Image img)
    {
        Time.timeScale = 1;
        img.enabled = true;
        QuestionUI.SetActive(false);
    }

    private void IncorrectAnswer(Image img)
    {
        Time.timeScale = 1;
        img.enabled = false;
        QuestionUI.SetActive(false);
    }
}
