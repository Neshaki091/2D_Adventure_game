 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    [SerializeField] private int Damage = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<EnemyHeath>())
        {
            EnemyHeath enemyHeath = other.gameObject.GetComponent<EnemyHeath>();
            enemyHeath.takeDame(Damage);
        }
    }
}
