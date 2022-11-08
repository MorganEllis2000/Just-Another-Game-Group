using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeAI : MonoBehaviour {
    [SerializeField] private GameObject Enemy;

    [Range(0.1f, 10f)]
    [SerializeField] private float EnemyMoveSpeed;
    [SerializeField] private Vector3 OriginalPosition;
    [SerializeField] private GameObject branch;

    private Node topNode;

    private void Start() {
        ConstructBehahaviourTree();
    }

    private void ConstructBehahaviourTree() {
        CloseRangeAttackNode closeRangeAttackNode = new CloseRangeAttackNode(this.gameObject, EnemyMoveSpeed);
        LongRangeAttack longRangeAttack = new LongRangeAttack(this.gameObject, EnemyMoveSpeed, OriginalPosition, branch);

        Sequence sequence = new Sequence(new List<Node> { closeRangeAttackNode, longRangeAttack });

        topNode = new Selector(new List<Node> { closeRangeAttackNode, longRangeAttack });
    }

    private void Update() {
        topNode.Evaluate();
    }
}