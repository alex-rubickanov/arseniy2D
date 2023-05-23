using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour, IDamageable
{
    [SerializeField] private float _health = 200;
    [SerializeField] private Slider _healthBar;

    private void Awake()
    {
        _healthBar.maxValue = _health;
        _healthBar.value= _health;
    }
    
    void Update()
    {
        CheckDeath();
    }

    private void CheckDeath()
    {
        if (_health <= 0)
        {
            Death();
        }
    }

    public void TakeDamage(float damage, float damageMultiplier)
    {
        _health -= damage;
        _healthBar.value = _health;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
