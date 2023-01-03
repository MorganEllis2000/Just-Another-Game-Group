using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class Tree : Enemy
{
    [SerializeField] private GameObject branch;
    [SerializeField] private GameObject ThrowPoint;
    [SerializeField] private GameObject Roots;
    [SerializeField] private GameObject navMeshSurface;

    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        UpdateTargetPosition();
        if (Health > 0) {

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("A_TreeIdle_FRONT") && DistanceOfAiToPlayer() > TransformationRange) {
                CanAttack = false;
            }

            if (DistanceOfAiToPlayer() < TransformationRange && CanAttack == false && animator.GetBool("CanTransform") == false) {
                CanMove = false;
                animator.SetBool("CanTransform", true);
                StartCoroutine(WaitForAnimationToFinish());
            }

            if (CanMove == true) {
                CheckEnemyDirection();
                UpdateEnemyDirection(spriteRenderer);

                if (DistanceOfAiToPlayer() < ShortRangeAttackRange) {
                    if (CanAttack == true) {
                        StartCoroutine(ShortRangeAttack());
                    }
                } else if (DistanceOfAiToPlayer() < MaxLongRangeAttackRange && DistanceOfAiToPlayer() > MinLongRangeAttackRange) {
                    if(animator.GetBool("IsThrowing") == false) {
                        StopCoroutine(LongRangeAttack());
                        StopCoroutine(ShortRangeAttack());
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
            }
        } else {
            if(SceneManager.GetActiveScene().name == "WFC") {
                WFCExample.Instance.currentRoom.NumberOfEnemies -= 1;
            }

            Destroy(this.gameObject);
        }
    }

    public IEnumerator LongRangeAttack() {
        CanAttack = false;
        animator.SetBool("IsThrowing", true);
        GameObject tempBranch = GameObject.Instantiate(branch, ThrowPoint.transform.position, ThrowPoint.transform.rotation);
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false; this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(0.5f);

        this.gameObject.GetComponent<NavMeshAgent>().enabled = true;
        animator.SetBool("IsThrowing", false);
        yield return new WaitForSeconds(6.0f);
        
        CanAttack = true;
    }

    public IEnumerator ShortRangeAttack() {
        CanAttack = false;
        animator.SetBool("CanAttackClose", true);
        this.gameObject.GetComponent<NavMeshAgent>().enabled = false;
        yield return new WaitForSeconds(0.75f);  

        Instantiate(Roots, PlayerController.Instance.transform.position, Roots.transform.rotation);
        yield return new WaitForSeconds(0.75f);

        this.gameObject.GetComponent<NavMeshAgent>().enabled = true;
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
