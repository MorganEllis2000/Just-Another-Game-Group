using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Rock : Enemy
{

    private GameObject CloseRangeCollider;
    [SerializeField] private GameObject ThrowPoint;
    [SerializeField] private GameObject branch;

    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        CloseRangeCollider = this.gameObject.transform.GetChild(0).gameObject;
        source = this.GetComponent<AudioSource>();
    }

    void Update() {
        UpdateTargetPosition();


        if (Health > 0) {

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE") && DistanceOfAiToPlayer() > TransformationRange) {
                CanAttack = false;
            }

            if (DistanceOfAiToPlayer() < TransformationRange && CanAttack == false && animator.GetBool("CanTransform") == false) {
                CanMove = false;
                animator.SetBool("CanTransform", true);
                source.clip = TransformSound;
                source.volume = 0.06f;
                source.Play();
                StartCoroutine(WaitForAnimationToFinish());
            }

            if (CanMove == true) {
                this.GetComponent<PolygonCollider2D>().enabled = true;
                CheckEnemyDirection();
                UpdateEnemyDirection(spriteRenderer);

                if (DistanceOfAiToPlayer() < ShortRangeAttackRange) {
                    if (animator.GetBool("CanAttackClose") == false && animator.GetBool("IsThrowing") == false) {
                        ChasePlayer();
                    }

                    if (CanAttack == true) {
                        StartCoroutine(ShortRangeAttack());
                    }
                } else if (DistanceOfAiToPlayer() < MaxLongRangeAttackRange && DistanceOfAiToPlayer() > MinLongRangeAttackRange) {
                    if (animator.GetBool("IsThrowing") == false && animator.GetBool("CanAttackClose") == false) {
                        ChasePlayer();
                    }

                    if (CanAttack == true) {
                        StartCoroutine(LongRangeAttack());
                    }

                } else if (DistanceOfAiToPlayer() < 6.0f) {
                    StopCoroutine(LongRangeAttack());
                    StopCoroutine(ShortRangeAttack());
                    ChasePlayer();
                } else if (DistanceOfAiToPlayer() > MaxLongRangeAttackRange) {
                    StopCoroutine(LongRangeAttack());
                    StopCoroutine(ShortRangeAttack());
                    ReturnToOriginalPos();
                }
            } else {
                this.GetComponent<PolygonCollider2D>().enabled = false;
            }
        } else {
            if(animator.GetBool("IsDead") == false) {
                StartCoroutine(WaitForDeath());
            }
            
        }
    }

    public IEnumerator WaitForDeath() {
        animator.SetBool("IsDead", true);
        this.GetComponent<NavMeshAgent>().isStopped = true;
        this.GetComponent<PolygonCollider2D>().enabled = false;
        source.clip = DeathSound;
        source.volume = 0.1f;
        source.Play();
        yield return new WaitForSeconds(3);
        if (SceneManager.GetActiveScene().name == "WFC") {
            WFCExample.Instance.currentRoom.NumberOfEnemiesLeft -= 1;
        }
        
        Destroy(this.gameObject);
    }


    public IEnumerator LongRangeAttack() {
        CanAttack = false;
        animator.SetBool("IsThrowing", true);
        source.clip = ThrowSound;
        source.volume = 0.03f;
        source.Play();
        GameObject tempBranch = GameObject.Instantiate(branch, ThrowPoint.transform.position, ThrowPoint.transform.rotation);
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false; 
        yield return new WaitForSeconds(0.5f);

        this.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        animator.SetBool("IsThrowing", false);
        yield return new WaitForSeconds(6.0f);

        CanAttack = true;
    }

    public IEnumerator ShortRangeAttack() {
        CanAttack = false;

        animator.SetBool("CanAttackClose", true);
        this.gameObject.GetComponent<NavMeshAgent>().isStopped = true;
        this.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        yield return new WaitForSeconds(0.75f);
        source.clip = SmashSound;
        source.Play();
        source.volume = 0.06f;
        yield return new WaitForSeconds(0.75f);

        this.gameObject.GetComponent<NavMeshAgent>().isStopped = false;
        animator.SetBool("CanAttackClose", false);
        yield return new WaitForSeconds(2.5f);

        CanAttack = true;
    }

    public IEnumerator WaitForAnimationToFinish() {
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[0].length + 1.5f);
        CanMove = true;
        CanAttack = true;
    }

    
}
