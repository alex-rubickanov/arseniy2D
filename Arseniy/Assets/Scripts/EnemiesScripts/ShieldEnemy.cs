using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldEnemy : MonoBehaviour
{
    [SerializeField] private float _health = 150f;
    [SerializeField] private float _speed;
    [SerializeField] private Slider _healthBar;

    // Start is called before the first frame update
    void Start()
    {
        _healthBar.value = _health;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        CheckDeath();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _healthBar.value = _health;
    }

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Move()
    {
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-4f, 0, 0), _speed * Time.deltaTime);
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
