using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastEnemy : Enemy
{
    public override void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown)
        {
            return;
        }

        animator.SetBool("IsAttacking", true);
        lastAttackTime = Time.time;
        wall.GetComponent<WallBehavior>().TakeDamage(damage);
    }

    public override void Die()
    {
        animator.SetBool("IsDead", true);
        Destroy(gameObject);
        gameManager.UpdateScore(score);
        enemySpawner.DecreaseEnemiesCount();
        enemySpawner.KilledFastEnemiesIncrease();
    }

}
