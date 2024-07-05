using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//LUYENDDBDBDB
public class PlayerController : SingleTon<PlayerController>
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    
    [SerializeField] private float moveSpeed;
    [SerializeField] private float dashSpeed;
    [SerializeField] private TrailRenderer myTrailRenderer;
    [SerializeField] private Transform weaponCollider;
    private PlayerControls playerControls;
    private Vector2 moverment;
    Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private KnockBack knockBack;
    private bool facingLeft = false;
    private bool isDashing = false;
    protected override void Awake() 
    {  
        base.Awake(); 
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        knockBack = GetComponent<KnockBack>();  
    }
    private void Start()
    {
        playerControls.Combat.Dash.performed += _ => Dash();
    }
    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void Update()
    {
        PlayerInput();
    }
    private void FixedUpdate()
    {
        AdjustPlayerFacingDirection();
        Move();
    }
    public Transform GetWeaponCollider()
    {
        return weaponCollider;
    }
    private void PlayerInput()
    {
        moverment = playerControls.Moverment.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("move.x", moverment.x);
        myAnimator.SetFloat("move.y", moverment.y);
    }
    private void Move()
    {
        if (knockBack.GettingKnockBack) { return; }
        rb.MovePosition(rb.position + moverment*(moveSpeed * Time.fixedDeltaTime));
    }
    private void AdjustPlayerFacingDirection()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 playerPoint = Camera.main.WorldToScreenPoint(transform.position);
        if (mousePos.x < playerPoint.x)
        {
            mySpriteRenderer.flipX = true;
            facingLeft = true;
        }
        else
        {
            mySpriteRenderer.flipX = false;
            facingLeft = false;
        }
    }
    private void Dash() {
        if (!isDashing)
        {
            isDashing = true;
            moveSpeed += dashSpeed;
            myTrailRenderer.emitting = true;
            StartCoroutine(EndDashRoutine());
        }
    }
    private IEnumerator EndDashRoutine()
    {
        float dashTime = .2f;
        float dashCD = .25f;
        yield return new WaitForSeconds(dashTime);
        moveSpeed -= dashSpeed;
        myTrailRenderer.emitting = false;
        yield return new WaitForSeconds(dashCD);
        isDashing = false;

    }
}
