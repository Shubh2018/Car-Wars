using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Player
{
    public int carID;

    public float maxHealth;
    public float maxSpeed;
    
    [Range(0, 1)] public float control;
    [Range(0, 1)] public float speedMultiplier;

    private float currentHealth;

    [SerializeField] private Slider healthSlider;

    public Player()
    {
        carID = 0;
        maxHealth = 0;
        maxSpeed = 0.0f;
        control = 0.0f;
    }

    public void AddHealth(float health)
    {
        currentHealth += health;

        if(currentHealth > maxHealth)
            currentHealth = maxHealth;

        healthSlider.value = (float)(currentHealth / maxHealth);
    }

    public void SetHealth()
    {
        currentHealth = maxHealth;

        healthSlider.value = (float)(currentHealth / maxHealth);
    }

    public void UpdateHealth(float damage)
    {
        currentHealth -= damage;

        if(currentHealth < 0)
            currentHealth = 0;

        healthSlider.value = (float)(currentHealth / maxHealth);
    }

    public float CurrentHealth()
    {
        return currentHealth;
    }

    public bool IsDead()
    {
        if (currentHealth <= 0)
            return true;
        else
            return false;
    }

    public Slider GetHealthSlider()
    {
        return healthSlider;
    }
}
