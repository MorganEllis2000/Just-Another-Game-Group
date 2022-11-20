using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : Enemy
{

    private GameObject CloseRangeCollider;

    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        CloseRangeCollider = this.gameObject.transform.GetChild(0).gameObject;
    }

    void Update() {
        if (Health > 0) {

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("IDLE") && DistanceOfAiToPlayer() > TransformationRange) {
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
                    if (animator.GetBool("CanAttackClose") == false && animator.GetBool("IsThrowing") == false) {
                        ChasePlayer();
                    }

                    if (CanAttack == true) {
                        StartCoroutine(ShortRangeAttack());
                    }
                } else if (DistanceOfAiToPlayer() < LongRangeAttackRange && DistanceOfAiToPlayer() > 6.0f) {
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
                } else if (DistanceOfAiToPlayer() > LongRangeAttackRange) {
                    StopCoroutine(LongRangeAttack());
                    StopCoroutine(ShortRangeAttack());
                    ReturnToOriginalPos();
                }
            }
        } else {
            Destroy(this.gameObject);
        }
    }

    public IEnumerator LongRangeAttack() {
        CanAttack = false;
        Debug.Log("Long Range Attacking Player");
        animator.SetBool("IsThrowing", true);
        //GameObject tempBranch = GameObject.Instantiate(branch, ThrowPoint.transform.position, ThrowPoint.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsThrowing", false);
        yield return new WaitForSeconds(6.0f);
        CanAttack = true;
    }

    public IEnumerator ShortRangeAttack() {
        CanAttack = false;
        Debug.Log("Short Range Attacking Player");
        animator.SetBool("CanAttackClose", true);
        yield return new WaitForSeconds(1.5f);
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
