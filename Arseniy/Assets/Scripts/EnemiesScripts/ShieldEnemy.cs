using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldEnemy : Enemy
{
    private void Awake()
    {
        health = 75;
        healthBar.value = health;
        damage = 7;
        _wall = GameObject.Find("Wall");
    }
}
