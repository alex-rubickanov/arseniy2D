using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : Weapon
{
    [SerializeField] ParticleSystem fireParticle;
    [SerializeField] PolygonCollider2D fireCollider;
    private string NAME_OF_WEAPON = "FireGun";



    [Header("----------PROPERTIES----------")]
    [SerializeField] float damage;

    [Header("----------DOT PROPERTIES----------")]
    [SerializeField] public float dotDamage;
    [SerializeField] public int dotTicks;
    [SerializeField] public float dotDelay;

    [Header("----------ULT PROPERTIES----------")]
    [SerializeField] private float percentOfSmallEnemies;
    [SerializeField] private float percentOfBigEnemies;

    public override void Aim()
    {
        Vector3 mousePosition = Utils.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - playerTransform.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(angle, -weaponRotationClamp, weaponRotationClamp));
    }

    private void Update()
    {
        if (playerScript.activeGun == Player.Weapon.FireGun)
        {
            if (Input.GetMouseButton(1))
            {
                Aim();
                if (Input.GetMouseButtonDown(0))
                {
                    Shoot();
                }
            }
            if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            {
                StopShoot();
            }
        }

        if(playerScript.activeGun != Player.Weapon.FireGun)
        {
            StopShoot();
        }
    }

    public override void Shoot()
    {
        fireParticle.Play();
        fireCollider.enabled = true;

    }

    public void StopShoot()
    {
        fireParticle.Stop();
        fireCollider.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if(enemy != null) {
            if(collision.tag == "Enemy With Shield") {
                if (collision.GetComponent<ShieldEnemy>().isShieldAlive == false) {
                    Debug.Log("FIRE DAMAGE");
                    collision.GetComponent<ShieldEnemy>().TakeDamage(damage * Time.fixedDeltaTime, NAME_OF_WEAPON);

                    if (!collision.GetComponent<FireDot>()) {
                        collision.gameObject.AddComponent<FireDot>();
                    }
                }
            } else {
                Debug.Log("FIRE DAMAGE");
                collision.GetComponent<Enemy>().TakeDamage(damage * Time.fixedDeltaTime, NAME_OF_WEAPON);

                if (!collision.GetComponent<FireDot>()) {
                    collision.gameObject.AddComponent<FireDot>();
                }
            }
        }
    }

    public void FireGunAbility()
    {
        if(playerScript.activeGun == Player.Weapon.FireGun) {
            Enemy[] enemies = FindObjectsOfType<Enemy>();
            foreach (Enemy enemy in enemies) {
                if (enemy.GetComponent<ShieldEnemy>() != null || enemy.GetComponent<StoneEnemy>() != null) {
                    float onePercentHealth = enemy.maxHealth / 100;
                    enemy.health -= onePercentHealth * percentOfBigEnemies;
                } else if (enemy.GetComponent<Shield>() != null) {
                    // no damage to shield
                } else {
                    float onePercentHealth = enemy.maxHealth / 100;
                    enemy.health -= onePercentHealth * percentOfSmallEnemies;
                }
            }
        }
    }
}
