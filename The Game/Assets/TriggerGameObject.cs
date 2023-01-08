using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGameObject : MonoBehaviour
{
    public GameObject go;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            go.SetActive(true);
        }
    }

    //private void OnTriggerExit2D(Collider2D collision) {
    //    if (collision.gameObject.CompareTag("Player")) {
    //        go.SetActive(false);
    //    }
    //}
}
