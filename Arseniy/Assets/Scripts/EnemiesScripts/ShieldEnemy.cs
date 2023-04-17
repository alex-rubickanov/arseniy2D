using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldEnemy : Enemy
{
    [SerializeField] private float _health = 150f;
    [SerializeField] private float _speed;

    // Start is called before the first frame update
    void Start()
    {
        healthBar.value = _health;
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    public override void Movement()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-4f, 0, 0), _speed * Time.deltaTime);
    }
}
