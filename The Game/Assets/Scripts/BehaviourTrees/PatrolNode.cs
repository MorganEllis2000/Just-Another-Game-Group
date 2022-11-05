using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolNode : Node
{
    private List<Transform> MoveSpots;
    private GameObject enemyAI;
    private bool IsPatrolling = false;
    public PatrolNode(List<Transform> moveSpots, GameObject enemyAI) {
        MoveSpots = moveSpots;
        this.enemyAI = enemyAI;
    }

    private int DestinationPoint = 0;
    private float WaitTime;
    private float startWaitTime = 2f;

    public override NodeState Evaluate() {
        if (DistanceOfAiToPlayer() < 8.0f && IsPatrolling == false && enemyAI.GetComponent<Animator>().GetBool("CanTransform") == false) {
            enemyAI.GetComponent<Animator>().SetBool("CanTransform", true);
            PerformCoroutine(WaitForAnimationToFinish());
            
        }

        WaitTime = startWaitTime;

        if(IsPatrolling == true) {
            if (CalculateDistanceFromAiToPoint() < 0.1) {
                PerformCoroutine(WaitAtPatrolPoint(1.5f));
                GoToNextPoint();
                return NodeState.SUCCESS;
            } else {
                CalculateDistanceFromAiToPoint();
                enemyAI.transform.position = Vector2.MoveTowards(enemyAI.transform.position, MoveSpots[DestinationPoint].position, 5 * Time.deltaTime);
                return NodeState.RUNNING;
            }
        } else {
            return NodeState.FAILURE;
        }
    }

    private float CalculateDistanceFromAiToPoint() {
        
        return Vector2.Distance(enemyAI.transform.position, MoveSpots[DestinationPoint].position);
    }

    IEnumerator WaitAtPatrolPoint(float a_seconds) {
        IsPatrolling = false;
        Debug.Log("WAITING FOR... " + a_seconds);
        yield return new WaitForSeconds(a_seconds);
        IsPatrolling = true;
    }

    void GoToNextPoint() {
        int sizeofList = MoveSpots.Count;
        //Debug.Log("Size of List: " + sizeofList);
        if (sizeofList == 0) {
            return;
        }

        DestinationPoint = (DestinationPoint + 1) % sizeofList;
    }

    public float DistanceOfAiToPlayer() {
        return Vector2.Distance(enemyAI.transform.position, PlayerController.Instance.transform.position);
    }

    public IEnumerator WaitForAnimationToFinish() {
        Debug.Log(enemyAI.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length);
        yield return new WaitForSeconds(enemyAI.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length + 1.5f);
        IsPatrolling = true;
    }
}
