using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGun : Weapon
{
    [SerializeField] ParticleSystem fireParticle;
    [SerializeField] BoxCollider2D boxCollider;

    [SerializeField] float damage;
    

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
        boxCollider.enabled = true;

    }

    public void StopShoot()
    {
        fireParticle.Stop();
        boxCollider.enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Debug.Log("FIRE DAMAGE");
            collision.GetComponent<Enemy>().TakeDamage(damage * Time.fixedDeltaTime);
        }
        if (collision.tag == "Shield")
        {
            Debug.Log("FIRE DAMAGE");
            collision.GetComponent<Shield>().TakeDamage(damage * Time.fixedDeltaTime);
        }
        if (collision.tag == "Enemy With Shield")
        {
            Debug.Log("FIRE DAMAGE");
            collision.GetComponent<ShieldEnemy>().TakeDamage(damage * Time.fixedDeltaTime);
        }
    }
}
