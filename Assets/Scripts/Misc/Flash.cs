using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material WhiteFlashMat;
    [SerializeField] private float restoreDefaultTime = .2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    private EnemyHeath enemyHeath;

    private void Awake()
    {
        enemyHeath = GetComponent<EnemyHeath>();    
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }

    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = WhiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultTime);
        spriteRenderer.material = defaultMat;
        enemyHeath.detectDeath();
    }
}
