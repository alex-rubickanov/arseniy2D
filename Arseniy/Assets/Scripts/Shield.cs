using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Shield : Enemy
{
    public string NOTE = "CHANGE SHIELD PROPERTIES IN ENEMY WITH SHIELD PREFAB";

    [SerializeField] ShieldEnemy parentEnemy;

    private void Start()
    {
        parentEnemy = gameObject.GetComponentInParent<ShieldEnemy>();
        armor = parentEnemy.shieldArmor;
        arrowDamageResist = parentEnemy.shieldArrowDamageResist;
        bombDamageResist = parentEnemy.shieldBombDamageResist;
        fireDamageResist = parentEnemy.shieldFireDamageResist;
    }
    void Update()
    {
        CheckDeath();
    }

    

    private void Death()
    {
        gameObject.GetComponentInParent<ShieldEnemy>().isShieldAlive = false;
        Destroy(gameObject);
    }
}
