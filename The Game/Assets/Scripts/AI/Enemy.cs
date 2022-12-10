using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyDirection {
    NE,
    NW,
    SW,
    SE,
    NONE
}

public class Enemy : MonoBehaviour
{
    protected EnemyDirection enemyDirection;

    protected Animator animator;
    protected SpriteRenderer spriteRenderer;
    [Range(50, 300)]
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

    private void Awake() {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void UpdateTargetPosition() {
        target = PlayerController.Instance.transform.position;
    }


    public void ChasePlayer() {
        //this.transform.position = Vector2.MoveTowards(this.transform.position, PlayerController.Instance.gameObject.transform.position, 3 * Time.deltaTime);
        agent.SetDestination(new Vector3(target.x, target.y, transform.position.z));
        animator.SetBool("IsWalking", true);
        animator.SetBool("CanTransformBack", false);
    }

    public void ReturnToOriginalPos() {
        //this.transform.position = Vector2.MoveTowards(this.transform.position, OriginalPosition, 3 * Time.deltaTime);
        agent.SetDestination(new Vector3(OriginalPosition.x + 1, OriginalPosition.y, transform.position.z));
        if (Vector2.Distance(this.transform.position, OriginalPosition) < 0.1f) {
            this.GetComponent<Animator>().SetBool("IsWalking", false);
            this.GetComponent<Animator>().SetBool("CanTransform", false);
            this.GetComponent<Animator>().SetBool("CanTransformBack", true);
            
            CanMove = false;
            CanAttack = false;
        }
    }

    public float DistanceOfAiToPlayer() {
        return Vector2.Distance(this.transform.position, PlayerController.Instance.transform.position);

    }
}
