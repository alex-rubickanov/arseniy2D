using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    [SerializeField] private float _health = 200;
    [SerializeField] private Slider _healthBar;

    // Start is called before the first frame update
    void Start()
    {
        _healthBar.value = _health;
    }

    // Update is called once per frame
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

    public void TakeDamage(float damage)
    {
        _health -= damage;
        _healthBar.value = _health;
    }

    private void Death()
    {
        Destroy(gameObject);
    }
}
