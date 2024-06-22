using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool FacingLeft { get { return facingLeft; } set { facingLeft = value; } }
    public static PlayerController Instance;
    [SerializeField] int speed;

    private PlayerControls playerControls;
    private Vector2 moverment;
    Rigidbody2D rb;
    private Animator myAnimator;
    private SpriteRenderer mySpriteRenderer;
    private bool facingLeft = false;
    private void Awake()
    {
        Instance = this;
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
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
    private void PlayerInput()
    {
        moverment = playerControls.Moverment.Move.ReadValue<Vector2>();
        myAnimator.SetFloat("move.x", moverment.x);
        myAnimator.SetFloat("move.y", moverment.y);
    }
    private void Move()
    {
        rb.MovePosition(rb.position + moverment*(speed * Time.fixedDeltaTime));
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
}
