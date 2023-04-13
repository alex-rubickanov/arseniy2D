using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : Projectile
{
    Mortar mortar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            mortar.isSentProjectileDropped = true;
            
        }
        if (collision.tag == "MovingEnemy")
        {
            collision.GetComponent<MovingEnemy>().TakeDamage(damage);
            mortar.isSentProjectileDropped = true;

        }
    }

    private void Start()
    {
        mortar = GameObject.Find("Mortar").GetComponent<Mortar>();
    }

    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}
