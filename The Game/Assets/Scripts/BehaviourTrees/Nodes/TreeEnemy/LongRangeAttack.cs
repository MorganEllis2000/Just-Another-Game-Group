using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRangeAttack : Node
{
    private GameObject enemyAI;
    private float EnemyMoveSpeed;
    private bool CanAttack = true;
    private bool HasFinishedTransforming = false;
    private Vector3 originalPosition;
    private GameObject branch;
    public LongRangeAttack(GameObject enemyAI, float enemyMoveSpeed, Vector3 originalPosition, GameObject branch) {
        this.enemyAI = enemyAI;
        this.EnemyMoveSpeed = enemyMoveSpeed;
        this.originalPosition = originalPosition;
        this.branch = branch;
    }

    public override NodeState Evaluate() {
        if (DistanceOfAiToPlayer() > 6.0f && DistanceOfAiToPlayer() < 15.0f) {
            ChasePlayer();
            if (CanAttack == true) {
                PerformCoroutine(AttackPlayer());
            }
            return NodeState.RUNNING;
        } else if (DistanceOfAiToPlayer() > 15.0f) {
            ReturnToOriginalPos();
            return NodeState.RUNNING;
        } else {
            return NodeState.FAILURE;
        }       
    }

    public IEnumerator AttackPlayer() {
        CanAttack = false;
        Debug.Log("Long Range Attacking Player");
        GameObject tempBranch = GameObject.Instantiate(branch, enemyAI.transform.position, enemyAI.transform.rotation);
        yield return new WaitForSeconds(3.0f);
        CanAttack = true;
    }

    public void ChasePlayer() {
        enemyAI.transform.position = Vector2.MoveTowards(enemyAI.transform.position, PlayerController.Instance.gameObject.transform.position, 3 * Time.deltaTime);
    }

    public void ReturnToOriginalPos() {
        enemyAI.transform.position = Vector2.MoveTowards(enemyAI.transform.position, originalPosition, 3 * Time.deltaTime);
    }

    public float DistanceOfAiToPlayer() {
        return Vector2.Distance(enemyAI.transform.position, PlayerController.Instance.transform.position);
    }

    public IEnumerator WaitForAnimationToFinish() {
        Debug.Log(enemyAI.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
        yield return new WaitForSeconds(enemyAI.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length + 1.5f);
        CanAttack = true;
        HasFinishedTransforming = true;
    }
}
