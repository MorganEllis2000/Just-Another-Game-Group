// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Used to exit the dungeon and enter the overworld
/// </summary>
public class DungeonExit : MonoBehaviour
{
    public GameObject EnterText;
    public GameObject LoadingScreen;

    public IEnumerator LoadSceneAsync(int sceneID) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        LoadingScreen.SetActive(true);

        while (!operation.isDone) {
            yield return null;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            EnterText.SetActive(true);
            if (Input.GetKey(KeyCode.F)) {
                StartCoroutine(LoadSceneAsync(2));
                PlayerController.Instance.transform.position = new Vector3(11.4f, -60f, 0);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            EnterText.SetActive(false);
        }
    }
}
