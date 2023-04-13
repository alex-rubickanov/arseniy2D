using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : Projectile
{
    Crossbow crossbow; 



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            crossbow.isSentProjectileDropped = true;
            Destroy(gameObject);
        }

        if(collision.tag == "MovingEnemy")
        {
            collision.GetComponent<MovingEnemy>().TakeDamage(damage);
            crossbow.isSentProjectileDropped = true;
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        crossbow = GameObject.Find("Crossbow").GetComponent<Crossbow>();

        DestroyThisIn(4f);
    }

}
