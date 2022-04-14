using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image currentHealthBar;
    [SerializeField] private Image maxHealthBar;
    
    private Health playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        // Fetch playerHealth
        playerHealth = GameManager.GetInstance().ActivePlayer.GetComponent<Health>();

        // Call init method
        InitializeHealthbar();
    }

    // Update is called once per frame
    void Update()
    {
        // Call healthbar update method
        UpdateHealthbar();
    }

    private void InitializeHealthbar()
    {
        // Initialize max healthbar
        if (playerHealth)
        {
            maxHealthBar.fillAmount = playerHealth.CurrentHealth / 10;
            Debug.Log("Max Health bar initialized");
        }
    }

    private void UpdateHealthbar()
    {
        // Initialize max healthbar
        if (playerHealth)
        {
            // Update current health bar
            currentHealthBar.fillAmount = playerHealth.CurrentHealth / 10;
        }
        else
        {
            // If there's no active player, set healthbar to zero
            currentHealthBar.fillAmount = 0;
        }
    }
}
