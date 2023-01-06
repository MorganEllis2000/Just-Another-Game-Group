using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{

    public static LoadingScene Instance { get; private set; }

    public GameObject LoadingScreen;
    public Image LoadingBarFill;
    public Slider slider;

    private void Awake() {

        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        }

        Instance = this;
    }

    public void LoadScene(int sceneID) {
        StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(int sceneID) {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        LoadingScreen.SetActive(true);

        while (!operation.isDone) {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            //LoadingBarFill.fillAmount = progressValue;
            slider.value = progressValue;

            yield return null;
        }
    }
}
