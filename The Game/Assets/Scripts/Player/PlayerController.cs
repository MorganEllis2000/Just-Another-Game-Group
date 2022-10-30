using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GunDirection {
    NE,
    NW,
    SW,
    SE,
    NONE
}
public enum PlayerDirection {
    FRONT,
    BACK
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    protected PlayerDirection playerDirection;
    protected GunDirection gunDirection;

    private float _Vertical;
    private float _Horizontal;


    private float _moveLimiter = 0.7f;

    [Tooltip("This describes how fast the player will move")]
    [Range(0f, 30f)]
    [SerializeField] protected float runSpeed = 0.0f;

    protected Rigidbody2D rigidBody2D;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    // DASH VARIABLES

    protected bool canDash = true;
    public bool isDashing;
    [Tooltip("This describes the strength of the players dash")]
    [Range(0f, 30f)]
    [SerializeField] protected float dashingPower = 24f;
    [Tooltip("This describes how long the player can dash for")]
    [Range(0f, 2f)]
    [SerializeField] protected float dashingTime = 0.2f;
    [Tooltip("This describes the strength of the players dash")]
    [Range(0f, 5f)]
    [SerializeField] protected float dashingCooldown = 1f;
    [SerializeField] protected TrailRenderer trailRenderer;

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
    }

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDashing == true) {
            return;
        }

        _Horizontal = Input.GetAxisRaw("Horizontal");
        _Vertical = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash == true) {
            StartCoroutine(Dash());
        }

        //Debug.Log((int)playerDirection);
    }

    private void FixedUpdate() {
        if(isDashing == true) {
            return;
        }

        if (_Horizontal != 0 || _Vertical != 0) {
            animator.SetBool("IsRunning", true);
        } else {
            SetGunDirection(GunDirection.NONE);
            animator.SetBool("IsRunning", false);
        }

        

        if (_Horizontal != 0 && _Vertical != 0) {
            _Horizontal *= _moveLimiter;
            _Vertical *= _moveLimiter;
        }

        //if(_Horizontal == 1) {
        //    Direction = PlayerDirection.Right;
        //} else if (_Horizontal == -1) {
        //    Direction = PlayerDirection.Left;
        //} else if (_Vertical == 1) {
        //    Direction = PlayerDirection.Up;
        //} else if (_Vertical == -1) {
        //    Direction = PlayerDirection.Down;
        //}

        rigidBody2D.velocity = new Vector2(_Horizontal * runSpeed, _Vertical * runSpeed);


        ChangePlayerSprite();
    }

    private IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        float originalGravity = rigidBody2D.gravityScale;
        rigidBody2D.gravityScale = 0.0f;
        switch (gunDirection) {
            case GunDirection.NE:
                rigidBody2D.velocity = new Vector2(transform.localScale.x * dashingPower * _moveLimiter, transform.localScale.y * dashingPower * _moveLimiter);
                break;
            case GunDirection.NW:
                rigidBody2D.velocity = new Vector2(-transform.localScale.x * dashingPower * _moveLimiter, transform.localScale.y * dashingPower * _moveLimiter);
                break;
            case GunDirection.SW:
                rigidBody2D.velocity = new Vector2(-transform.localScale.x * dashingPower * _moveLimiter, -transform.localScale.y * dashingPower * _moveLimiter);
                break;
            case GunDirection.SE:
                rigidBody2D.velocity = new Vector2(transform.localScale.x * dashingPower * _moveLimiter, -transform.localScale.y * dashingPower * _moveLimiter);
                break;
            default:
                break;
        }
        //rigidBody2D.velocity = new Vector2(transform.localScale.x * dashingPower, transform.localScale.y * dashingPower);
        trailRenderer.emitting = true;
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(dashingTime);
        spriteRenderer.enabled = true;
        trailRenderer.emitting = false;
        rigidBody2D.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    public void ChangePlayerSprite() {
        UpdateGunDirection();
        UpdatePlayerDirection();
    }

    public void UpdateGunDirection() {
        switch (gunDirection) {
            case GunDirection.SE:
                //spriteRenderer.color = Color.red;
                spriteRenderer.flipX = false;
                animator.SetInteger("SetGunDirection", (int)gunDirection);
                return;
            case GunDirection.NE:
                //spriteRenderer.color = Color.blue;
                spriteRenderer.flipX = false;
                animator.SetInteger("SetGunDirection", (int)gunDirection);
                return;
            case GunDirection.NW:
                //spriteRenderer.color = Color.yellow;
                spriteRenderer.flipX = true;
                animator.SetInteger("SetGunDirection", (int)gunDirection);
                return;
            case GunDirection.SW:
                //spriteRenderer.color = Color.green;
                spriteRenderer.flipX = true;
                animator.SetInteger("SetGunDirection", (int)gunDirection);
                return;
            case GunDirection.NONE:
                animator.SetInteger("SetGunDirection", (int)gunDirection);
                return;
            default:
                break;
        }
    }

    public void UpdatePlayerDirection() {
        switch (playerDirection) {
            case PlayerDirection.FRONT:
                animator.SetInteger("SetPlayerDirection", (int)playerDirection);
                return;
            case PlayerDirection.BACK:
                animator.SetInteger("SetPlayerDirection", (int)playerDirection);
                return;
            default:
                break;

        }
    }

    public PlayerDirection SetPlayerDirection(PlayerDirection direction) {
        return playerDirection = direction;
    }

    public GunDirection SetGunDirection(GunDirection direction) {
        return gunDirection = direction;
    }
}
