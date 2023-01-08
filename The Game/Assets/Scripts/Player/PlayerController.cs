using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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

public enum OneHandedWeapons {
    NONE,
    PISTOL
}

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance { get; private set; }

    protected PlayerDirection playerDirection;
    protected GunDirection gunDirection;
    protected OneHandedWeapons oneHandedWeapons;

    public float _Vertical;
    public float _Horizontal;


    private float _moveLimiter = 0.7f;

    [Tooltip("This describes how fast the player will move")]
    [Range(0f, 30f)]
    [SerializeField] public float runSpeed = 0.0f;

    [Range(0f, 200f)]
    [SerializeField] public float Health = 100;
    [SerializeField] public float MaxHealth = 100;

    public float MaxOxygen;
    public float Oxygen;

    public bool IsTalking = false;

    public Rigidbody2D rigidBody2D;
    protected SpriteRenderer spriteRenderer;
    protected Animator animator;

    [SerializeField] private GameObject GameOverPanel;

    

    // DASH VARIABLES

    public bool canDash = true;
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

    [SerializeField] AudioSource DashSound;

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;

        DontDestroyOnLoad(this);
    }

    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        if(SceneManager.GetActiveScene().name == "Tutorial") {
            Oxygen = Oxygen;
        } else {
            Oxygen = MaxOxygen;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Health > 0) {
            if (IsTalking == false) {
                if (Health > 0) {
                    if (isDashing == true) {
                        return;
                    }

                    _Horizontal = Input.GetAxisRaw("Horizontal");
                    _Vertical = Input.GetAxisRaw("Vertical");

                    if (Input.GetKey(KeyCode.LeftShift) && canDash == true) {
                        StartCoroutine(Dash());
                    }
                }
            }

            if (Oxygen > MaxOxygen) {
                Oxygen = MaxOxygen;
            }

            if (Input.mousePosition.y < this.transform.position.y) {
                SetPlayerDirection(PlayerDirection.FRONT);
            } else if (Input.mousePosition.y > this.transform.position.y) {
                SetPlayerDirection(PlayerDirection.BACK);
            }
        }
    }

    private void FixedUpdate() {
        if(IsTalking == false) {
            if (Health > 0) {
                if (isDashing == true) {
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

                rigidBody2D.velocity = new Vector2(_Horizontal * runSpeed, _Vertical * runSpeed);

                ChangePlayerSprite();
            } else {
                //Destroy(this.gameObject);
                //Time.timeScale = 0;
                StartCoroutine(PlayerDying());
            }
        }
    }

    public IEnumerator PlayerDying() {
        PlayerController.Instance.rigidBody2D.velocity = Vector2.zero;      
        animator.Play("Base Layer.A_PlayerDeath");
        yield return new WaitForSeconds(5);
        GameOverPanel.SetActive(true);
    }

    public IEnumerator Dash() {
        canDash = false;
        isDashing = true;
        float originalGravity = rigidBody2D.gravityScale;
        rigidBody2D.gravityScale = 0.0f;

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W)) {
            if (Input.GetKey(KeyCode.D)) {
                rigidBody2D.velocity = new Vector2(transform.localScale.x * dashingPower * _moveLimiter, transform.localScale.y * dashingPower * _moveLimiter);
            } else if (Input.GetKey(KeyCode.A)) {
                rigidBody2D.velocity = new Vector2(-transform.localScale.x * dashingPower * _moveLimiter, transform.localScale.y * dashingPower * _moveLimiter);
            } else {
                rigidBody2D.velocity = new Vector2(0, transform.localScale.y * dashingPower * _moveLimiter);
            }
        } else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S)) {
            if (Input.GetKey(KeyCode.D)) {
                rigidBody2D.velocity = new Vector2(transform.localScale.x * dashingPower * _moveLimiter, -transform.localScale.y * dashingPower * _moveLimiter);
            } else if (Input.GetKey(KeyCode.A)) {
                rigidBody2D.velocity = new Vector2(-transform.localScale.x * dashingPower * _moveLimiter, -transform.localScale.y * dashingPower * _moveLimiter);
            } else {
                rigidBody2D.velocity = new Vector2(0, -transform.localScale.y * dashingPower * _moveLimiter);
            }
        } else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A)) {
            rigidBody2D.velocity = new Vector2(-transform.localScale.x * dashingPower * _moveLimiter, 0);
        } else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D)) {
            rigidBody2D.velocity = new Vector2(transform.localScale.x * dashingPower * _moveLimiter, 0);
        }

        DashSound.Play();
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

    public void UpdatePlayerOneHandedWeapon() {
        switch (oneHandedWeapons) {
            case OneHandedWeapons.NONE:
                animator.SetInteger("SetOneHandedWeapon", (int)oneHandedWeapons);
                return;
            case OneHandedWeapons.PISTOL:
                animator.SetInteger("SetOneHandedWeapon", (int)oneHandedWeapons);
                return;
            default:
                break;
        }
    }

    public OneHandedWeapons SetOneHandedWeapon(OneHandedWeapons weapon) {
        return oneHandedWeapons = weapon;
    }

    public void TakeDamage(float damage) {
        Health -= damage;
        StartCoroutine(ChangeSpriteColour(0.2f));
    }

    public IEnumerator ChangeSpriteColour(float seconds) {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(seconds);
        spriteRenderer.color = Color.white;
    }

    public IEnumerator ChangeSpriteColour(Color color, float seconds) {
        spriteRenderer.color = color;
        yield return new WaitForSeconds(seconds);
        spriteRenderer.color = Color.white;
    }
}
