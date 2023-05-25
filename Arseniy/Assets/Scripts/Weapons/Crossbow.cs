using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{

    [SerializeField] public float projectileSpeed = 10f;
    public Vector3 target;
    GameObject sentProjectile;
    public bool isSentProjectileDropped = true;
<<<<<<< Updated upstream
=======
    [SerializeField] float reloadTime = 2f;
    [SerializeField] bool canShoot = true;
    [SerializeField] bool isReloading = false;
>>>>>>> Stashed changes

    [SerializeField] float projectileDamage;

    public override void Shoot()
    {
        //Debug.Log("Shoot");
        if (!isReloading)
        {
            sentProjectile = GameObject.Instantiate(projectile, projectileSpawnerTransform.position, transform.rotation);
            sentProjectile.GetComponent<Rigidbody2D>().AddForce(projectileSpawnerTransform.right * projectileSpeed, ForceMode2D.Impulse);
            canShoot = false;
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    public override void Aim()
    {
        Vector3 mousePosition = Utils.GetMouseWorldPosition();

        Vector3 aimDirection = (mousePosition - playerTransform.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(angle, -weaponRotationClamp, weaponRotationClamp));
    }

    private void Update()
    {
        if (playerScript.activeGun == Player.Weapon.Crossbow)
        {
            if (Input.GetMouseButton(1))
            {
                Crosshair();
                Aim();
                if (isSentProjectileDropped)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        target = crosshair.transform.position;
                        Shoot();

                    }
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                //CrosshairDisabled();
            }

            
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        canShoot = true;
        isReloading = false;
    }
}
