using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SuperArrowProjectile : Projectile
{
    Crossbow crossbow;
    private string NAME_OF_WEAPON = "Ballista";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null) {

            enemy.TakeDamage(damage, NAME_OF_WEAPON);
        }

        if (collision.CompareTag("Shield") || collision.CompareTag("Fast Enemy") || collision.CompareTag("FlyingEnemy"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.tag == "Border")
        {
            Destroy(gameObject);

        }
    }

    private void Start()
    {
        crossbow = GameObject.Find("Crossbow").GetComponent<Crossbow>();
        damage = crossbow.projectileDamage;
        DestroyThisIn(10f);
    }
}
