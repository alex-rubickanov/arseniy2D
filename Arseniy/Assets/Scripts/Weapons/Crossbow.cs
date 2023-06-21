using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crossbow : Weapon
{

    
    [HideInInspector]public Vector3 target;
    GameObject sentProjectile;
    bool isSentProjectileDropped = true;

    [Space]
    [Header("----------PROPERTIES----------")]
    [SerializeField] public float projectileDamage;
    [SerializeField] public float projectileSpeed = 10f;
    [SerializeField] float reloadTime = 2f;

    [SerializeField] GameObject superProjectile;
    [SerializeField] float coolDownTime;
    [SerializeField] int superShootsCount = 3;
    [SerializeField] public float superProjectileSpeed = 10f;


    bool isReloading = false;
    bool isSuperPowerActivated = false;

    public override void Shoot()
    {
        if (!isReloading) {
            sentProjectile = GameObject.Instantiate(projectile, projectileSpawnerTransform.position, transform.rotation);
            sentProjectile.GetComponent<Rigidbody2D>().AddForce(projectileSpawnerTransform.right * projectileSpeed, ForceMode2D.Impulse);
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    public void SuperShoot()
    {
        if (!isReloading)
        {
            sentProjectile = GameObject.Instantiate(superProjectile, projectileSpawnerTransform.position, transform.rotation);
            sentProjectile.GetComponent<Rigidbody2D>().AddForce(projectileSpawnerTransform.right * superProjectileSpeed, ForceMode2D.Impulse);
            isReloading = true;
            superShootsCount--;
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
                        
                        if(isSuperPowerActivated && superShootsCount > 0)
                        {
                            SuperShoot();
                        }
                        else
                        {
                            Shoot();
                        }
                    }
                }
            }
            if (Input.GetMouseButtonUp(1))
            {
                //CrosshairDisabled();
            }
        }

        if (superShootsCount == 0)
        {
            isSuperPowerActivated = false;
            StartCoroutine(Cooldown());
        }
    }

    public void ActivatePower()
    {
        isSuperPowerActivated = true;
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }
    private IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(coolDownTime);
        superShootsCount = 3;
    }
}
