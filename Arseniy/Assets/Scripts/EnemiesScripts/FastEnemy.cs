using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    public override void Die()
    {
        Destroy(gameObject);
        gameManager.UpdateScore(score);
        enemySpawner.DecreaseEnemiesCount();
        enemySpawner.KilledFastEnemiesIncrease();
    }
}
