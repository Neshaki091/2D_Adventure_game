using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHeath : MonoBehaviour
{
    [SerializeField] private int startHeath = 3;
    [SerializeField] private GameObject slimeDeathVFX;

    private int currentHeath;
    private KnockBack knockBack;
    private Flash flash;
    private PlayerHealth health;
    private void Awake()
    {
        flash = GetComponent<Flash>();
        knockBack = GetComponent<KnockBack>();
    }
    private void Start()
    {
        currentHeath = startHeath;
    }
    public void takeDame(int Damage)
    {
        currentHeath -= Damage;
        knockBack.getKnockBack(PlayerController.Instance.transform, 15f);
        StartCoroutine(flash.FlashRoutine());
        StartCoroutine(CheckDetectDeathRoutine());
    }
    private IEnumerator CheckDetectDeathRoutine()
    {
        yield return new WaitForSeconds(flash.GetRestoreMatTime());
        detectDeath();
    }
    public void detectDeath()
    {
        if (currentHeath <= 0)
        {
            GetComponent<Pickupspawner>().DropItems();
            Instantiate(slimeDeathVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
