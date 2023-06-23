using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Mortar : Weapon
{
     bool isReloading = false;

    GameObject sentProjectile;
    [HideInInspector] public Vector3 target;

    [Space]
    [Header("----------PROPERTIES----------")]
    [SerializeField] public float projectileDamage;
    [SerializeField] public float projectileSpeed = 10f;
    [SerializeField] float reloadTime = 2f;

    [SerializeField] GameObject superProjectile;
    [SerializeField] float coolDownTime;
    [SerializeField] int superShootsCount = 3;
    [SerializeField] public float superProjectileSpeed = 10f;
    [SerializeField] Button button;

    bool isSuperPowerActivated = false;


    public override void Aim()
    {
        Vector3 mousePosition = Utils.GetMouseWorldPosition();


        Vector3 aimDirection = (mousePosition - playerTransform.transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        transform.eulerAngles = new Vector3(0, 0, Mathf.Clamp(angle, -weaponRotationClamp, weaponRotationClamp));
    }
    
    private void Update()
    {
        if (playerScript.activeGun == Player.Weapon.Mortar)
        {
            if (Input.GetMouseButton(1))
            {
                Crosshair();
                CrosshairUnHide();
                Aim();
                if (!isReloading)
                {
                    if (Input.GetMouseButtonDown(0))
                    {
                        target = crosshair.transform.position;

                        if (isSuperPowerActivated && superShootsCount > 0)
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
                CrosshairHide();
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

    public override void Shoot()
    {
        if (!isReloading)
        {
            GameObject.Instantiate(projectile, projectileSpawnerTransform.position, transform.rotation);
            isReloading = true;
            StartCoroutine(Reload());
        }
    }

    public  void SuperShoot()
    {
        if (!isReloading)
        {
            GameObject.Instantiate(superProjectile, projectileSpawnerTransform.position, transform.rotation);
            isReloading = true;
            superShootsCount--;
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        yield return new WaitForSeconds(reloadTime);
        isReloading = false;
    }

    private IEnumerator Cooldown()
    {
        ColorBlock colors = button.colors;
        Color originalColor = colors.normalColor;

        colors.normalColor = Color.red;
        button.colors = colors;

        yield return new WaitForSeconds(coolDownTime);

        colors.normalColor = originalColor;
        button.colors = colors;

        superShootsCount = 3;
    }
}
