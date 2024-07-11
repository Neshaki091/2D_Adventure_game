using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : SingleTon <PlayerHealth>
{
    public bool canUseMana { get { return true; } set { useMana = value; } }

    [SerializeField] private int maxHealth = 10;
    [SerializeField] private int maxMana = 20;
    [SerializeField] private float knockBackThrustAmount = 10f;
    [SerializeField] private float damageRecoveryTime = 1f;
   
    private Slider healthSlider;
    private Slider manaSlider;
    private int currentHealth;
    private int currentMana;
    private bool canTakeDamage = true;
    private KnockBack knockback;
    private Flashofplayer flash;
    private Rigidbody2D rb;
    private bool useMana;
    protected override void Awake()
    {
        base.Awake();

        flash = GetComponent<Flashofplayer>();
        knockback = GetComponent<KnockBack>();
    }

    private void Start()
    {
        
        currentHealth = maxHealth;
        currentMana = maxMana;
        updateHealthSlider();
        updateManaSlider();
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        EnemyAI enemy = other.gameObject.GetComponent<EnemyAI>();

        if (enemy &&canTakeDamage)
        {
            TakeDamge(1);
            knockback.getKnockBack(other.gameObject.transform, knockBackThrustAmount);

        }
    }
    public void HealPlayer()
    {
        if (currentHealth < maxHealth)
        {
            currentHealth += 1;
            updateHealthSlider();
        }
    }
    public void HealMana()
    {
        if (currentMana < maxMana)
        {
            currentMana += 4;
            if (currentMana > maxMana) { currentMana = maxMana; }
            updateManaSlider();
        }
    }
    private void TakeDamge(int damageAmount)
    { if(!canTakeDamage) { return;  }
        canTakeDamage = false;
        currentHealth -= damageAmount;
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(DamageRecoveryRoutine());
        updateHealthSlider();
        checkIfplayerDeath();
    }
    public void UseMana()
    {
        if (currentMana > 0)
        {
            useMana = true;
            currentMana -= 1;
            updateManaSlider();
        }
        else
        {
            useMana = false;
        }
        
    }
    private void checkIfplayerDeath()
    {
        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Player Death");
        }
    }
    private IEnumerator DamageRecoveryRoutine()
    {
        yield return new WaitForSeconds(damageRecoveryTime);
        canTakeDamage = true;
    }

    private void updateHealthSlider()
    {
        if (healthSlider == null)
        {
            healthSlider = GameObject.Find("Health Slider").GetComponent<Slider>();
        }
        healthSlider.maxValue = maxHealth;
        healthSlider.value = currentHealth;
    }
    private void updateManaSlider()
    {
        if (manaSlider == null)
        {
            manaSlider = GameObject.Find("Mana Slider").GetComponent<Slider>();
        }
        manaSlider.maxValue = maxMana;
        manaSlider.value = currentMana;
    }
}
