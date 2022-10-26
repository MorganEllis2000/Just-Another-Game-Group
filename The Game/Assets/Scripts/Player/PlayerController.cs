using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public enum PlayerDirection {
        Left, 
        Right, 
        Up, 
        Down
    }

    public PlayerDirection Direction;

    private float _Vertical;
    private float _Horizontal;


    private float _moveLimiter = 0.7f;

    [Tooltip("This describes how fast the player will move")]
    [Range(0f, 30f)]
    [SerializeField] protected float f_RunSpeed = 0.0f;

    protected Rigidbody2D rigidBody2D;
    public SpriteRenderer spriteRenderer;

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        _Horizontal = Input.GetAxisRaw("Horizontal");
        _Vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        if (_Horizontal != 0 && _Vertical != 0) {

            _Horizontal *= _moveLimiter;
            _Vertical *= _moveLimiter;
        }

        if(_Horizontal == 1) {
            Direction = PlayerDirection.Right;
        } else if (_Horizontal == -1) {
            Direction = PlayerDirection.Left;
        } else if (_Vertical == 1) {
            Direction = PlayerDirection.Up;
        } else if (_Vertical == -1) {
            Direction = PlayerDirection.Down;
        }

        rigidBody2D.velocity = new Vector2(_Horizontal * f_RunSpeed, _Vertical * f_RunSpeed);
        ChangePlayerSprite();
    }

    public void ChangePlayerSprite() {
        switch (Direction) {
            case PlayerDirection.Left:
                spriteRenderer.color = Color.red;
                return;
            case PlayerDirection.Right:
                spriteRenderer.color = Color.blue;
                return;
            case PlayerDirection.Up:
                spriteRenderer.color = Color.yellow;
                return;
            case PlayerDirection.Down:
                spriteRenderer.color = Color.green;
                return;
            default:
                break;
        }
    }
}
