using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] Slider healthBar;

    private void Start()
    {
        healthBar.value = health;
    }

    private void Update()
    {
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }
}
