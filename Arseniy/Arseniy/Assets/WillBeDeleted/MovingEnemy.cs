using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovingEnemy : MonoBehaviour
{
    [SerializeField] float health = 100f;
    [SerializeField] Slider healthBar;

    private void Start()
    {
        healthBar.value = health;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-4f, 0, 0), 1f * Time.deltaTime);
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
