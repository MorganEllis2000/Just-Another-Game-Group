// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a helper script that can be placed on gameobjects with triggers in the scene
/// which inturn trigger game objects
/// </summary>
public class TriggerGameObject : MonoBehaviour
{
    public GameObject go;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            go.SetActive(true);
        }
    }
}
