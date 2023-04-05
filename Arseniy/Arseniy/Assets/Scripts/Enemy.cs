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
        if (health <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        healthBar.value = health;
    }
    
    private void Death()
    {
        Destroy(gameObject);
    }
}
