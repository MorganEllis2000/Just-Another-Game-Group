using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviourTrees {
    [System.Serializable]
    public abstract class Node {
        protected NodeState _nodeState;

        public NodeState nodeState { get { return _nodeState; } } // get the current state of our node

        public abstract NodeState Evaluate();
    }

    public enum NodeState {
        RUNNING,
        SUCCESS,
        FAILURE,
    }
}