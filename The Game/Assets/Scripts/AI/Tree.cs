using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : Enemy
{
    [SerializeField] private GameObject branch;
    [SerializeField] private GameObject ThrowPoint;

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
            Destroy(this.gameObject);
        }
    }

    public IEnumerator LongRangeAttack() {
        CanAttack = false;
        Debug.Log("Long Range Attacking Player");
        animator.SetBool("IsThrowing", true);
        GameObject tempBranch = GameObject.Instantiate(branch, ThrowPoint.transform.position, ThrowPoint.transform.rotation);
        yield return new WaitForSeconds(0.5f);
        animator.SetBool("IsThrowing", false);
        yield return new WaitForSeconds(6.0f);
        CanAttack = true;
    }

    public IEnumerator ShortRangeAttack() {
        CanAttack = false;
        Debug.Log("Attacking Player");
        yield return new WaitForSeconds(6.0f);
        CanAttack = true;
    }

    public IEnumerator WaitForAnimationToFinish() {
        yield return new WaitForSeconds(animator.runtimeAnimatorController.animationClips[0].length + 1.5f);
        CanMove = true;
        CanAttack = true;
    }
}
