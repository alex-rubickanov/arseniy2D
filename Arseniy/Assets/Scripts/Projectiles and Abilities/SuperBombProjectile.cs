using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class SuperBombProjectile : Projectile
{
    Mortar mortar;
    Vector3 projectileTarget;
    [SerializeField] float magnetRadius = 5f;
    [SerializeField] float stunRadius = 2f;
    [SerializeField] float destroyTime = 2f;

    public static event EventHandler OnSuperBombActivation;

    public static void ResetStaticData()
    {
        OnSuperBombActivation = null;
    }

    private void Start()
    {
        mortar = GameObject.Find("Mortar").GetComponent<Mortar>();
        damage = mortar.projectileDamage;
        projectileTarget = mortar.target;
    }
    private void Update()
    {
        if(mortar != null)
        {
            if(gameObject.transform.position != projectileTarget) 
            {
                transform.position = Vector3.MoveTowards(transform.position, projectileTarget, mortar.projectileSpeed * Time.deltaTime);
            } 
            else 
            {
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(true);
                GetComponent<CircleCollider2D>().enabled = true;
                StartCoroutine(DestroyCoroutine(destroyTime));

                OnSuperBombActivation?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, magnetRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, stunRadius);
    }

    private IEnumerator DestroyCoroutine(float delay)
    {
        yield return new WaitForSeconds(delay);

        Enemy[] enemies = FindObjectsOfType<Enemy>();

        foreach (Enemy enemy in enemies)
        {
            enemy.GetUnstunned();
            enemy.GetUnattracted();
        }

        Destroy(gameObject);
    }
}
