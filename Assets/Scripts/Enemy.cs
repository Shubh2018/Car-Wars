using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Enemy
{
    public float maxHealth;
    public int range;

    private float currentHealth;

    public int damage;

    public int damagePerBullet;
    public int noOfBullets;

    [SerializeField] private Slider healthSlider;

    public Enemy()
    {
        maxHealth = 0f;
        range = 0;
    }

    public void SetHealth()
    {
        currentHealth = maxHealth;

        healthSlider.value = (float)(currentHealth / maxHealth);
    }

    public void UpdateEnemyHealth(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
            currentHealth = 0;

        float health = (float)(currentHealth / maxHealth);

        healthSlider.value = health;
    }

    public float TotalDamage()
    {
        return damagePerBullet * noOfBullets;
    }

    public bool IsDead()
    {
        if (currentHealth <= 0)
        {
            GameManager.Instance._enemyDeathCount += 1;
            return true;
        }
        else
            return false;
    }
}
