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
    protected PlayerDirection playerDirection;
    protected GunDirection gunDirection;

    private float _Vertical;
    private float _Horizontal;


    private float _moveLimiter = 0.7f;

    [Tooltip("This describes how fast the player will move")]
    [Range(0f, 30f)]
    [SerializeField] protected float f_RunSpeed = 0.0f;

    protected Rigidbody2D rigidBody2D;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _Horizontal = Input.GetAxisRaw("Horizontal");
        _Vertical = Input.GetAxisRaw("Vertical");

        Debug.Log((int)playerDirection);
    }

    private void FixedUpdate() {
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

        rigidBody2D.velocity = new Vector2(_Horizontal * f_RunSpeed, _Vertical * f_RunSpeed);
        ChangePlayerSprite();
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
