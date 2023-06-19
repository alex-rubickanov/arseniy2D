using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] public Slider healthBar;
    [HideInInspector] public WallBehavior wall;
    float lastAttackTime;
    [HideInInspector] public bool isAttacking = false;

    [Space]
    [Header("----------PROPERTIES----------")]
    [SerializeField] public float maxHealth;
    [SerializeField] public float health;
    [SerializeField] public float speed = 0.5f;
    [SerializeField] public float damage;
    [SerializeField] float attackCooldown = 2f;


    [Header("----------DAMAGE RESIST----------")]
    [SerializeField] public float armor;
    [SerializeField] public float arrowDamageResist;
    [SerializeField] public float bombDamageResist;
    [SerializeField] public float fireDamageResist;
    public float currentDamageResist;
    public const string BALLISTA = "Ballista";
    public const string MORTAR = "Mortar";
    public const string FIREGUN= "FireGun";

    [HideInInspector] public float damageReduce;
    [HideInInspector] public float actualDamage;



    private void Awake()
    {
        wall = GameObject.Find("Wall").GetComponent<WallBehavior>();

        health = maxHealth;
        healthBar.maxValue = maxHealth; 
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

        UpdateHealthBar();
        CheckDeath();
    }

    protected void CheckDeath()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void TakeDamage(float weaponDamage, string weaponName)
    {
        
        switch (weaponName) {
            case BALLISTA:
                currentDamageResist = arrowDamageResist;
                break;
            case MORTAR:
                currentDamageResist = bombDamageResist;
                break;
            case FIREGUN:
                currentDamageResist = fireDamageResist;
                break;
        }
            


        damageReduce = armor / (armor + 400);
        actualDamage = (weaponDamage * (1 - damageReduce)) * (1 - currentDamageResist);
        health -= actualDamage;
    }
    
    public virtual void Die()
    {
        Destroy(gameObject);
    }

    public virtual void Move()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;
    }

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Wall" ) {
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

    private void UpdateHealthBar()
    {
        healthBar.value = health;
    }
}
