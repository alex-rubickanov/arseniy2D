using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombProjectile : Projectile
{
    Mortar mortar;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.GetComponent<IDamageable>();
        if (damageable != null)
        {
            float damageMultiplier = 1.0f;

            switch (collision.tag)
            {
                case "Fast Enemy":
                    damageMultiplier = 1.5f;
                    break;
                case "Shield":
                    damageMultiplier = 0.5f;
                    break;
                case "Enemy With Shield":
                    damageMultiplier = 0.75f;
                    break;
                case "FlyingEnemy":
                    damageMultiplier = 0f;
                    break;
            }

            damageable.TakeDamage(damage, damageMultiplier);
            Debug.Log(damageMultiplier + collision.tag);
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
