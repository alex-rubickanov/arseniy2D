using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public Slider healthBar;
    [SerializeField] WallBehavior wall;
    float lastAttackTime;
    bool isAttacking = false;

    [Space]
    [Header("----------PROPERTIES----------")]
    [SerializeField] public float health;
    [SerializeField] public float speed = 0.5f;
    [SerializeField] public float damage;
    [SerializeField] float attackCooldown = 2f;


    [Header("----------DAMAGE RESIST----------")]
    [SerializeField] float armor;
    [SerializeField] float arrowDamageResist;
    [SerializeField] float bombDamageResist;
    [SerializeField] float fireDamageResist;
    float damageReduce;
    float actualDamage;


    private void Awake()
    {
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

    public void TakeDamage(float weaponDamage, )
    {
        damageReduce = armor / (armor + 400);
        actualDamage = (weaponDamage * (1 - damageReduce)) * (1 /*- damageResist*/);
        health -= actualDamage;

        healthBar.value = health;
    }
    
    private void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Move()
    {
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
