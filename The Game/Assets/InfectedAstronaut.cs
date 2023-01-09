using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class InfectedAstronaut : Enemy
{
    public GameObject Shotgun;
    [SerializeField] private GameObject ammo;
    [SerializeField] private GameObject FirePoint;
    private float SpreadAngle = 40f;
    private int NumberOfProjectiles = 8;
    [SerializeField] private bool CanShoot = true;
    [SerializeField] private float ShootCooldown;


    private bool CanDash = true;
    private bool isDashing = true;
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
    private float _moveLimiter = 0.7f;
    [SerializeField] private int DashPercentage;


    [SerializeField] private Slider HealthBar;
    private Rigidbody2D rigidBody2D;

    [SerializeField] private GameObject RootsBlockingExit;

    // Start is called before the first frame update

    [SerializeField] private GameObject GameCompletePanel;
    void Start()
    {
        rigidBody2D = GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        HealthBar.maxValue = Health;
        StartCoroutine(PlayTransformation());
    }

    

    // Update is called once per frame
    void Update()
    {
        HealthBar.value = Health;

        UpdateTargetPosition();
        if (Health > 0 && CanMove == true) {

            
            ChasePlayer();
            CheckEnemyDirection();
            UpdateEnemyDirection(spriteRenderer);
            if (CanShoot == true) {
                CanDash = false;
                ShootPlayer();
                StartCoroutine(CanShootPlayer());
            }

            if (GameObject.FindGameObjectWithTag("Bullet") != null) {
                if (Vector3.Distance(this.gameObject.transform.position, GameObject.FindGameObjectWithTag("Bullet").transform.position) < 5) {
                    if (CanDash == true) {
                        StartCoroutine(Dash());
                    }
                }
            }
        } else if (CanMove == true && Health <= 0) {
            StartCoroutine(WaitForDeath());
        }

        Vector3 dir = PlayerController.Instance.transform.position - Shotgun.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        Shotgun.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        if(PlayerController.Instance.transform.position.x < this.transform.position.x) {
            Shotgun.GetComponent<SpriteRenderer>().flipY = true;
        } else {
            Shotgun.GetComponent<SpriteRenderer>().flipY = false;
        } 
    }

    public bool LineOfSight() {

        RaycastHit hit;
        if(Physics.Raycast(FirePoint.transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity, 0)) {
            if(hit.transform.CompareTag("Player")) {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                return true;
            } 
        } else {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 1000, Color.white);
        }

        return false;
    }

    public IEnumerator PlayTransformation() {
        animator.Play("Base Layer.A_InfectedAstronautTransforming");
        yield return new WaitForSeconds(3);
        this.GetComponent<PolygonCollider2D>().enabled = true;
        CanMove = true;
        Shotgun.SetActive(true);
    }
    public IEnumerator WaitForDeath() {
        animator.SetBool("IsWalking", false);
        this.GetComponent<NavMeshAgent>().isStopped = true;
        this.GetComponent<PolygonCollider2D>().enabled = false;
        this.rigidBody2D.velocity = Vector2.zero;
        animator.SetFloat("Direction", -1);
        animator.Play("Base Layer.A_InfectedAstronautTransforming", -1, float.NegativeInfinity);
        yield return new WaitForSeconds(3);
        Destroy(RootsBlockingExit);
        Destroy(this.gameObject);
        GameCompletePanel.SetActive(true);
        Time.timeScale = 0;
    }


    public void ShootPlayer() {
        CanShoot = false;
        for (int i = 0; i < NumberOfProjectiles; i++) {

            float angleStep = SpreadAngle / NumberOfProjectiles;
            float aimingAngle = FirePoint.transform.rotation.eulerAngles.z;
            float centeringOffset = (SpreadAngle / 2) - (angleStep / 2);

            float currentBulletAngle = angleStep * i;

            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, aimingAngle + currentBulletAngle - centeringOffset));
            GameObject bullet = Instantiate(ammo, FirePoint.transform.position, rotation);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(bullet.transform.right * 15, ForceMode2D.Impulse);
        }

        CanDash = true;
    }

    public IEnumerator CanShootPlayer() {
        yield return new WaitForSeconds(ShootCooldown);
        CanShoot = true;
    }


    public IEnumerator Dash() {
        int DashChance = Random.Range(1, 100);
        Debug.Log(DashChance);
        CanDash = false;
        if (DashChance > DashPercentage) {
            
            this.GetComponent<NavMeshAgent>().isStopped = true;
            isDashing = true;
            float originalGravity = rigidBody2D.gravityScale;
            rigidBody2D.gravityScale = 0.0f;

            int DashDirection = Random.Range(1, 8);

            if (DashDirection == 1) {
                rigidBody2D.velocity = new Vector2(transform.localScale.x * dashingPower * _moveLimiter, transform.localScale.y * dashingPower * _moveLimiter);
            } else if (DashDirection == 2) {
                rigidBody2D.velocity = new Vector2(-transform.localScale.x * dashingPower * _moveLimiter, transform.localScale.y * dashingPower * _moveLimiter);
            } else if (DashDirection == 3) {
                rigidBody2D.velocity = new Vector2(0, transform.localScale.y * dashingPower * _moveLimiter);
            } else if (DashDirection == 4) {
                rigidBody2D.velocity = new Vector2(transform.localScale.x * dashingPower * _moveLimiter, -transform.localScale.y * dashingPower * _moveLimiter);
            } else if (DashDirection == 5) {
                rigidBody2D.velocity = new Vector2(-transform.localScale.x * dashingPower * _moveLimiter, -transform.localScale.y * dashingPower * _moveLimiter);
            } else if (DashDirection == 6) {
                rigidBody2D.velocity = new Vector2(0, -transform.localScale.y * dashingPower * _moveLimiter);
            } else if (DashDirection == 7) {
                rigidBody2D.velocity = new Vector2(-transform.localScale.x * dashingPower * _moveLimiter, 0);
            } else if (DashDirection == 8) {
                rigidBody2D.velocity = new Vector2(transform.localScale.x * dashingPower * _moveLimiter, 0);
            }

            //DashSound.Play();
            trailRenderer.emitting = true;
            spriteRenderer.enabled = false;
            yield return new WaitForSeconds(dashingTime);
            spriteRenderer.enabled = true;
            trailRenderer.emitting = false;
            rigidBody2D.gravityScale = originalGravity;
            isDashing = false;
            this.GetComponent<NavMeshAgent>().isStopped = false;
            rigidBody2D.velocity = Vector2.zero;
            yield return new WaitForSeconds(dashingCooldown);
            CanDash = true;
        } else {
            yield return new WaitForSeconds(1);
            CanDash = true;
        }
    }
}
