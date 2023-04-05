using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mortar : Weapon
{
    
    [SerializeField] float projectileSpeed = 10f;
    GameObject sentProjectile;
    public Vector3 target;
    public bool isSentProjectileDropped = true;
    
    


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
                CrosshairHide();
            }

            
        }

        if (sentProjectile != null)
        {
            sentProjectile.transform.position = Vector3.MoveTowards(sentProjectile.transform.position, target, projectileSpeed * Time.deltaTime);
            if (sentProjectile.transform.position == target)
            {
                isSentProjectileDropped = true;
                sentProjectile.transform.GetChild(0).GetComponent<Renderer>().enabled = false;
                sentProjectile.transform.GetComponent<Animator>().SetTrigger("Explosion");
                sentProjectile.GetComponent<CircleCollider2D>().enabled = true;

            }
            else
            {
                
                isSentProjectileDropped = false;
            }

        }
    }

    

    public override void Shoot()
    {
        Debug.Log("Shoot");
        sentProjectile = GameObject.Instantiate(projectile, projectileSpawnerTransform.position, transform.rotation);
    }
}
