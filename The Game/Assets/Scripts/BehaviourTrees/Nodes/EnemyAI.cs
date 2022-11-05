using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {
    [SerializeField] private List<Transform> PatrolSpots;
    [SerializeField] private GameObject Enemy;

    private Node topNode;

    private void Start() {
        ConstructBehahaviourTree();
    }

    private void ConstructBehahaviourTree() {     
        PatrolNode patrolNode = new PatrolNode(PatrolSpots, this.gameObject);

        Sequence sequence = new Sequence(new List<Node> { patrolNode });

        topNode = new Selector(new List<Node> { sequence });
    }

    private void Update() {
        topNode.Evaluate();
    }
}