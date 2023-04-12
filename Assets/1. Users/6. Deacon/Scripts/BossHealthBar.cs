using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class BossHealthBar : MonoBehaviour
{
    public Slider healthSlider;

    Damageable bossDamageble;

    private void Awake()
    {
        GameObject boss = GameObject.FindGameObjectWithTag("Boss");

        if (boss == null)
        {
            Debug.Log("No player found in the scene. Make sure it has the tag 'Player'");
        }
        bossDamageble = boss.GetComponent<Damageable>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthSlider.value = CalculateSliderPercentage(bossDamageble.Health, bossDamageble.MaxHealth);
    }

    private void OnEnable()
    {
        bossDamageble.healthChanged.AddListener(OnPlayerHealthChanged);
    }

    private void OnDisable()
    {
        bossDamageble.healthChanged.RemoveListener(OnPlayerHealthChanged);
    }

    private float CalculateSliderPercentage(float currentHealth, float maxHealth)
    {
        return currentHealth / maxHealth;
    }

    // Update is called once per frame
    private void OnPlayerHealthChanged(int newHealth, int maxHealth)
    {
        healthSlider.value = CalculateSliderPercentage(newHealth, maxHealth);
    }
}
