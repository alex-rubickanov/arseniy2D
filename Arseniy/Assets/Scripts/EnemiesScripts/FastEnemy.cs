using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    private void Awake()
    {
        healthBar.value = health;
        health = 50;
        damage = 3;
        _wall = GameObject.Find("Wall");
    }
}
