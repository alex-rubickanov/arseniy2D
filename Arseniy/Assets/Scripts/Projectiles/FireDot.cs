using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDot : MonoBehaviour
{
    private Enemy parentEnemy;
    private int ticks;
    private float damage;
    private float delay;

    FireGun fireGun;

    private void Start()
    {
        fireGun = GameObject.Find("FireGun").GetComponent<FireGun>();

        damage = fireGun.dotDamage;
        ticks = fireGun.dotTicks;
        delay = fireGun.dotDelay;
        parentEnemy = GetComponent<Enemy>();
        StartCoroutine(DotDamage());
    }


    private IEnumerator DotDamage()
    {
        for (int i = 0; i < ticks; i++) {
            parentEnemy.TakeDamage(damage);
            yield return new WaitForSeconds(delay);
        }

        Destroy(this);
    }

}