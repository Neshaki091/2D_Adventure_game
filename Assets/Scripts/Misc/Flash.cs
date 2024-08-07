using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flash : MonoBehaviour
{
    [SerializeField] private Material WhiteFlashMat;
    [SerializeField] private float restoreDefaultTime = .2f;

    private Material defaultMat;
    private SpriteRenderer spriteRenderer;
    

    private void Awake()
    {
          
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMat = spriteRenderer.material;
    }
    public float GetRestoreMatTime()
    {
        return restoreDefaultTime;
    }
    public IEnumerator FlashRoutine()
    {
        spriteRenderer.material = WhiteFlashMat;
        yield return new WaitForSeconds(restoreDefaultTime);
        spriteRenderer.material = defaultMat;
        
    }
}
