using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BombProjectile : Projectile
{
    Mortar mortar;
    Vector3 projectileTarget;
    

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
                case "StoneEnemy":
                    damageMultiplier = 1f;
                    break;
            }

            damageable.TakeDamage(damage, damageMultiplier);
            Debug.Log(damageMultiplier + collision.tag);
        }
    }

    private void Start()
    {
        mortar = GameObject.Find("Mortar").GetComponent<Mortar>();
        projectileTarget = mortar.target;
    }
    private void Update()
    {
        if(mortar != null) {
            if(gameObject.transform.position != projectileTarget) {
                transform.position = Vector3.MoveTowards(transform.position, projectileTarget, mortar.projectileSpeed * Time.deltaTime);
            } else {

                transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                transform.GetComponent<Animator>().SetTrigger("Explosion");
                GetComponent<CircleCollider2D>().enabled = true;
            }
        }
    }
    public void DestroyAfterAnimation()
    {
        Destroy(gameObject);
    }
}
