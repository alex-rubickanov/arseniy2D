using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArrowProjectile : Projectile
{
    Crossbow crossbow;
    private string NAME_OF_WEAPON = "Ballista";
    private bool hasEntered = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();

        if (enemy != null && !hasEntered) {

            enemy.TakeDamage(damage, NAME_OF_WEAPON);
            hasEntered = true;
            Destroy(gameObject);
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
