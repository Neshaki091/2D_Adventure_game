using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashAniPrefab;
    [SerializeField] private Transform slashSpawmPoint;
    [SerializeField] private float SAttackcooldown = .5f;
    [SerializeField] private WeaponInfo weaponInfo;
    private Transform weaponCollider;
    private Animator myAnimator;


    private GameObject slashAni;     
    private void Awake()
    {
        myAnimator = GetComponent<Animator>();
    }
    private void Start()
    {
        weaponCollider = PlayerController.Instance.GetWeaponCollider();
        slashSpawmPoint = GameObject.Find("SlashSpawmPoint").transform;
    }
    private void Update()
    {
        MouseFollowingOffset();
    }
    public WeaponInfo GetWeaponInfo()
    {
        return weaponInfo;
    }
    public void Attack()
    {
        
            myAnimator.SetTrigger("Attack");
            weaponCollider.gameObject.SetActive(true);
            slashAni = Instantiate(slashAniPrefab, slashSpawmPoint.position, Quaternion.identity);
            slashAni.transform.parent = this.transform.parent;
        
    }
    public void DoneAttack()
    {
        weaponCollider.gameObject.SetActive(false);
    }
    public void swingUpAni()
    {
        slashAni.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        if (PlayerController.Instance.FacingLeft) { 
        slashAni.GetComponent<SpriteRenderer>().flipX = true;
    }
    }
    public void swingDownAni()
    {
        slashAni.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        if (PlayerController.Instance.FacingLeft)
        {
            slashAni.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
    
    private void MouseFollowingOffset()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPoint = Camera.main.WorldToScreenPoint(PlayerController.Instance.transform.position);

        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        if (mousePos.x < playerPoint.x)
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0,-180,angle);
            weaponCollider.transform.rotation= Quaternion.Euler(0,-180,angle) ;
        } else
        {
            ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0, 0, angle);
            weaponCollider.transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
