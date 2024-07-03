using System.Collections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAniPrefab;
    [SerializeField] private Transform slashSpawmPoint;
    [SerializeField] private GameObject WeaponCollider;
    [SerializeField] private float SAttackcooldown = .5f;

    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAni;     
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }

    private void Update()
    {
        MouseFollowingOffset();
    }
    public void Attack()
    {
        
            //isAttacking = true;
            myAnimator.SetTrigger("Attack");
            WeaponCollider.gameObject.SetActive(true);
            slashAni = Instantiate(slashAniPrefab, slashSpawmPoint.position, Quaternion.identity);
            slashAni.transform.parent = this.transform.parent;
            StartCoroutine(AttackCDRoutine());
        
    }
    private IEnumerator AttackCDRoutine()
    {
        yield return new WaitForSeconds(SAttackcooldown);
        ActiveWeapon.Instance.ToggleIsAttacking(false);
    }
    public void DoneAttack()
    {
        WeaponCollider.gameObject.SetActive(false);
    }
    public void swingUpAni()
    {
        slashAni.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        if (playerController.FacingLeft)
        {
            slashAni.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    public void swingDownAni()
    {
        slashAni.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (playerController.FacingLeft)
        {
            slashAni.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    private void MouseFollowingOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPoint = Camera.main.WorldToScreenPoint(playerController.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        if (mousePos.x < playerPoint.x)
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0,-180,angle);
            WeaponCollider.transform.rotation= Quaternion.Euler(0,-180,angle) ;
        } else
        {
            activeWeapon.transform.rotation = Quaternion.Euler(0, 0, angle);
            WeaponCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
