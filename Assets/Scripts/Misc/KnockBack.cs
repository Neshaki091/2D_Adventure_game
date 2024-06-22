using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public bool GettingKnockBack { get; private set; }
    [SerializeField] private float knockBackTime = .1f;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void getKnockBack(Transform DamageSource, float KnockbackThrust)
    {
        GettingKnockBack = true;
        Vector2 different = (transform.position - DamageSource.position).normalized * KnockbackThrust * rb.mass;
        rb.AddForce(different, ForceMode2D.Impulse);
        StartCoroutine(knockroutine());
    }
    private IEnumerator knockroutine()
    {
        yield return new WaitForSeconds(knockBackTime);
        rb.velocity = Vector2.zero;
        GettingKnockBack = false;
    }
}
