// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Used to trigger the entrance to the dungeon
/// </summary>
public class DungeonEntrance : MonoBehaviour
{
    [SerializeField] private LoadingScene loadingScene;
    public GameObject EnterText;
    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            EnterText.SetActive(true);
            if (Input.GetKey(KeyCode.F)) {
                loadingScene.LoadScene(3);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            EnterText.SetActive(false);
        }
    }
}
