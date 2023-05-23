using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField] public float health;
    [SerializeField] public Slider healthBar;
    [SerializeField] public float speed = 0.5f;
    [SerializeField] public float damage;

     bool isAttacking = false;

    [SerializeField] WallBehavior wall;
    float lastAttackTime;
    [SerializeField] float attackCooldown = 2f;

    private void Awake()
    {
        healthBar.value = health;
        wall = GameObject.Find("Wall").GetComponent<WallBehavior>();

        healthBar.maxValue = health; 
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

    protected void CheckDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage,  float damageMultiplier)
    {
        health -= damage * damageMultiplier;
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
        if(Time.time - lastAttackTime < attackCooldown) {
            return;
        }
        lastAttackTime = Time.time;
        wall.GetComponent<WallBehavior>().TakeDamage(damage);
    }
}
