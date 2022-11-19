using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseRangeAttackNode : Node {

    private GameObject enemyAI;
    private float EnemyMoveSpeed;
    private Vector3 originalPosition;
    private bool CanAttack = false;
    private bool HasFinishedTransforming = false;
    public CloseRangeAttackNode(GameObject enemyAI, Vector3 originalPosition, float enemyMoveSpeed) {
        this.enemyAI = enemyAI;
        this.EnemyMoveSpeed = enemyMoveSpeed;
        this.originalPosition = originalPosition;
    }

    public override NodeState Evaluate() {
        if (DistanceOfAiToPlayer() < 10.0f && CanAttack == false && enemyAI.GetComponent<Animator>().GetBool("CanTransform") == false) {
            HasFinishedTransforming = false;
            enemyAI.GetComponent<Animator>().SetBool("CanTransform", true);
            //enemyAI.GetComponent<Animator>().SetBool("CanTransformBack", false);
            PerformCoroutine(WaitForAnimationToFinish());
        }

        if(HasFinishedTransforming == true) {
            if (DistanceOfAiToPlayer() < 3.0f) {
                if (CanAttack == true) {
                    PerformCoroutine(AttackPlayer());
                }
                return NodeState.RUNNING;
            } else if (DistanceOfAiToPlayer() < 6.0f ) {
                ChasePlayer();
                return NodeState.RUNNING;
            } else {
                return NodeState.FAILURE;
            }
        } else {
            return NodeState.RUNNING;
        }
    }

    public IEnumerator AttackPlayer() {
        CanAttack = false;
        Debug.Log("Attacking Player");
        yield return new WaitForSeconds(3.0f);
        CanAttack = true;
    }

    public void ChasePlayer() {
        enemyAI.GetComponent<Animator>().SetBool("IsWalking", true);
        enemyAI.transform.position = Vector2.MoveTowards(enemyAI.transform.position, PlayerController.Instance.gameObject.transform.position, 3 * Time.deltaTime);
    }
    public float DistanceOfAiToPlayer() {
        return Vector2.Distance(enemyAI.transform.position, PlayerController.Instance.transform.position);
    }

    public void ReturnToOriginalPos() {
        enemyAI.transform.position = Vector2.MoveTowards(enemyAI.transform.position, originalPosition, 3 * Time.deltaTime);
        //if (Vector2.Distance(enemyAI.transform.position, originalPosition) < 0.1f) {
        //    enemyAI.GetComponent<Animator>().SetBool("IsWalking", false);
        //    enemyAI.GetComponent<Animator>().SetBool("CanTransform", false);
        //    enemyAI.GetComponent<Animator>().SetBool("CanTransformBack", true);
        //}
    }

    public IEnumerator WaitForAnimationToFinish() {
        Debug.Log(enemyAI.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
        yield return new WaitForSeconds(enemyAI.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length + 1.5f);
        CanAttack = true;
        HasFinishedTransforming = true;
    }
}
