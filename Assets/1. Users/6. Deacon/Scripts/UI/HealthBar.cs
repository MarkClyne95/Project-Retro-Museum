using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthSlider;
    public TMP_Text healthBarText;

    Damageable playerDamageble;

    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");

        if(player == null)
        {
            Debug.Log("No player found in the scene. Make sure it has the tag 'Player'");
        }
        playerDamageble = player.GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(playerDamageble.Health, playerDamageble.MaxHealth);
        healthBarText.text = "HP " + playerDamageble.Health + " / " + playerDamageble.MaxHealth;
    }

    private void OnEnable()
    {
        playerDamageble.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        playerDamageble.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    // Update is called once per frame
    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
        healthBarText.text = "HP " + newHealth + " / " + maxHealth;
    }
}
