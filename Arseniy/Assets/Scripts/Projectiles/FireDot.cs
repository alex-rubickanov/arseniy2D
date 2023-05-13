using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDot : MonoBehaviour
{
    private Enemy parentEnemy;
    [SerializeField] private int ticks = 5;
    [SerializeField] private float damage = 2;

    private void Start()
    {
        parentEnemy = GetComponent<Enemy>();
        StartCoroutine(DotDamage());
    }


    private IEnumerator DotDamage()
    {
        for(int i = 0; i < ticks; i++) {
            parentEnemy.TakeDamage(damage, 1);
            yield return new WaitForSeconds(2f);
        }

        Destroy(this);
    }

}
