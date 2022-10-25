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

    float f_Vertical;
    float f_Horizontal;
    float f_moveLimiter = 0.7f;
    public float f_RunSpeed = 0.0f;
    private Rigidbody2D rb_Player;
    public SpriteRenderer sr_Player;

    void Start()
    {
        rb_Player = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        f_Horizontal = Input.GetAxisRaw("Horizontal");
        f_Vertical = Input.GetAxisRaw("Vertical");
    }

    private void FixedUpdate() {
        if (f_Horizontal != 0 && f_Vertical != 0) {

            f_Horizontal *= f_moveLimiter;
            f_Vertical *= f_moveLimiter;
        }

        if(f_Horizontal == 1) {
            Direction = PlayerDirection.Right;
        } else if (f_Horizontal == -1) {
            Direction = PlayerDirection.Left;
        } else if (f_Vertical == 1) {
            Direction = PlayerDirection.Up;
        } else if (f_Vertical == -1) {
            Direction = PlayerDirection.Down;
        }

        rb_Player.velocity = new Vector2(f_Horizontal * f_RunSpeed, f_Vertical * f_RunSpeed);
        ChangePlayerSprite();
    }

    public void ChangePlayerSprite() {
        switch (Direction) {
            case PlayerDirection.Left:
                sr_Player.color = Color.red;
                return;
            case PlayerDirection.Right:
                sr_Player.color = Color.blue;
                return;
            case PlayerDirection.Up:
                sr_Player.color = Color.yellow;
                return;
            case PlayerDirection.Down:
                sr_Player.color = Color.green;
                return;
            default:
                break;
        }
    }
}
