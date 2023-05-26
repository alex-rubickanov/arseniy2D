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
}
