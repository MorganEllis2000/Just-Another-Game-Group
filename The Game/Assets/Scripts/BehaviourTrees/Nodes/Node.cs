using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public abstract class Node {
    /// <summary>
    /// https://www.linkedin.com/pulse/coroutines-inside-static-classes-simpler-than-ever-andersen-damian/
    /// How to use a coroutine without monobehvaiour inheritance
    /// </summary>
    //Create a class that actually inheritance from MonoBehaviour
    public class MyStaticMB : MonoBehaviour { }

    //Variable reference for the class
    private static MyStaticMB myStaticMB;

    //Now Initialize the variable (instance)
    private static void Init() {
        //If the instance not exit the first time we call the static class
        if (myStaticMB == null) {
            //Create an empty object called MyStatic
            GameObject gameObject = new GameObject("MyStatic");


            //Add this script to the object
            myStaticMB = gameObject.AddComponent<MyStaticMB>();
        }
    }

    //Now, a simple function
    public static void PerformCoroutine(IEnumerator coroutine) {
        //Call the Initialization
        Init();
        myStaticMB.StartCoroutine(coroutine);
    }

    protected NodeState _nodeState;

    public NodeState nodeState { get { return _nodeState; } } // get the current state of our node

    public abstract NodeState Evaluate();
}

public enum NodeState {
    RUNNING,
    SUCCESS,
    FAILURE,
}