using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class S_PlayerUI : MonoBehaviour
{
    [SerializeField] private Image affordance, hardware, history, software;
    [SerializeField] private TMP_Text question, op1, op2;
    [SerializeField] private GameObject QuestionUI;
    [SerializeField] private Button re1, re2;
    [SerializeField] private GameObject portal;
    // Start is called before the first frame update
    void Start()
    {
        affordance.enabled = false;
        hardware.enabled = false;
        history.enabled = false;
        software.enabled = false;
        
        QuestionUI.SetActive(false);
    }

    public void SetAffordanceBadge(S_BadgePickup badge)
    {
        if (badge != null)
        {
            if (S_GameManager.instance.GetCoinAmount() >= 89.99f)
            {
                affordance.enabled = true;
                badge.gameObject.SetActive(false);
                S_GameManager.instance.SetCoinAmount(S_GameManager.instance.GetCoinAmount() - 90);
            }
        }
    }

    public void SetHardwareBadge(S_BadgePickup badge)
    {
        if (badge != null)
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            QuestionUI.SetActive(true);
            question.text = "What model is the CPU used in the Nintendo Entertainment System?";
            op1.text = "2A03";
            op2.text = "2C02";

            re1.onClick.AddListener(() =>
            {
                CorrectAnswer(hardware);
                badge.gameObject.SetActive(false);
            });
            re2.onClick.AddListener(() =>
            {
                IncorrectAnswer(hardware);
            });
        }
    }

    public void SetHistoryBadge(S_BadgePickup badge)
    {
        if (badge != null)
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            QuestionUI.SetActive(true);
            question.text = "What year did the Nintendo Entertainment System release in North America?";
            op1.text = "1986";
            op2.text = "1983";

            re1.onClick.AddListener(() =>
            {
                CorrectAnswer(history);
                badge.gameObject.SetActive(false);
                portal.SetActive(true);
            });
            re2.onClick.AddListener(() =>
            {
                IncorrectAnswer(history);
            });
        }
    }

    public void SetSoftwareBadge(S_BadgePickup badge)
    {
        if (badge != null)
        {
            Cursor.visible = true;
            Time.timeScale = 0;
            QuestionUI.SetActive(true);
            question.text = "How many copies did 'Super Mario Bros.' sell?";
            op1.text = "40.2m";
            op2.text = "28.3m";

            re1.onClick.AddListener(() =>
            {
                CorrectAnswer(software);
                badge.gameObject.SetActive(false);
            });
            re2.onClick.AddListener(() =>
            {
                IncorrectAnswer(software);
            });
        }
    }

    private void CorrectAnswer(Image img)
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        img.enabled = true;
        QuestionUI.SetActive(false);
    }

    private void IncorrectAnswer(Image img)
    {
        Cursor.visible = false;
        Time.timeScale = 1;
        img.enabled = false;
        QuestionUI.SetActive(false);
    }
}
