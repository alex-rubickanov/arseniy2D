using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ArrowProjectile : Projectile
{
    Crossbow crossbow;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            float damageMultiplier = 1.0f;

            switch (collision.tag)
            {
                case "StoneEnemy":
                    damageMultiplier = 1.5f;
                    break;
                case "Shield":
                    damageMultiplier = 0.5f;
                    break;
                case "Enemy With Shield":
                    damageMultiplier = 0.75f;
                    break;
                case "FlyingEnemy":
                    damageMultiplier = 1.25f;
                    break;
            }

            damageable.TakeDamage(damage, damageMultiplier);
            crossbow.isSentProjectileDropped = true;
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

        DestroyThisIn(10f);
    }
}
