using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    [SerializeField] private GameObject slashAniPrefab;
    [SerializeField] private Transform slashSpawmPoint;
    [SerializeField] private GameObject WeaponCollider;

    private PlayerControls playerControls;
    private Animator myAnimator;
    private PlayerController playerController;
    private ActiveWeapon activeWeapon;

    private GameObject slashAni;
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
        playerControls = new PlayerControls();
        playerController = GetComponentInParent<PlayerController>();
        activeWeapon = GetComponentInParent<ActiveWeapon>();
    }
    private void OnEnable()
    {
        playerControls.Enable();
        
    }
   
    void Start()
    {
        playerControls.Combat.Attack.started += _ => Attack();
    }
    private void Update()
    {
        MouseFollowingOffset();
    }
    private void Attack()
    {
        myAnimator.SetTrigger("Attack");
        WeaponCollider.gameObject.SetActive(true);

        slashAni = Instantiate(slashAniPrefab, slashSpawmPoint.position, Quaternion.identity);
        slashAni.transform.parent = this.transform.parent;
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
