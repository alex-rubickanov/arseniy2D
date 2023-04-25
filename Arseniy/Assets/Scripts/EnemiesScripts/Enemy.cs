using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] public float health = 100;
    [SerializeField] public Slider healthBar;
    [SerializeField] public float speed = 0.5f;

    [SerializeField] bool isAttacking = false;

    private void Awake()
    {
        healthBar.value = health;
    }

    private void Update()
    {
        if (!isAttacking) 
        {
            Move();
        } 
        else if(isAttacking) 
        {
            Attack();
        }

        CheckDeath();
    }

    private void CheckDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage,  float damageMultiplier)
    {
        int actualDamage = Mathf.RoundToInt(damage * damageMultiplier);
        health -= actualDamage;
        healthBar.value = health;
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Move()
    {
        //transform.position = Vector3.MoveTowards(transform.position, new Vector3(-4f, transform.position.y, transform.position.z), speed * Time.deltaTime);
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Wall") {
            isAttacking = true;
        }
    }

    public virtual void Attack()
    {

    }
}
