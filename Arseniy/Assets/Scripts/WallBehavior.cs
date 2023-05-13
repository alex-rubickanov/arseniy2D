using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBehavior : MonoBehaviour
{
    [SerializeField] private float _health;
    private Renderer _renderer;
    private int _blinkCount = 5;

    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void TakeDamage(float damage)
    {
        _health -= damage * Time.deltaTime;
        StartCoroutine(BlinkCoroutine());
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        float damage = 0;

        switch (collision.tag)
        {
            case "Enemy":
                damage = 1;
                TakeDamage(damage);
                break;
            case "Rock":
                 damage = 2;
                TakeDamage(damage);
                break;
            case "Enemy With Shield":
                damage = 3;
                TakeDamage(damage);
                break;
            case "FlyingEnemy":
                 damage = 4;
                TakeDamage(damage);
                break;
        }
    }

    private IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < _blinkCount; i++)
        {
            _renderer.material.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            _renderer.material.color = Color.gray;
            yield return new WaitForSeconds(0.2f);
        }
    }
}
