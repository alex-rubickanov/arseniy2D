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
       if(_health < 0)
        {
            Die();
        } 
    }

    public void TakeDamage(float damage)
    {
        _health -= damage * Time.deltaTime;
        StartCoroutine(BlinkCoroutine());
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private IEnumerator BlinkCoroutine()
    {
        for (int i = 0; i < _blinkCount; i++)
        {
            _renderer.material.color = Color.red;
            yield return new WaitForSeconds(0.5f);
            _renderer.material.color = Color.gray;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
