// WRITTEN BY: MORGAN ELLIS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// This is a type of helper script that can be used to load levels async within the game
/// </summary>
public class LoadingScene : MonoBehaviour
{

    public static LoadingScene Instance { get; private set; }

    public GameObject LoadingScreen;

    [SerializeField] private int ID;

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public void LoadScene(int sceneID) {
        StartCoroutine(LoadSceneAsync(sceneID));

        if(sceneID == 2) {
            PlayerController.Instance.transform.position = new Vector3(-32.6f, -0.9f, 0);
        }

        if (sceneID == 3) {
            PlayerController.Instance.transform.position = new Vector3(-25.2f, 23.7f, 0);
        }
    }

    public IEnumerator LoadSceneAsync(int sceneID) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        LoadingScreen.SetActive(true);

        while (!operation.isDone) {
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            LoadScene(2);

            if (SceneManager.GetActiveScene().name == "WFC") {
                PlayerController.Instance.transform.position = new Vector3(-25.2f, 23.7f, 0);
            }
        }
    }
}
