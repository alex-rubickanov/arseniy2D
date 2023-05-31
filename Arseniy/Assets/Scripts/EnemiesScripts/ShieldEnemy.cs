using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldEnemy : Enemy
{
    public bool isShieldAlive = true;

    [Header("----------SHIELD PROPERTIES----------")]
    [SerializeField] public float shieldHealth;
    [SerializeField] public float shieldArmor;
    [SerializeField] public float shieldArrowDamageResist;
    [SerializeField] public float shieldBombDamageResist;
    [SerializeField] public float shieldFireDamageResist;
    [SerializeField] private float shieldBombDamageXAxisReduce = 0.2f;


    private bool triggerHit = false;
    public void TakeDamage(float weaponDamage, string weaponName, GameObject projectile)
    {

        switch (weaponName) {
            case BALLISTA:
                currentDamageResist = arrowDamageResist;
                break;
            case MORTAR:
                currentDamageResist = bombDamageResist;
                break;
            case FIREGUN:
                currentDamageResist = fireDamageResist;
                break;
        }



        damageReduce = armor / (armor + 400);
        actualDamage = (weaponDamage * (1 - damageReduce)) * (1 - currentDamageResist);
        if(weaponName == MORTAR) {
            if(isShieldAlive) {
                if (projectile.transform.position.x > transform.position.x - shieldBombDamageXAxisReduce) {
                    health -= actualDamage;
                    healthBar.value = health;
                }
            } else {
                health -= actualDamage;
                healthBar.value = health;
            }
        }  
    }


    public override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Wall") {
            isAttacking = true;
        }
    }
}
