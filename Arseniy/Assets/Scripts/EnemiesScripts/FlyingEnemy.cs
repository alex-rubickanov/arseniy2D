using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlyingEnemy : MonoBehaviour
{
    public float amplitude = 1f; // Амплитуда синусоиды
    public float frequency = 1f; // Частота синусоиды
    public float speed = 1f; // Скорость движения

    private Vector3 startPosition;

    [SerializeField] private float _health = 100f;
    [SerializeField] private Slider _healthBar;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        CheckDeath();
        Move();
    }

    private void Move()
    {
        float y = startPosition.y + Mathf.Sin(Time.time * speed * frequency) * amplitude;
        transform.position = Vector3.MoveTowards(transform.position, new Vector3(-4f, y, 0), 1f * Time.deltaTime);
    }

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            Die();
        }
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _healthBar.value = _health;
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}

