//WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// This enum tracks which direction the enemy is facing
public enum EnemyDirection {
    NE,
    NW,
    SW,
    SE,
    NONE
}

/// <summary>
/// This is a parent class in which the enemies in the game are derived from
/// </summary>
public class Enemy : MonoBehaviour
{
    protected EnemyDirection enemyDirection;

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    public float Health;

    [Range(0.1f, 10f)]
    [SerializeField] protected float ShortRangeAttackRange;
    [Range(0.1f, 30f)]
    [SerializeField] protected float MinLongRangeAttackRange;
    [Range(0.1f, 30f)]
    [SerializeField] protected float MaxLongRangeAttackRange;
    [Range(0.1f, 30f)]
    [SerializeField] protected float TransformationRange;

    [SerializeField] protected Vector3 OriginalPosition;
    [SerializeField] protected bool CanAttack = false;
    [SerializeField] protected bool CanMove = false;

    NavMeshAgent agent;
    public Vector3 target;

    protected AudioSource source;
    [SerializeField] protected AudioClip TransformSound;
    [SerializeField] protected AudioClip ThrowSound;
    [SerializeField] protected AudioClip SmashSound;
    [SerializeField] protected AudioClip DeathSound;

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        OriginalPosition = this.transform.position;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Depedning on the players x and y coordinates relative to the position of the enemy the direction the enemy is facing is decided
    /// </summary>
    public void CheckEnemyDirection() {
        if (PlayerController.Instance.transform.position.x < this.gameObject.transform.position.x && PlayerController.Instance.transform.position.y < this.gameObject.transform.position.y) {
            enemyDirection = EnemyDirection.SW;
        } else if (PlayerController.Instance.transform.position.x > this.gameObject.transform.position.x && PlayerController.Instance.transform.position.y < this.gameObject.transform.position.y) {
            enemyDirection = EnemyDirection.SE;
        } else if (PlayerController.Instance.transform.position.x > this.gameObject.transform.position.x && PlayerController.Instance.transform.position.y > this.gameObject.transform.position.y) {
            enemyDirection = EnemyDirection.NE;
        } else if (PlayerController.Instance.transform.position.x < this.gameObject.transform.position.x && PlayerController.Instance.transform.position.y > this.gameObject.transform.position.y) {
            enemyDirection = EnemyDirection.NW;
        }
    }

    /// <summary>
    /// Use the enum to update the enemy sprite with the correct direction
    /// </summary>
    /// <param name="sprite"></param>
    public void UpdateEnemyDirection(SpriteRenderer sprite) {
        switch (enemyDirection) {
            case EnemyDirection.SW:
                sprite.flipX = true;
                animator.SetInteger("SetEnemyDirection", (int)enemyDirection);
                break;
            case EnemyDirection.SE:
                sprite.flipX = false;
                animator.SetInteger("SetEnemyDirection", (int)enemyDirection);
                break;
            case EnemyDirection.NW:
                sprite.flipX = true;
                animator.SetInteger("SetEnemyDirection", (int)enemyDirection);
                break;
            case EnemyDirection.NE:
                sprite.flipX = false;
                animator.SetInteger("SetEnemyDirection", (int)enemyDirection);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// Update the target position to that of the player so the enemy knows what to chase and throw to
    /// </summary>
    public void UpdateTargetPosition() {
        target = PlayerController.Instance.transform.position;
    }


    /// <summary>
    /// Sets the destination of the NavMeshAgent to the players position
    /// </summary>
    public void ChasePlayer() {
        if (Vector3.Distance(this.transform.position, PlayerController.Instance.transform.position) > 1.0f) {
            agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
            animator.SetBool("IsWalking", true);
            animator.SetBool("CanTransformBack", false);
        } 
    }

    /// <summary>
    /// When the enemy leaves the range in which they can attack the player they return to the their original position
    /// </summary>
    public void ReturnToOriginalPos() {
        agent.SetDestination(OriginalPosition);
        if (Vector2.Distance(this.transform.position, OriginalPosition) < 0.1f) {
            this.GetComponent<Animator>().SetBool("IsWalking", false);
            this.GetComponent<Animator>().SetBool("CanTransform", false);
            this.GetComponent<Animator>().SetBool("CanTransformBack", true);
            StopAllCoroutines();
            CanMove = false;
            CanAttack = false;
        }
    }

    /// <summary>
    /// Calculates the distance between this enemy and the player as a float
    /// </summary>
    /// <returns></returns>
    public float DistanceOfAiToPlayer() {
        return Vector2.Distance(this.transform.position, PlayerController.Instance.transform.position);
    }
}
