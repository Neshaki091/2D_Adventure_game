using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
   
    private int currentHealth;
    private bool canTakeDamage = true;
    private KnockBack knockback;
    private Flashofplayer flash;
         void Awake()
    {

        flash = GetComponent<Flashofplayer>();
        knockback = GetComponent<KnockBack>();
    }
    private void Start()
    {
        
        currentHealth = maxHealth;
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy &&canTakeDamage)
        {
            TakeDamge(1);
            knockback.getKnockBack(other.gameObject.transform, knockBackThrustAmount);
            StartCoroutine(flash.FlashRoutine());
        }
    }
    private void TakeDamge(int damageAmount)
    {
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(DamageRecoveryRoutine());
    }
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }
}
